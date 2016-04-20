namespace HelperTools.Navigation.Test.ViewModels
{
    using HelperTools.Navigation.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LeftTitlebarCommandsViewModelTest
    {
        private readonly LeftTitlebarCommandsViewModel vm;

        public LeftTitlebarCommandsViewModelTest()
        {
            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            vm = new LeftTitlebarCommandsViewModel();
        }

        [TestMethod]
        public void CanCreateLeftTitlebarCommandsViewModel()
        {
            var viewModel = new LeftTitlebarCommandsViewModel();
            Assert.IsNotNull(viewModel);
        }
    }
}