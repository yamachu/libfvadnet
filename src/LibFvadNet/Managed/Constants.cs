using System;
namespace LibFvadNet.Managed
{
    public static class Constants
    {
        public enum FvadModeAggressiveness
        {
            Quality = 0,
            LowBitrate,
            Aggressive,
            VeryAggressive
        }

        public readonly static int[] ValidSampleRate = new[] { 8_000, 16_000, 32_000, 48_000 };
    }
}
