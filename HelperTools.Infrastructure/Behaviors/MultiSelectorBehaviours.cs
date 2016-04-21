namespace HelperTools.Infrastructure.Behaviors
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using Services;

    /// <summary>A sync behavior for a multiselector.</summary>
    public static class MultiSelectorBehaviours
    {
        /// <summary>...</summary>
        public static readonly DependencyProperty SynchronizedSelectedItems = DependencyProperty.RegisterAttached("SynchronizedSelectedItems",
            typeof(IList), typeof(MultiSelectorBehaviours), new PropertyMetadata(null, OnSynchronizedSelectedItemsChanged));

        private static readonly DependencyProperty SynchronizationManagerProperty = DependencyProperty.RegisterAttached("SynchronizationManager",
            typeof(SynchronizationManager), typeof(MultiSelectorBehaviours), new PropertyMetadata(null));

        /// <summary>Gets the synchronized selected items.</summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns>The list that is acting as the sync list.</returns>
        public static IList GetSynchronizedSelectedItems(DependencyObject dependencyObject)
        {
            return (IList) dependencyObject.GetValue(SynchronizedSelectedItems);
        }

        /// <summary>Sets the synchronized selected items.</summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="value">The value to be set as synchronized items.</param>
        public static void SetSynchronizedSelectedItems(DependencyObject dependencyObject, IList value)
        {
            dependencyObject.SetValue(SynchronizedSelectedItems, value);
        }

        private static SynchronizationManager GetSynchronizationManager(DependencyObject dependencyObject)
        {
            return (SynchronizationManager) dependencyObject.GetValue(SynchronizationManagerProperty);
        }

        private static void SetSynchronizationManager(DependencyObject dependencyObject, SynchronizationManager value)
        {
            dependencyObject.SetValue(SynchronizationManagerProperty, value);
        }

        private static void OnSynchronizedSelectedItemsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                SynchronizationManager synchronizer = GetSynchronizationManager(dependencyObject);
                synchronizer.StopSynchronizing();

                SetSynchronizationManager(dependencyObject, null);
            }

            IList list = e.NewValue as IList;
            Selector selector = dependencyObject as Selector;

            // check that this property is an IList, and that it is being set on a ListBox
            if (list != null && selector != null)
            {
                SynchronizationManager synchronizer = GetSynchronizationManager(dependencyObject);
                if (synchronizer == null)
                {
                    synchronizer = new SynchronizationManager(selector);
                    SetSynchronizationManager(dependencyObject, synchronizer);
                }

                synchronizer.StartSynchronizingList();
            }
        }

        /// <summary>A synchronization manager.</summary>
        private class SynchronizationManager
        {
            private readonly Selector multiSelector;
            private TwoListSynchronizerService synchronizer;

            /// <summary>Initializes a new instance of the <see cref="SynchronizationManager" /> class.</summary>
            /// <param name="selector">The selector.</param>
            internal SynchronizationManager(Selector selector)
            {
                multiSelector = selector;
            }

            /// <summary>Starts synchronizing the list.</summary>
            public void StartSynchronizingList()
            {
                IList list = GetSynchronizedSelectedItems(multiSelector);

                if (list == null)
                {
                    return;
                }

                synchronizer = new TwoListSynchronizerService(GetSelectedItemsCollection(multiSelector), list);
                synchronizer.StartSynchronizing();
            }

            /// <summary>Stops synchronizing the list.</summary>
            public void StopSynchronizing()
            {
                synchronizer.StopSynchronizing();
            }

            private static IList GetSelectedItemsCollection(Selector selector)
            {
                var multiSelector = selector as MultiSelector;
                if (multiSelector != null)
                {
                    return multiSelector.SelectedItems;
                }

                var box = selector as ListBox;
                if (box != null)
                {
                    return box.SelectedItems;
                }
                throw new InvalidOperationException("Target object has no SelectedItems property to bind.");
            }
        }
    }
}