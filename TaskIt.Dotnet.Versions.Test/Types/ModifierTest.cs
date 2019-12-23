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
    }
}
