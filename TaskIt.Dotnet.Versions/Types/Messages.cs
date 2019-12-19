using System.Diagnostics.CodeAnalysis;

namespace TaskIt.Dotnet.Versions.Types
{
    /// <summary>
    /// Message Texts
    /// </summary>
    ///     
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    public static class Messages
    {
        public static readonly string MSG_UPLOAD = "Uploading...";
        public static readonly string MSG_UPLOAD_Ok = " - Ok";
        public static readonly string MSG_UPLOAD_ERROR = " - ERROR: ";
        public static readonly string MSG_ROLLBACK = "Removing...";
        public static readonly string MSG_ROLLBACK_OK = "Removing...";

        public static readonly string ERR_UPLOAD_FAILED = "ERROR - Upload Failed";
        public static readonly string ERR_PARAMS = "ERROR - Invalid Parameters";
        public static readonly string ERR_FOLDER = "ERROR - Invalid Folder";

        public static readonly string MSG_ROLLBACK_FINISH = "Finished Removing Files";
    }
}
