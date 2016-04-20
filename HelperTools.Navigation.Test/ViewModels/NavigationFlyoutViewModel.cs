namespace HelperTools.Navigation.Test.ViewModels
{
    using HelperTools.Navigation.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NavigationFlyoutViewModelTest
    {
        private readonly NavigationFlyoutViewModel vm;

        public NavigationFlyoutViewModelTest()
        {
            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            vm = new NavigationFlyoutViewModel();
        }

        [TestMethod]
        public void CanCreateNavigationFlyoutViewModel()
        {
            var viewModel = new NavigationFlyoutViewModel();
            Assert.IsNotNull(viewModel);
        }
    }
}