namespace HelperTools.MatFileGen.Test.ViewModels
{
    using System.Windows.Media;
    using HelperTools.MatFileGen.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SettingsThumbnailViewModelTest
    {
        private readonly SettingsThumbnailViewModel vm;
        private const string EXEPTED_STRING = "Test";
        private readonly Color balckColor = Color.FromRgb(0, 0, 0);
        private readonly Color whiteColor = Color.FromRgb(255, 255, 255);

        public SettingsThumbnailViewModelTest()
        {
            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            vm = new SettingsThumbnailViewModel();
        }

        [TestMethod]
        public void CanCreateSettingsThumbnailViewModel()
        {
            var viewModel = new SettingsThumbnailViewModel();
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void CanSetAndGetGenerateThumb()
        {
            vm.GenerateThumb = true;
            Assert.IsTrue(vm.GenerateThumb);
        }

        [TestMethod]
        public void CanSetAndGetThumbFolder()
        {
            const string expected = "thumb";
            Assert.AreEqual(expected, vm.ThumbFolder);
            vm.ThumbFolder = EXEPTED_STRING;
            Assert.AreEqual(EXEPTED_STRING, vm.ThumbFolder);
        }

        [TestMethod]
        public void CanSetAndGetGenerateOuterFrame()
        {
            vm.GenerateOuterFrame = true;
            Assert.IsTrue(vm.GenerateOuterFrame);
        }

        [TestMethod]
        public void CanSetAndGetOuterFrameColor()
        {
            Assert.AreEqual(balckColor, vm.OuterFrameColor);
            vm.OuterFrameColor = whiteColor;
            Assert.AreEqual(whiteColor, vm.OuterFrameColor);
        }

        [TestMethod]
        public void CanSetAndGetOuterFrameSize()
        {
            Assert.AreEqual(2, vm.OuterFrameSize);
            const int expected = 5;
            vm.OuterFrameSize = expected;
            Assert.AreEqual(expected, vm.OuterFrameSize);
        }

        [TestMethod]
        public void CanSetAndGetGenerateInnerFrame()
        {
            vm.GenerateInnerFrame = true;
            Assert.IsTrue(vm.GenerateInnerFrame);
        }

        [TestMethod]
        public void CanSetAndGetInnerFrameColor()
        {
            Assert.AreEqual(whiteColor, vm.InnerFrameColor);
            vm.InnerFrameColor = balckColor;
            Assert.AreEqual(balckColor, vm.InnerFrameColor);
        }

        [TestMethod]
        public void CanSetAndGetInnerFrameSize()
        {
            Assert.AreEqual(1, vm.InnerFrameSize);
            const int expected = 4;
            vm.InnerFrameSize = expected;
            Assert.AreEqual(expected, vm.InnerFrameSize);
        }
    }
}