using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.SoundEffects
{
    public class SoundEffect : IDisposable
    {
        private readonly WaveOutEvent waveOut;
        private readonly AudioFileReader fileReader;

        public int DeviceNumber
        {
            get => waveOut.DeviceNumber;
            set => waveOut.DeviceNumber = value;
        }

        public PlaybackState PlaybackState => waveOut.PlaybackState;

        public SoundEffect(string filePath)
        {
            this.fileReader = new AudioFileReader(filePath);
            this.waveOut = new ();
            waveOut.Init(fileReader);
        }

        public SoundEffect(string filePath, int device)
        {
            this.fileReader = new AudioFileReader(filePath);
            this.waveOut = new ();
            waveOut.Init(fileReader);
            waveOut.DeviceNumber = device;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            waveOut.Dispose();
            fileReader.Dispose();
        }

        public void Play()
        {
            waveOut.Play();
        }

        public void Stop()
        {
            waveOut.Stop();
        }
    }
}
