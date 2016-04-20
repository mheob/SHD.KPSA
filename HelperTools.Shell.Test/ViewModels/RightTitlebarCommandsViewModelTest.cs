namespace HelperTools.Shell.Test.ViewModels
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shell.ViewModels;

    [TestClass]
    public class RightTitlebarCommandsViewModelTest
    {
        private readonly RightTitlebarCommandsViewModel vm;

        public RightTitlebarCommandsViewModelTest()
        {
            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            vm = new RightTitlebarCommandsViewModel();
        }

        [TestMethod]
        public void CanCreateRightTitlebarCommandsViewModel()
        {
            var viewModel = new RightTitlebarCommandsViewModel();
            Assert.IsNotNull(viewModel);
        }
    }
}