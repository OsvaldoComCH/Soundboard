using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.SoundEffects
{
    public class SoundEffectPlayer : IDisposable
    {
        protected ICollection<SoundEffect> SoundEffects = [];

        public SoundEffectPlayer(string filePath)
        {
            SoundEffects.Add(new SoundEffect(filePath));
        }

        public SoundEffectPlayer(string filePath, int[] devices)
        {
            foreach (var device in devices)
            {
                SoundEffects.Add(new SoundEffect(filePath, device));
            }
        }

        public Task PlayAsync()
        {
            return Task.Run(Play);
        }

        public void Play()
        {
            foreach (var soundEffect in SoundEffects)
            {
                soundEffect.Play();
            }

            while(SoundEffects.Any(x => x.PlaybackState == PlaybackState.Playing))
            {
                Thread.Sleep(500);
            }
        }

        public void Stop()
        {
            foreach (var soundEffect in SoundEffects)
            {
                soundEffect.Stop();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            foreach (var soundEffect in SoundEffects)
            {
                soundEffect.Dispose();
            }
            SoundEffects.Clear();
        }
    }
}
