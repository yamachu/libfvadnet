using System;
using LibFvadNet.Native;
namespace LibFvadNet.Managed
{
    public class FvadCore : IDisposable
    {
        private IntPtr inst;

        internal FvadCore()
        {
            inst = NativeMethods.fvad_new();
            if (inst == IntPtr.Zero)
            {
                throw new Exception("Could not allocate fvad instance");
            }
        }

        internal void SetMode(Constants.FvadModeAggressiveness mode)
        {
            if (NativeMethods.fvad_set_mode(inst, Convert.ToInt32(mode)) != 0)
            {
                throw new ArgumentException($"mode must be contained in FvadModeAggressiveness");
            }
        }

        internal void SetSampleRate(int sampleRate)
        {
            if (NativeMethods.fvad_set_sample_rate(inst, sampleRate) != 0)
            {
                throw new ArgumentException($"sampleRate must be {string.Join(',', Constants.ValidSampleRate)}");
            }
        }

        public bool IsVoiceActive(in short[] frame)
        {
            // TODO: Can we call this method from multi-thread?
            var result = NativeMethods.fvad_process(inst, frame, frame.Length);
            if (result == -1)
            {
                throw new ArgumentException("Invalid length frames passed, it must be 10, 20 or 30msec");
            }
            return result == 1;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    NativeMethods.fvad_free(inst);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                inst = IntPtr.Zero;

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~FvadCore()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
