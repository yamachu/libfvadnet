using System;
using System.Linq;
using LibFvadNet.Managed;
using NAudio.Wave;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: REPLACE IT
            var readWavFilePath = "";
            var writeWavFilePath = "";

            using var waveReader = new WaveFileReader(readWavFilePath);
            if (waveReader.WaveFormat.Channels != 1)
            {
                Console.WriteLine("Support 1 channel wav only");
                return;
            }

            var samples = new float[waveReader.Length / waveReader.BlockAlign];
            for (var i = 0; i < samples.Length; i++)
            {
                samples[i] = waveReader.ReadNextSampleFrame()[0];
            }

            using var fvadCore = FvadBuilder.Create()
                .SetMode(Constants.FvadModeAggressiveness.Quality)
                .SetSampleRate(waveReader.WaveFormat.SampleRate)
                .Build();

            // per 10msec
            var samplePerMsec = waveReader.WaveFormat.SampleRate / 1000 * 10;
            var result = Enumerable.Range(0, (int)Math.Floor(samples.Length / samplePerMsec * 1f))
                .Select(i =>
                {
                    var shortSample = samples.AsSpan(samplePerMsec * i, samplePerMsec)
                    .ToArray()
                    .Select(v => (short)(v * short.MaxValue))
                    .ToArray();
                    return (index: i, isActive: fvadCore.IsVoiceActive(shortSample));
                });

            foreach (var (i, active) in result)
            {
                Console.WriteLine($"index: {i}, active: {active}");
            }

            var activeSamples = result.Where(v => v.isActive)
                .SelectMany(v => samples.AsSpan(samplePerMsec * v.index, samplePerMsec).ToArray())
                .ToArray();

            using var waveWriter = new WaveFileWriter(writeWavFilePath, new WaveFormat(
                waveReader.WaveFormat.SampleRate,
                waveReader.WaveFormat.BitsPerSample,
                waveReader.WaveFormat.Channels));
            waveWriter.WriteSamples(activeSamples, 0, activeSamples.Length);
        }
    }
}
