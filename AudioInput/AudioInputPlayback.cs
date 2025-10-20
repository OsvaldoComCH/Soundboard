using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.AudioInput
{
    public class AudioInputPlayback : IDisposable
    {
        private VolumeSampleProvider Provider;
        private WaveInEvent Input;
        private WaveOutEvent Output;

        public AudioInputPlayback() : this(null, null) { }

        public AudioInputPlayback(int outputDevice) : this(null, outputDevice) { }

        public AudioInputPlayback(int? inputDevice, int? outputDevice)
        {
            Output = new();
            if (outputDevice != null)
            {
                Output.DeviceNumber = (int)outputDevice;
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

            Output.Init(Provider);
        }

        public void Start()
        {
            Output.Play();
            Input.StartRecording();
        }

        public void Stop()
        {
            Input.StopRecording();
            Output.Stop();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Input.Dispose();
            Output.Dispose();
        }
    }
}
