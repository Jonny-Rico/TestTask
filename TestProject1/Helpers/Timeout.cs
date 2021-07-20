namespace TestProject1.Helpers
{
    /// <summary>
    /// Class for implementing timeouts
    /// </summary>
    public class Timeout
    {
        public static readonly int WaitFact = 1;
        public static int Zero = 0;
        public static int OneSec => decimal.ToInt32(1) * WaitFact;
        public static int ThreeSec => decimal.ToInt32(3) * WaitFact;
        public static int FiveSec => decimal.ToInt32(5) * WaitFact;
        public static int TenSec => decimal.ToInt32(10) * WaitFact;
        public static int TwentySec => decimal.ToInt32(20) * WaitFact;
        public static int ThirtySec => decimal.ToInt32(30) * WaitFact;
        public static int OneMin => decimal.ToInt32(60) * WaitFact;
        public static int ThreeMin => decimal.ToInt32(180) * WaitFact;
    }
}
