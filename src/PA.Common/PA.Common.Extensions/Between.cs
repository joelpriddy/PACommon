namespace PA.Common.Extensions
{
    public static partial class DateTimeExtensions
    {
        public static bool Between(this DateTime dt, DateTime lowerBound, DateTime upperBound, bool inclusive = true)
        {
            if (inclusive)
            {
                return (dt >= lowerBound && dt <= upperBound);
            }

            return (dt > lowerBound && dt < upperBound);
        }
    }
}
