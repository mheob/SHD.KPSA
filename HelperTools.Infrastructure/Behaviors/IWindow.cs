namespace HelperTools.Infrastructure.Behaviors
{
    using System;
    using System.Windows;

    /// <summary>Defines the interface for the Dialogs that are shown by <see cref="DialogActivationBehavior" />.</summary>
    public interface IWindow
    {
        /// <summary>Occurs when [closed].</summary>
        event EventHandler Closed;

        /// <summary>Gets or sets the content.</summary>
        /// <value>The content.</value>
        object Content { get; set; }

        /// <summary>Gets or sets the owner.</summary>
        /// <value>The owner.</value>
        object Owner { get; set; }


        /// <summary>Gets or sets the style.</summary>
        /// <value>The style.</value>
        Style Style { get; set; }


        /// <summary>Shows this instance.</summary>
        void Show();


        /// <summary>Closes this instance.</summary>
        void Close();
    }
}