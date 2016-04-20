namespace HelperTools.Navigation.Test.ViewModels
{
    using HelperTools.Navigation.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HomeTilesViewModelTest
    {
        private readonly HomeTilesViewModel vm;

        public HomeTilesViewModelTest()
        {
            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            vm = new HomeTilesViewModel();
        }

        [TestMethod]
        public void CanCreateHomeTilesViewModel()
        {
            var viewModel = new HomeTilesViewModel();
            Assert.IsNotNull(viewModel);
        }
    }
}