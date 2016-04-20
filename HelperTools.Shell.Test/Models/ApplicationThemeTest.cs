namespace HelperTools.Shell.Test.Models
{
    using System.Windows.Media;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shell.Models;

    [TestClass]
    public class ApplicationThemeTest
    {
        private readonly ApplicationTheme ctt;
        private readonly Brush colorBrush;

        public ApplicationThemeTest()
        {
            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            ctt = new ApplicationTheme();
            colorBrush = new SolidColorBrush(Color.FromRgb(200, 100, 0));
        }

        [TestMethod]
        public void CanSetAndGetName()
        {
            const string excepted = "Test";
            ctt.Name = excepted;
            Assert.AreEqual(excepted, ctt.Name);
        }

        [TestMethod]
        public void CanSetAndGetColorBrush()
        {
            var excepted = colorBrush;
            ctt.ColorBrush = excepted;
            Assert.AreEqual(excepted, ctt.ColorBrush);
        }

        [TestMethod]
        public void CanSetAndGetBorderColorBrush()
        {
            var excepted = colorBrush;
            ctt.BorderColorBrush = excepted;
            Assert.AreEqual(excepted, ctt.BorderColorBrush);
        }
    }
}