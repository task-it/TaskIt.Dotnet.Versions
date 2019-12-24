namespace TaskIt.Dotnet.Versions.Types
{
    /// <summary>
    /// Result indicating errors / Messages
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Error / Message code
        /// </summary>
        public EExitCode Code { get; set; }

        /// <summary>
        /// Error / Message text
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Construction
        /// </summary>
        public Result()
        {

        }

        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public Result(EExitCode code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
