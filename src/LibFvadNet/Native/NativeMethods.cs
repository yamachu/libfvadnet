using System;
using System.Runtime.InteropServices;

namespace LibFvadNet.Native
{
    internal static class NativeMethods
    {
        [DllImport("fvad", CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr fvad_new();

        [DllImport("fvad", CallingConvention = CallingConvention.Cdecl)]
        internal extern static void fvad_free(IntPtr inst);

        [DllImport("fvad", CallingConvention = CallingConvention.Cdecl)]
        internal extern static void fvad_reset(IntPtr inst);

        [DllImport("fvad", CallingConvention = CallingConvention.Cdecl)]
        internal extern static int fvad_set_mode(IntPtr inst, int mode);

        [DllImport("fvad", CallingConvention = CallingConvention.Cdecl)]
        internal extern static int fvad_set_sample_rate(IntPtr inst, int sampleRate);

        [DllImport("fvad", CallingConvention = CallingConvention.Cdecl)]
        internal extern static int fvad_process(IntPtr inst, [MarshalAs(UnmanagedType.LPArray)]short[] frame, int length);
    }
}
