namespace IdentitySecurityTokenService.Application.Common.Helpers
{
    public static class DurationUnitChecker
    {
        public static string DurationWithUnitCheck(int BlockDuration, char durationUnit)
        {
            switch (durationUnit)
            {
                case 'M':
                    return BlockDuration > 1 ? $"{BlockDuration} Months" : $"{BlockDuration} Month";
                case 'D':
                    return BlockDuration > 1 ? $"{BlockDuration} Days" : $"{BlockDuration} Day";

                default:
                    return "";
            }
        }
    }
}
