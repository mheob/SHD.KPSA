namespace HelperTools.Infrastructure.Behaviors
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using Prism.Regions;
    using Prism.Regions.Behaviors;

    /// <summary>Defines a behavior that creates a Dialog to display the active view of the target <see cref="IRegion" />.</summary>
    /// <seealso cref="RegionBehavior" />
    /// <seealso cref="IHostAwareRegionBehavior" />
    public abstract class DialogActivationBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        /// <summary>The key of this behavior</summary>
        public const string BEHAVIOR_KEY = "DialogActivation";

        private IWindow contentDialog;


        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.DependencyObject" /> that the <see cref="T:Prism.Regions.IRegion" />
        /// is attached to.
        /// </summary>
        /// <value>
        /// A <see cref="T:System.Windows.DependencyObject" /> that the <see cref="T:Prism.Regions.IRegion" /> is attached to.
        /// This is usually a <see cref="T:System.Windows.FrameworkElement" /> that is part of the tree.
        /// </value>
        public DependencyObject HostControl { get; set; }

        /// <summary>Override this method to perform the logic after the behavior has been attached.</summary>
        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }


        /// <summary>Creates the window.</summary>
        /// <returns></returns>
        protected abstract IWindow CreateWindow();

        [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                CloseContentDialog();
                PrepareContentDialog(e.NewItems[0]);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                CloseContentDialog();
            }
        }

        private Style GetStyleForView()
        {
            return HostControl.GetValue(RegionPopupBehaviors.ContainerWindowStyleProperty) as Style;
        }

        private void PrepareContentDialog(object view)
        {
            contentDialog = CreateWindow();
            contentDialog.Content = view;
            contentDialog.Owner = HostControl;
            contentDialog.Closed += ContentDialogClosed;
            contentDialog.Style = GetStyleForView();
            contentDialog.Show();
        }

        private void CloseContentDialog()
        {
            if (contentDialog == null)
                return;

            contentDialog.Closed -= ContentDialogClosed;
            contentDialog.Close();
            contentDialog.Content = null;
            contentDialog.Owner = null;
        }

        private void ContentDialogClosed(object sender, EventArgs e)
        {
            Region.Remove(contentDialog.Content);
            CloseContentDialog();
        }
    }
}