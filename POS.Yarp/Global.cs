namespace POS.Yarp
{
    internal class Global
    {
        public static bool IsOriginAllowed(string origin)
        {
            Uri uri = new(origin);
            _ = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "n/a";

            bool isAllowed = uri.Host.Equals("admin.quickfoodmm.com", StringComparison.OrdinalIgnoreCase)
                            || uri.Host.Equals("analytics.quickfoodmm.com", StringComparison.OrdinalIgnoreCase);
            //if (!isAllowed && env.Contains("DEV", StringComparison.OrdinalIgnoreCase))
            //    isAllowed = uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

            if (!isAllowed)
                isAllowed = uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

            return isAllowed;
        }
    }
}
