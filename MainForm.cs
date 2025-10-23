using NAudio.Wave;
using Soundboard.Playback;
using Soundboard.InputReader;
using Soundboard.SoundEffects;

namespace Soundboard
{
    public partial class MainForm : Form
    {
        private AudioInputPlayback Playback;

        public MainForm()
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
                    {
                        if (e.Alt)
                        {
                            PlayButton_Click(sender, e);
                        }
                    }
                    break;
            }
        }

        ~MainForm()
        {
            KeyboardHookManager.StopHook();
            Playback.Stop();
            Playback.Dispose();
        }
    }
}