namespace ClinicPro.Core.Common
{
    public class RateLimitExceededException : Exception
    {

        public RateLimitExceededException(string message) : base(message)
        {
        }

    }
}
