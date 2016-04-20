namespace HelperTools.Shell.Test.Models
{
    using System.Windows.Media;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shell.Models;

    [TestClass]
    public class AccentColorTest
    {
        private readonly AccentColor ctt;
        private readonly Brush colorBrush;

        public AccentColorTest()
        {
            TestBootstrapper bootstrapper = new TestBootstrapper();
            bootstrapper.Run();

            ctt = new AccentColor();
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
    }
}