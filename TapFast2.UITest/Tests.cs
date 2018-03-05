using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TapFast2.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            //app.
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
            //app.Repl();
            app.Flash("New Game");
            app.Tap("New Game");
            app.Screenshot("Tapping on New Game");

        }
    }
}

