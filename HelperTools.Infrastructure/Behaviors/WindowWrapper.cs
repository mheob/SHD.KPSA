namespace HelperTools.Infrastructure.Behaviors
{
    using System;
    using System.Windows;

    /// <summary>Defines a wrapper for the <see cref="Window" /> class that implements the <see cref="IWindow" /> interface.</summary>
    /// <seealso cref="IWindow" />
    public class WindowWrapper : IWindow
    {
        private readonly Window window;

        /// <summary>Initializes a new instance of the <see cref="WindowWrapper" /> class.</summary>
        public WindowWrapper()
        {
            window = new Window {WindowStartupLocation = WindowStartupLocation.CenterOwner};
        }

        /// <summary>Occurs when the <see cref="IWindow" /> is closed.</summary>
        public event EventHandler Closed
        {
            add { window.Closed += value; }
            remove { window.Closed -= value; }
        }

        /// <summary>Gets or sets the content for the <see cref="IWindow" />.</summary>
        public object Content
        {
            get { return window.Content; }
            set { window.Content = value; }
        }

        /// <summary>Gets or sets the owner control of the <see cref="IWindow" />.</summary>
        public object Owner
        {
            get { return window.Owner; }
            set { window.Owner = value as Window; }
        }

        /// <summary>Gets or sets the <see cref="System.Windows.Style" /> to apply to the <see cref="IWindow" />.</summary>
        public Style Style
        {
            get { return window.Style; }
            set { window.Style = value; }
        }

        /// <summary>Opens the <see cref="IWindow" />.</summary>
        public void Show()
        {
            window.ShowDialog();
        }

        /// <summary>Closes the <see cref="IWindow" />.</summary>
        public void Close()
        {
            window.Close();
        }
    }
}