namespace HelperTools.Infrastructure.Services
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows;
    using Interfaces;

    /// <summary>Keeps two lists synchronized.</summary>
    public class TwoListSynchronizerService : IWeakEventListener
    {
        private static readonly IListItemConverter DefaultConverter = new DoNothingListItemConverter();
        private readonly IList masterList;
        private readonly IListItemConverter masterTargetConverter;
        private readonly IList targetList;

        /// <summary>Initializes a new instance of the <see cref="TwoListSynchronizerService" /> class.</summary>
        /// <param name="masterList">The master list.</param>
        /// <param name="targetList">The target list.</param>
        /// <param name="masterTargetConverter">The master-target converter.</param>
        public TwoListSynchronizerService(IList masterList, IList targetList, IListItemConverter masterTargetConverter)
        {
            this.masterList = masterList;
            this.targetList = targetList;
            this.masterTargetConverter = masterTargetConverter;
        }

        /// <summary>Initializes a new instance of the <see cref="TwoListSynchronizerService" /> class.</summary>
        /// <param name="masterList">The master list.</param>
        /// <param name="targetList">The target list.</param>
        public TwoListSynchronizerService(IList masterList, IList targetList) : this(masterList, targetList, DefaultConverter)
        {
        }

        private delegate void ChangeListAction(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter);

        /// <summary>Starts synchronizing the lists.</summary>
        public void StartSynchronizing()
        {
            ListenForChangeEvents(masterList);
            ListenForChangeEvents(targetList);

            // Update the Target list from the Master list
            SetListValuesFromSource(masterList, targetList, ConvertFromMasterToTarget);

            // In some cases the target list might have its own view on which items should included:
            // so update the master list from the target list
            // (This is the case with a ListBox SelectedItems collection: only items from the ItemsSource can be included in SelectedItems)
            if (!TargetAndMasterCollectionsAreEqual())
            {
                SetListValuesFromSource(targetList, masterList, ConvertFromTargetToMaster);
            }
        }

        /// <summary>Stop synchronizing the lists.</summary>
        public void StopSynchronizing()
        {
            StopListeningForChangeEvents(masterList);
            StopListeningForChangeEvents(targetList);
        }

        /// <summary>Receives events from the centralized event manager.</summary>
        /// <param name="managerType">The type of the <see cref="T:System.Windows.WeakEventManager" /> calling this method.</param>
        /// <param name="sender">Object that originated the event.</param>
        /// <param name="e">Event data.</param>
        /// <returns>
        /// true if the listener handled the event. It is considered an error by the
        /// <see cref="T:System.Windows.WeakEventManager" /> handling in WPF�to register a listener for an event that the listener does
        /// not handle. Regardless, the method should return false if it receives an event that it does not recognize or handle.
        /// </returns>
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            HandleCollectionChanged(sender as IList, e as NotifyCollectionChangedEventArgs);

            return true;
        }

        /// <summary>Listens for change events on a list.</summary>
        /// <param name="list">The list to listen to.</param>
        protected void ListenForChangeEvents(IList list)
        {
            var source = list as INotifyCollectionChanged;
            if (source != null)
            {
                CollectionChangedEventManager.AddListener(source, this);
            }
        }

        /// <summary>Stops listening for change events.</summary>
        /// <param name="list">The list to stop listening to.</param>
        protected void StopListeningForChangeEvents(IList list)
        {
            var source = list as INotifyCollectionChanged;
            if (source != null)
            {
                CollectionChangedEventManager.RemoveListener(source, this);
            }
        }

        private static void AddItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter)
        {
            int itemCount = e.NewItems.Count;

            for (int i = 0; i < itemCount; i++)
            {
                int insertionPoint = e.NewStartingIndex + i;

                if (insertionPoint > list.Count)
                {
                    list.Add(converter(e.NewItems[i]));
                }
                else
                {
                    list.Insert(insertionPoint, converter(e.NewItems[i]));
                }
            }
        }

        private object ConvertFromMasterToTarget(object masterListItem)
        {
            return masterTargetConverter == null ? masterListItem : masterTargetConverter.Convert(masterListItem);
        }

        private object ConvertFromTargetToMaster(object targetListItem)
        {
            return masterTargetConverter == null ? targetListItem : masterTargetConverter.ConvertBack(targetListItem);
        }

        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IList sourceList = sender as IList;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    PerformActionOnAllLists(AddItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Move:
                    PerformActionOnAllLists(MoveItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    PerformActionOnAllLists(RemoveItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    PerformActionOnAllLists(ReplaceItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    UpdateListsFromSource(sender as IList);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void MoveItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter)
        {
            RemoveItems(list, e, converter);
            AddItems(list, e, converter);
        }

        private void PerformActionOnAllLists(ChangeListAction action, IEnumerable sourceList,
            NotifyCollectionChangedEventArgs collectionChangedArgs)
        {
            if (Equals(sourceList, masterList))
            {
                PerformActionOnList(targetList, action, collectionChangedArgs, ConvertFromMasterToTarget);
            }
            else
            {
                PerformActionOnList(masterList, action, collectionChangedArgs, ConvertFromTargetToMaster);
            }
        }

        private void PerformActionOnList(IList list, ChangeListAction action,
            NotifyCollectionChangedEventArgs collectionChangedArgs, Converter<object, object> converter)
        {
            StopListeningForChangeEvents(list);
            action(list, collectionChangedArgs, converter);
            ListenForChangeEvents(list);
        }

        private static void RemoveItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter)
        {
            int itemCount = e.OldItems.Count;

            // for the number of items being removed, remove the item from the Old Starting Index
            // (this will cause following items to be shifted down to fill the hole).
            for (int i = 0; i < itemCount; i++)
            {
                list.RemoveAt(e.OldStartingIndex);
            }
        }

        private static void ReplaceItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter)
        {
            RemoveItems(list, e, converter);
            AddItems(list, e, converter);
        }

        private void SetListValuesFromSource(IEnumerable sourceList, IList targetsList, Converter<object, object> converter)
        {
            StopListeningForChangeEvents(targetsList);

            targetsList.Clear();

            foreach (object o in sourceList)
            {
                targetsList.Add(converter(o));
            }

            ListenForChangeEvents(targetsList);
        }

        private bool TargetAndMasterCollectionsAreEqual()
        {
            return masterList.Cast<object>().SequenceEqual(targetList.Cast<object>().Select(ConvertFromTargetToMaster));
        }

        /// <summary>Makes sure that all synchronized lists have the same values as the source list.</summary>
        /// <param name="sourceList">The source list.</param>
        private void UpdateListsFromSource(IEnumerable sourceList)
        {
            if (Equals(sourceList, masterList))
            {
                SetListValuesFromSource(masterList, targetList, ConvertFromMasterToTarget);
            }
            else
            {
                SetListValuesFromSource(targetList, masterList, ConvertFromTargetToMaster);
            }
        }

        /// <summary>An implementation that does nothing in the conversions.</summary>
        internal class DoNothingListItemConverter : IListItemConverter
        {
            /// <summary>Converts the specified master list item.</summary>
            /// <param name="masterListItem">The master list item.</param>
            /// <returns>The result of the conversion.</returns>
            public object Convert(object masterListItem)
            {
                return masterListItem;
            }

            /// <summary>Converts the specified target list item.</summary>
            /// <param name="targetListItem">The target list item.</param>
            /// <returns>The result of the conversion.</returns>
            public object ConvertBack(object targetListItem)
            {
                return targetListItem;
            }
        }
    }
}