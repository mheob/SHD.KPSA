namespace HelperTools.MatFileGen.Test.ViewModels
{
    using System.Linq;
    using HelperTools.MatFileGen.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SettingsAttributesViewModelTest
    {
        private readonly SettingsAttributesViewModel vm;
        private const string EXEPTED_STRING = "Test";

        public SettingsAttributesViewModelTest()
        {
            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            vm = new SettingsAttributesViewModel();
        }

        [TestMethod]
        public void CanCreateSettingsAttributesViewModel()
        {
            var viewModel = new SettingsAttributesViewModel();
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void CanSetAndGetAddScale()
        {
            vm.AddScale = true;
            Assert.IsTrue(vm.AddScale);
        }

        [TestMethod]
        public void CanGetComboBoxScaleCollection()
        {
            const string expected = "0.1";
            Assert.IsTrue(vm.ComboBoxScaleCollection.Count > 1);
            Assert.AreEqual(expected, vm.ComboBoxScaleCollection.FirstOrDefault());
        }

        [TestMethod]
        public void CanSetAndGetSelectedScaleX()
        {
            vm.SelectedScaleX = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.SelectedScaleX);
        }

        [TestMethod]
        public void CanSetAndGetSelectedScaleY()
        {
            vm.SelectedScaleY = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.SelectedScaleY);
        }

        [TestMethod]
        public void CanSetAndGetSelectedScaleZ()
        {
            vm.SelectedScaleZ = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.SelectedScaleZ);
        }

        [TestMethod]
        public void CanSetAndGetAddRotate()
        {
            vm.AddRotate = true;
            Assert.IsTrue(vm.AddRotate);
        }

        [TestMethod]
        public void CanGetComboBoxRotateCollection()
        {
            const string expected = "0";
            Assert.IsTrue(vm.ComboBoxRotateCollection.Count > 1);
            Assert.AreEqual(expected, vm.ComboBoxRotateCollection.FirstOrDefault());
        }

        [TestMethod]
        public void CanSetAndGetSelectedRotateX()
        {
            vm.SelectedRotateX = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.SelectedRotateX);
        }

        [TestMethod]
        public void CanSetAndGetSelectedRotateY()
        {
            vm.SelectedRotateY = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.SelectedRotateY);
        }

        [TestMethod]
        public void CanSetAndGetSelectedRotateZ()
        {
            vm.SelectedRotateZ = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.SelectedRotateZ);
        }

        [TestMethod]
        public void CanSetAndGetAddAuto()
        {
            vm.AddAuto = true;
            Assert.IsTrue(vm.AddAuto);
        }

        [TestMethod]
        public void CanSetAndGetAddRauto()
        {
            vm.AddRauto = true;
            Assert.IsTrue(vm.AddRauto);
        }

        [TestMethod]
        public void CanSetAndGetAddGlass()
        {
            vm.AddGlass = true;
            Assert.IsTrue(vm.AddGlass);
        }

        [TestMethod]
        public void CanSetAndGetAddMirror()
        {
            vm.AddMirror = true;
            Assert.IsTrue(vm.AddMirror);
        }

        [TestMethod]
        public void CanSetAndGetMirror()
        {
            vm.Mirror = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.Mirror);
        }

        [TestMethod]
        public void CanSetAndGetAddShi()
        {
            vm.AddShi = true;
            Assert.IsTrue(vm.AddShi);
        }

        [TestMethod]
        public void CanSetAndGetShi()
        {
            vm.Shi = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.Shi);
        }

        [TestMethod]
        public void CanSetAndGetAddRef()
        {
            vm.AddRef = true;
            Assert.IsTrue(vm.AddRef);
        }

        [TestMethod]
        public void CanSetAndGetRef()
        {
            vm.Ref = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.Ref);
        }

        [TestMethod]
        public void CanSetAndGetAddTra()
        {
            vm.AddTra = true;
            Assert.IsTrue(vm.AddTra);
        }

        [TestMethod]
        public void CanSetAndGetTra()
        {
            vm.Tra = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.Tra);
        }

        [TestMethod]
        public void CanInitComboBoxes()
        {
            // TODO: write test for private function
        }
    }
}