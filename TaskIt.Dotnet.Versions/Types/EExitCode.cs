using System.Diagnostics.CodeAnalysis;

namespace TaskIt.Dotnet.Versions.Types
{
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    public enum EExitCode
    {
        SUCCESS = 0,
        INVALID_PARAMS = 1,
        INVALID_FILE = 2,
        UPLOAD_ERROR = 3
    }
}
