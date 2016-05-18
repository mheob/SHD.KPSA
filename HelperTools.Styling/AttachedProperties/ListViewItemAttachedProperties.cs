namespace HelperTools.Styling.AttachedProperties
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>The ListViewItemAttachedProperties.</summary>
    public class ListViewItemAttachedProperties
    {
        #region IsSelectableProperty
        /// <summary>The is selectable property.</summary>
        public static readonly DependencyProperty IsSelectableProperty = DependencyProperty.RegisterAttached("IsSelectable", typeof(bool),
            typeof(ListViewItemAttachedProperties), new UIPropertyMetadata(true, IsSelectableChanged));

        /// <summary>Gets the is selectable.</summary>
        /// <param name="obj">The object.</param>
        /// <returns>The value if the <see cref="IsSelectableProperty" /> is set.</returns>
        public static bool GetIsSelectable(ListViewItem obj)
        {
            return (bool) obj.GetValue(IsSelectableProperty);
        }

        /// <summary>Sets the is selectable.</summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <c>true</c> set value.</param>
        public static void SetIsSelectable(ListViewItem obj, bool value)
        {
            obj.SetValue(IsSelectableProperty, value);
        }

        private static void IsSelectableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListViewItem item = d as ListViewItem;
            if (item == null) return;

            if ((bool) e.NewValue == false && (bool) e.OldValue)
            {
                item.Selected -= item_Selected;
                item.Selected += item_Selected;
                BindingOperations.ClearBinding(item, ListBoxItem.IsSelectedProperty);

                if (item.IsSelected) item.IsSelected = false;
            }
            else if ((bool) e.NewValue && (bool) e.OldValue == false)
            {
                item.Selected -= item_Selected;
            }
        }

        private static void item_Selected(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item == null) return;

            item.IsSelected = false;
        }
        #endregion IsSelectableProperty
    }
}