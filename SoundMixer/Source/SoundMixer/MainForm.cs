using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SoundMixer
{
    public partial class MainForm : Form
    {
        // [SoundMixer]        
        string soundMixerArchive = string.Empty;

        // [GameAudio]
        string doorSounds = string.Empty;
        string explosionSounds = string.Empty;
        string hostageSounds = string.Empty;
        string playerSounds = string.Empty;
        string sprayerSounds = string.Empty;
        string weaponSounds = string.Empty;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private int GetRandom(int max)
        {
            Random random = new Random();
            int seconds = System.DateTime.Now.Second;
            int rnd = 0;

            for (int i = 0; i < seconds; i++)
            {
                rnd = random.Next(max);
            }

            return rnd;
        }

        private string ReadConfig(string key)
        {
            string line = string.Empty;
            string match = string.Empty;
            int count = 0;

            string config = System.IO.Path.Combine(Application.StartupPath, "SoundMixer.ini");

            System.IO.StreamReader file = new System.IO.StreamReader(config);
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith(";"))
                {
                    continue;
                }
                else if (line.ToLower().StartsWith(key.ToLower() + "="))
                {
                    match = line.ToLower().Replace(key.ToLower() + "=", string.Empty);
                    count += 1;
                }
            }
            file.Close();

            if (count == 0)
            {
                MessageBox.Show("No config entry for \"" + key + "\"!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else if (count > 1)
            {
                MessageBox.Show("Too many config entries for \"" + key + "\"!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
            return match;    
        }
        
        private void Doors(int max)
        {
            int num = 1;
            string path = soundMixerArchive + @"\doors";
            string[] allFiles = Directory.GetFiles(path);

            if (allFiles.Length < max)
            {
                MessageBox.Show("Not enough sound files for \"" + path + "\"!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            while (true)
            {
                int rnd = GetRandom(allFiles.Length);
                string file = allFiles[rnd];
                allFiles[rnd] = null;

                if (file != null)
                {
                    File.Copy(file, doorSounds + @"\doormove" + num.ToString() + ".wav", true);
                    break;
                }
            }
        }

        private void Explosions(int max)
        {
            int num = 3;
            string path = soundMixerArchive + @"\explode";
            string[] allFiles = Directory.GetFiles(path);
            List<string> rndFiles = new List<string>();

            if (allFiles.Length < max)
            {
                MessageBox.Show("Not enough sound files for \"" + path + "\"!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            while (rndFiles.Count < max)
            {
                int rnd = GetRandom(allFiles.Length);
                string file = allFiles[rnd];
                allFiles[rnd] = null;

                if (file != null)
                {
                    rndFiles.Add(file);
                }
            }

            foreach (string s in rndFiles)
            {
                File.Copy(s, explosionSounds + @"\explode" + num.ToString() + ".wav", true);
                num++;
            }
        }

        private void Hostage(int max)
        {
            int num = 1;
            string path = soundMixerArchive + @"\hostage";
            string[] allFiles = Directory.GetFiles(path);
            List<string> rndFiles = new List<string>();

            if (allFiles.Length < max)
            {
                MessageBox.Show("Not enough sound files for \"" + path +"\"!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            while (rndFiles.Count < max)
            {
                int rnd = GetRandom(allFiles.Length);
                string file = allFiles[rnd];
                allFiles[rnd] = null;

                if (file != null)
                {
                    rndFiles.Add(file);
                }
            }
            
            foreach (string s in rndFiles)
            {
                File.Copy(s, hostageSounds + @"\hos" + num.ToString() + ".wav", true);
                num++;
            }
        }

        private void Player()
        {
            string[] subDirs = Directory.GetDirectories(soundMixerArchive + @"\player");

            foreach (string dir in subDirs)
            {
                int max = Convert.ToInt32(dir.Substring(dir.Length - 1));
                string[] temp = dir.Split(Convert.ToChar(@"\"));
                string fileName = temp[temp.Length - 1];
                temp = fileName.Split(Convert.ToChar("."));
                string fileNamePre = temp[0];

                int num = 1;
                string path = dir;
                string[] allFiles = Directory.GetFiles(path);
                List<string> rndFiles = new List<string>();

                if (allFiles.Length < max)
                {
                    MessageBox.Show("Not enough sound files for \"" + dir + "\"!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

                while (rndFiles.Count < max)
                {
                    int rnd = GetRandom(allFiles.Length);
                    string file = allFiles[rnd];
                    allFiles[rnd] = null;

                    if (file != null)
                    {
                        rndFiles.Add(file);
                    }
                }

                foreach (string s in rndFiles)
                {
                    File.Copy(s, playerSounds + @"\" + fileNamePre + num.ToString() + ".wav", true);
                    num++;
                }
            }
        }

        private void Sprayer()
        {
            int num = 1;
            string path = soundMixerArchive + @"\sprayer";
            string[] allFiles = Directory.GetFiles(path);
            List<string> rndFiles = new List<string>();

            if (allFiles.Length < 1)
            {
                MessageBox.Show("Not enough sound files for \"" + path + "\"!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            while (rndFiles.Count < 1)
            {
                int rnd = GetRandom(allFiles.Length);
                string file = allFiles[rnd];
                allFiles[rnd] = null;

                if (file != null)
                {
                    rndFiles.Add(file);
                }
            }

            foreach (string s in rndFiles)
            {
                File.Copy(s, sprayerSounds + @"\sprayer.wav", true);
                num++;
            }
        }

        private void Weapons()
        {
            string[] subDirs = Directory.GetDirectories(soundMixerArchive + @"\weapons");

            foreach (string dir in subDirs)
            {
                int max = Convert.ToInt32(dir.Substring(dir.Length - 1));
                string[] temp = dir.Split(Convert.ToChar(@"\"));
                string fileName = temp[temp.Length - 1];
                temp = fileName.Split(Convert.ToChar("."));
                string fileNamePre = temp[0];

                int num = 1;
                string path = dir;
                string[] allFiles = Directory.GetFiles(path);
                List<string> rndFiles = new List<string>();

                if (allFiles.Length < max)
                {
                    MessageBox.Show("Not enough sound files for \"" + dir + "\"!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

                while (rndFiles.Count < max)
                {
                    int rnd = GetRandom(allFiles.Length);
                    string file = allFiles[rnd];
                    allFiles[rnd] = null;

                    if (file != null)
                    {
                        rndFiles.Add(file);
                    }
                }

                foreach (string s in rndFiles)
                {
                    File.Copy(s, weaponSounds + @"\" + fileNamePre + num.ToString() + ".wav", true);
                    num++;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Show();

            soundMixerArchive = ReadConfig("SoundMixerArchive");

            doorSounds = ReadConfig("Doors");
            explosionSounds = ReadConfig("Explosions");
            hostageSounds = ReadConfig("Hostages");
            playerSounds = ReadConfig("Players");
            sprayerSounds = ReadConfig("Sprayer");
            weaponSounds = ReadConfig("Weapons");

            Doors(10);
            Explosions(3);
            Hostage(6);
            Player();
            Sprayer();
            Weapons();

            Process.Start("Counter-Strike.cmd");
            Application.Exit();
        }
    }
}
