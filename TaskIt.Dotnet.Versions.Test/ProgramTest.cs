using Xunit;

namespace TaskIt.Dotnet.Versions.Test
{
    public class ProgramTest
    {
        [Fact]
        public void TestSet()
        {
            string[] args = new string[]
            { "set",
                "-s", @"C:\Users\cb\Documents\Projekte\TaskIt.Common\TaskIt.Common.sln",
                "-v", "4.0.1" };
            var testResult = TaskIt.Dotnet.Versions.Program.Main(args);
            Assert.True(testResult == 0, $"Not successful. Result: {testResult}");
        }

        [Fact]
        public void TestSetWithSemver()
        {
            string[] args = new string[]
            { "set",
                "-s", @"C:\Users\cb\Documents\Projekte\TaskIt.Common\TaskIt.Common.sln",
                "-v", "4.0.1-RC1" };
            var testResult = TaskIt.Dotnet.Versions.Program.Main(args);
            Assert.True(testResult == 0, $"Not successful. Result: {testResult}");
        }

        [Fact]
        public void TestSetKeepVersionAddSemver()
        {
            string[] args = new string[]
            { "set",
                "-s", @"C:\Users\cb\Documents\Projekte\TaskIt.Common\TaskIt.Common.sln",
                "-v", "*.*.*-SNAPSHOT1" };
            var testResult = TaskIt.Dotnet.Versions.Program.Main(args);
            Assert.True(testResult == 0, $"Not successful. Result: {testResult}");
        }

        [Fact]
        public void TestSetKeepVersionRemoveSemver()
        {
            string[] args = new string[]
            { "set",
                "-s", @"C:\Users\cb\Documents\Projekte\TaskIt.Common\TaskIt.Common.sln",
                "-v", "*.*.*" };
            var testResult = TaskIt.Dotnet.Versions.Program.Main(args);
            Assert.True(testResult == 0, $"Not successful. Result: {testResult}");
        }

        [Fact]
        public void TestMod()
        {
            string[] args = new string[]
            { "mod",
                "-s", @"C:\Users\cb\Documents\Projekte\TaskIt.Common\TaskIt.Common.sln",
                "-i", "1",
                "-t", "0",
                "-r", "RC(\\d+)",
                "-v", "1"
            };
            var testResult = TaskIt.Dotnet.Versions.Program.Main(args);

            Assert.True(testResult == 0, $"Not successful. Result: {testResult}");

        }
    }
}
