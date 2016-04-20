namespace HelperTools.Changelog.Test.ViewModels
{
    using System.IO;
    using System.Linq;
    using System.Windows.Controls;
    using HelperTools.Changelog.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ChangelogListViewModelTest
    {
        private readonly ChangelogListViewModel vm;

        public ChangelogListViewModelTest()
        {
            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            vm = new ChangelogListViewModel();
        }

        [TestMethod]
        public void CanCreateChangelogListViewModel()
        {
            var viewModel = new ChangelogListViewModel();
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void GetTheCorrectChangelogFile()
        {
            var expected = Directory.GetCurrentDirectory() + @"\Resources\CHANGELOG.md";
            Assert.AreEqual(expected, vm.ChangelogFile);
        }

        [TestMethod]
        public void GetTheBuildNoteIsNotNull()
        {
            Assert.IsNotNull(vm.BuildNote);
        }

        [TestMethod]
        public void CanSetAndGetChangelogLines()
        {
            var expected = new Label {Content = "Test"};
            vm.ChangelogLines.Clear();
            vm.ChangelogLines.Add(new Label {Content = "Test"});
            Assert.AreEqual(expected.Content, vm.ChangelogLines.FirstOrDefault()?.Content);
            Assert.IsInstanceOfType(vm.ChangelogLines.FirstOrDefault(), typeof (Label));
        }

        [TestMethod]
        private void TestDesignChangelogContent()
        {
            // TODO: write test for these function
        }
    }
}