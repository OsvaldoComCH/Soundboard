using NAudio.Wave;
using Soundboard.AudioInput;
using Soundboard.InputReader;
using Soundboard.SoundEffects;

namespace Soundboard
{
    public partial class Form1 : Form
    {
        private AudioInputPlayback Playback;

        public Form1()
        {
            InitializeComponent();
            KeyboardHookManager.StartHook();
            KeyboardInputHandler.OnKeyPress += InputEventHandler;
            Playback = new();
            Playback.Start();
        }

        public static void PlaySound()
        {
            //var sfx = new SoundEffectPlayer("Bad To The Bone.mp3");
            var sfx = new SoundEffectPlayer("Bone.mp3");

            sfx.PlayAsync()
                .ContinueWith((x) =>
                {
                    sfx.Dispose();
                });
        }

        private void PlayButton_Click(object? sender, EventArgs e)
        {
            PlaySound();
        }

        private void InputEventHandler(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Z:
                    PlayButton_Click(sender, e);
                    break;
            }
        }

        ~Form1()
        {
            KeyboardHookManager.StopHook();
            Playback.Stop();
            Playback.Dispose();
        }
    }
}