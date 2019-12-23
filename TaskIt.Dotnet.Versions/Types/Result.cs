namespace TaskIt.Dotnet.Versions.Types
{
    public class Result
    {
        public EExitCode Code { get; set; }

        public string Message { get; set; }

        public Result()
        {

        }

        public Result(EExitCode code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
