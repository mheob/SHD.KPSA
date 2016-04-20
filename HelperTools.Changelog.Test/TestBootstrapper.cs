namespace HelperTools.Changelog.Test
{
    using System.Windows;
    using Prism.Unity;

    public class TestBootstrapper : UnityBootstrapper
    {
        #region Overrides of Bootstrapper
        protected override DependencyObject CreateShell()
        {
            return new DependencyObject();
        }
        #endregion
    }
}