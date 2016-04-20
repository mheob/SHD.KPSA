namespace HelperTools.Shell.Test.ViewModels
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shell.ViewModels;

    [TestClass]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class MainWindowViewModelTest
    {
        private MainWindowViewModel viewModel;

        public MainWindowViewModelTest()
        {
            if (Application.Current == null)
            {
                App app = new App();
            }

            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            viewModel = new MainWindowViewModel();
        }

        [TestMethod]
        public void CanCreateMainWindowViewModel()
        {
            viewModel = new MainWindowViewModel();
        }

        [TestMethod]
        public void CanGetStatusbarMessage()
        {
            Assert.IsNotNull(viewModel.StatusBarMessage);
        }

        [TestMethod]
        public void CanSetStatusbarMessage()
        {
            const string expected = "test";
            viewModel.StatusBarMessage = expected;
            Assert.AreEqual(expected, viewModel.StatusBarMessage);
        }

        [TestMethod]
        public void CanOnStatusBarMessageUpdateEvent()
        {
            // TODO: write test for private function
        }
    }
}