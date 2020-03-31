using System;
namespace LibFvadNet.Managed
{
    public class FvadBuilder
    {
        private readonly Constants.FvadModeAggressiveness _mode;
        private readonly int _sampleRate;

        private FvadBuilder(Constants.FvadModeAggressiveness mode, int sampleRate)
            => (_mode, _sampleRate) = (mode, sampleRate);

        public static FvadBuilder Create()
            => new FvadBuilder(Constants.FvadModeAggressiveness.Quality, default);

        public FvadBuilder SetMode(Constants.FvadModeAggressiveness mode)
            => new FvadBuilder(mode, _sampleRate);

        public FvadBuilder SetSampleRate(int sampleRate)
            => new FvadBuilder(_mode, sampleRate);

        public FvadCore Build()
        {
            var core = new FvadCore();
            core.SetMode(_mode);
            core.SetSampleRate(_sampleRate);
            return core;
        }
    }
}
