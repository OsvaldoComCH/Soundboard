using NAudio.Wave;
using Soundboard.InputReader;
using Soundboard.SoundEffects;

namespace Soundboard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyboardHookManager.StartHook();
        }

        public static void PlaySound()
        {
            var sfx = new SoundEffectPlayer("Bad To The Bone.mp3");

            sfx.PlayAsync()
                .ContinueWith((x) =>
                {
                    sfx.Dispose();
                });
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            PlaySound();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Z:
                    PlayButton_Click((object)sender, e);
                    break;
            }
        }

        ~Form1()
        {
            KeyboardHookManager.StopHook();
        }
    }
}
