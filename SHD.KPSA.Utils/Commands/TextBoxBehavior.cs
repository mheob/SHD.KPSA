namespace SHD.KPSA.Utils.Commands
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// A behavior class for text boxes.
    /// </summary>
    public class TextBoxBehavior
    {
        /// <summary>
        /// A <see cref="DependencyProperty" /> with the <see cref="OnLostFocus" /> event.
        /// </summary>
        public static DependencyProperty OnLostFocusProperty = DependencyProperty.RegisterAttached("OnLostFocus",
            typeof (ICommand), typeof (TextBoxBehavior), new UIPropertyMetadata(OnLostFocus));

        /// <summary>
        /// Sets the <see cref="ICommand" /> to the <see cref="OnLostFocusProperty" />.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="value">The value.</param>
        public static void SetOnLostFocus(DependencyObject target, ICommand value)
        {
            target.SetValue(OnLostFocusProperty, value);
        }

        /// <summary>
        /// PropertyChanged callback for OnLostFocusProperty. Hooks up an event handler with the actual target.
        /// </summary>
        private static void OnLostFocus(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var element = target as TextBox;

            if (element == null)
            {
                throw new InvalidOperationException("This behavior can be attached to a TextBox item only.");
            }

            if ((e.NewValue != null) && (e.OldValue == null))
            {
                element.LostFocus += OnPreviewLostFocus;
            }
            else if ((e.NewValue == null) && (e.OldValue != null))
            {
                element.LostFocus -= OnPreviewLostFocus;
            }
        }

        private static void OnPreviewLostFocus(object sender, RoutedEventArgs e)
        {
            UIElement element = (UIElement) sender;
            ICommand command = (ICommand) element.GetValue(OnLostFocusProperty);

            command?.Execute(e);
        }
    }
}