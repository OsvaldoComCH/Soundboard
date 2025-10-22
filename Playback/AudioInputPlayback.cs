using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.Playback
{
    public class AudioInputPlayback : IDisposable
    {
        private readonly VolumeSampleProvider Provider;
        private readonly WaveInEvent Input;
        private readonly List<WaveOutEvent> Outputs = [];

        public AudioInputPlayback() : this(null, []) { }

        public AudioInputPlayback(int[] outputDevices) : this(null, outputDevices) { }

        public AudioInputPlayback(int? inputDevice, int[] outputDevices)
        {
            if (outputDevices.Length > 0)
            {
                foreach (var device in outputDevices)
                {
                    WaveOutEvent output = new();
                    output.DeviceNumber = (int)device;
                    Outputs.Add(output);
                }
            }
            else
            {
                WaveOutEvent output = new();
                Outputs.Add(output);
            }

            Input = new();
            if(inputDevice != null)
            {
                Input.DeviceNumber = (int)inputDevice;
            }

            var waveProvider = new BufferedWaveProvider(Input.WaveFormat);

            Input.DataAvailable += (sender, args) =>
            {
                waveProvider.AddSamples(args.Buffer, 0, args.BytesRecorded);
            };

            Provider = new(waveProvider.ToSampleProvider());
            Provider.Volume = 2.0f;

            foreach (var output in Outputs)
            {
                output.Init(Provider);
            }
        }

        public void Start()
        {
            foreach (var output in Outputs)
            {
                output.Play();
            }
            Input.StartRecording();
        }

        public void Stop()
        {
            Input.StopRecording();
            foreach (var output in Outputs)
            {
                output.Stop();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Input.Dispose();
            foreach (var output in Outputs)
            {
                output.Dispose();
            }
        }
    }
}
