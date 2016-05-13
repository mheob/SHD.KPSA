namespace HelperTools.Infrastructure.Behaviors
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using Microsoft.Practices.ServiceLocation;
    using Prism.Regions;

    /// <summary>Declares the Attached Properties and Behaviors for implementing Popup regions.</summary>
    /// <remarks>
    /// Although the fastest way is to create a RegionAdapter for a Window and register it with the RegionAdapterMappings,
    /// this would be conceptually incorrect because we want to create a new popup window every time a view is added  (instead of
    /// having a Window as a host control and replacing its contents every time Views are added, as other adapters do). This is why
    /// we have a different class for this behavior, instead of reusing the <see cref="RegionManager.RegionNameProperty" /> attached
    /// property.
    /// </remarks>
    public static class RegionPopupBehaviors
    {
        /// <summary>The create popup region with name property</summary>
        public static readonly DependencyProperty CreatePopupRegionWithNameProperty =
            DependencyProperty.RegisterAttached("CreatePopupRegionWithName", typeof(string), typeof(RegionPopupBehaviors),
                new PropertyMetadata(CreatePopupRegionWithNamePropertyChanged));

        /// <summary>The container window style property</summary>
        public static readonly DependencyProperty ContainerWindowStyleProperty =
            DependencyProperty.RegisterAttached("ContainerWindowStyle", typeof(Style), typeof(RegionPopupBehaviors), null);

        /// <summary>Gets the name of the create popup region with.</summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string GetCreatePopupRegionWithName(DependencyObject owner)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));

            return owner.GetValue(CreatePopupRegionWithNameProperty) as string;
        }

        /// <summary>Sets the name of the create popup region with.</summary>
        /// <param name="owner">The owner.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static void SetCreatePopupRegionWithName(DependencyObject owner, string value)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));

            owner.SetValue(CreatePopupRegionWithNameProperty, value);
        }

        /// <summary>Gets the container window style.</summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static Style GetContainerWindowStyle(DependencyObject owner)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));

            return owner.GetValue(ContainerWindowStyleProperty) as Style;
        }

        /// <summary>Sets the container window style.</summary>
        /// <param name="owner">The owner.</param>
        /// <param name="style">The style.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static void SetContainerWindowStyle(DependencyObject owner, Style style)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));

            owner.SetValue(ContainerWindowStyleProperty, style);
        }

        /// <summary>Registers the new popup region.</summary>
        /// <param name="owner">The owner.</param>
        /// <param name="regionName">Name of the region.</param>
        public static void RegisterNewPopupRegion(DependencyObject owner, string regionName)
        {
            // Creates a new region and registers it in the default region manager.
            // Another option if you need the complete infrastructure with the default region behaviors is to extend
            // DelayedRegionCreationBehavior overriding the CreateRegion method and create an instance of it that will be in 
            // charge of registering the Region once a RegionManager is set as an attached property in the Visual Tree.
            IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (regionManager == null) return;

            IRegion region = new SingleActiveRegion();

            DialogActivationBehavior behavior = new WindowDialogActivationBehavior();
            behavior.HostControl = owner;

            region.Behaviors.Add(DialogActivationBehavior.BEHAVIOR_KEY, behavior);
            regionManager.Regions.Add(regionName, region);
        }

        private static void CreatePopupRegionWithNamePropertyChanged(DependencyObject hostControl, DependencyPropertyChangedEventArgs e)
        {
            if (IsInDesignMode(hostControl)) return;

            RegisterNewPopupRegion(hostControl, e.NewValue as string);
        }

        private static bool IsInDesignMode(DependencyObject element)
        {
            // Due to a known issue in Cider, GetIsInDesignMode attached property value is not enough to know if it's in design mode.
            return DesignerProperties.GetIsInDesignMode(element) || Application.Current == null ||
                   Application.Current.GetType() == typeof(Application);
        }
    }
}