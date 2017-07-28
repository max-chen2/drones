using System;

namespace Drone
{
    public class DeadlineUtil
    {
        public static bool CanDeliverBeforeDeadline(double totalDistance, double deadline, double speedKmPerHour)
        {
            double metresPerSecond = speedKmPerHour * 1000.0 / 60.0 / 60.0;
            double nowSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
            var duration = totalDistance / metresPerSecond;

            double etaSeconds = nowSeconds + duration;
            var can = etaSeconds <= deadline;
            return can;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
    }
}
