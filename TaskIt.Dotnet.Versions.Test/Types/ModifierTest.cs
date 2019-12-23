using TaskIt.Dotnet.Versions.Types;
using Xunit;

namespace TaskIt.Dotnet.Versions.Test.Types
{
    public class ModifierTest
    {
        [Fact]
        public void TestConstructionSimple()
        {
            var testdata = new Modifier("*.1.0");
            Assert.False(testdata.Major.HasValue);
            Assert.True(testdata.Minor.HasValue);
            Assert.True(testdata.Patch.HasValue);
            Assert.True(string.IsNullOrEmpty(testdata.SemverPattern));
            Assert.False(testdata.Semver.HasValue);
        }

        [Fact]
        public void TestConstructionSemver()
        {
            var testdata = new Modifier("*.*.*", @"RC(\d+)", 1);
            Assert.False(testdata.Major.HasValue);
            Assert.False(testdata.Minor.HasValue);
            Assert.False(testdata.Patch.HasValue);
            Assert.False(string.IsNullOrEmpty(testdata.SemverPattern));
            Assert.True(testdata.Semver.HasValue);
        }

        [Fact]
        public void TestModify()
        {
            var testdata = new Modifier("*.2.0", @"beta(\d+)", 1);
            var testVersion = new ProjectVersion("5.1.1-RC12-beta1+build47");

            var testResult = testdata.Modify(testVersion, true);
            Assert.True(testResult.Equals("5.3.0-RC12-beta2+build47"));

        }

        [Fact]
        public void TestOverwrite()
        {
            var testdata = new Modifier("*.2.0");
            var testVersion = new ProjectVersion("5.1.1-RC12-beta1+build47");

            var testResult = testdata.Overwrite(testVersion, true);
            Assert.True(testResult.Equals("5.2.0"));
        }

        [Fact]
        public void TestOverwriteAddSemver()
        {
            var testdata = new Modifier("*.2.0-SNAPSHOT1");
            var testVersion = new ProjectVersion("5.1.1-RC12-beta1+build47");

            var testResult = testdata.Overwrite(testVersion, true);
            Assert.True(testResult.Equals("5.2.0-SNAPSHOT1"));
        }
    }
}
