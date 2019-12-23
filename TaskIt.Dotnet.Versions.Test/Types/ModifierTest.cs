using TaskIt.Dotnet.Versions.Types;
using Xunit;

namespace TaskIt.Dotnet.Versions.Test.Types
{
    public class ModifierTest
    {
        [Fact]
        public void TestConstruction()
        {
            var testdata = new Modifier("*.1.0");
            Assert.False(testdata.Major.HasValue);
            Assert.True(testdata.Minor.HasValue);
            Assert.True(testdata.Patch.HasValue);

        }

        [Fact]
        public void TestModify()
        {
            var testdata = new Modifier("*.2.0", @"beta(\d+)", 1);
            var testVersion = new ProjectVersion("5.1.1-RC12-beta1+build47");

            var testResult = testdata.Modify(testVersion, true);
            Assert.True(testResult.Equals("5.3.0-RC12-beta2+build47"));



        }
    }
}
