namespace HelperTools.MatFileGen.Test.ViewModels
{
    using System.Windows.Media;
    using HelperTools.MatFileGen.ViewModels;
    using Infrastructure.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SettingsSolidViewModelTest
    {
        private readonly SettingsSolidViewModel vm;
        private const string EXEPTED_STRING = "Test";

        public SettingsSolidViewModelTest()
        {
            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            vm = new SettingsSolidViewModel();
        }

        [TestMethod]
        public void CanCreateSettingsSolidViewModel()
        {
            var viewModel = new SettingsSolidViewModel();
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void CanSetAndGetSolidColorName()
        {
            vm.SolidColorName = EXEPTED_STRING;
            Assert.AreEqual(CharConverterService.ConvertCharsToAscii(EXEPTED_STRING), vm.SolidColorName);
        }

        [TestMethod]
        public void CanSetAndGetSelectedColor()
        {
            var exepted = Color.FromRgb(98, 87, 76);
            vm.SelectedColor = exepted;
            Assert.AreEqual(exepted, vm.SelectedColor);
        }
    }
}