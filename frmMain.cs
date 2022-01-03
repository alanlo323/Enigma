using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Enigma
{
    public partial class frmMain : Form
    {
        public const string FILE_EXT = "eni";

        private EnigmaMachine enigma;

        private bool enigmaRuning = false;

        public frmMain()
        {
            InitializeComponent();
            enigma = EnigmaMachine.CreateDefault();
        }

        public bool LoadFromFile(string path, out string errorMessage)
        {
            try
            {
                string json = File.ReadAllText(path);

                EnigmaMachine loadedMachine = (EnigmaMachine)json.ToObject(typeof(EnigmaMachine));
                loadedMachine.Resolve();

                this.enigma = loadedMachine;

                RefreshRingStatus();

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public void RefreshRingStatus()
        {
            numericUpDownRing1.Minimum = -1;
            numericUpDownRing1.Maximum = enigma.GetRingConnectorsCount(0);
            numericUpDownRing1.Value = enigma.GetRingDegree(0);

            numericUpDownRing2.Minimum = -1;
            numericUpDownRing2.Maximum = enigma.GetRingConnectorsCount(1);
            numericUpDownRing2.Value = enigma.GetRingDegree(1);

            numericUpDownRing3.Minimum = -1;
            numericUpDownRing3.Maximum = enigma.GetRingConnectorsCount(2);
            numericUpDownRing3.Value = enigma.GetRingDegree(2);
        }

        public bool SaveToFile(string path, out string errorMessage)
        {
            try
            {
                string json = enigma.ToJSON();

                File.WriteAllText(path, json);

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog()
            {
                AddExtension = true,
                ValidateNames = true,
                CheckFileExists = true,
                CheckPathExists = true,
                ReadOnlyChecked = true,
                Title = "Load from file",
                FileName = $"enigma.{FILE_EXT}",
                DefaultExt = $".{FILE_EXT}",
                Filter = $"Enigma Machine File|*.{FILE_EXT}",
            };

            switch (load.ShowDialog())
            {
                case DialogResult.OK:
                    bool loadResult = LoadFromFile(load.FileName, out string errorMessage);
                    if (!loadResult)
                    {
                        MessageBox.Show(errorMessage);
                    }
                    else
                    {
                        textBoxOutput.Text = string.Empty;
                    }
                    break;

                default:
                    break;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = string.Empty;
            enigma.Reset();
            RefreshRingStatus();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                AddExtension = true,
                ValidateNames = true,
                OverwritePrompt = true,
                Title = "Save to file",
                FileName = $"enigma.{FILE_EXT}",
                DefaultExt = $".{FILE_EXT}",
                Filter = $"Enigma Machine File|*.{FILE_EXT}",
            };

            switch (save.ShowDialog())
            {
                case DialogResult.OK:
                    bool saveResult = SaveToFile(save.FileName, out string errorMessage);
                    if (!saveResult) MessageBox.Show(errorMessage);
                    break;

                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshRingStatus();
        }

        private void numericUpDownRing1_ValueChanged(object sender, EventArgs e)
        {
            enigma.UpdateRingDegree(0, (int)numericUpDownRing1.Value);
            RefreshRingStatus();
        }

        private void numericUpDownRing2_ValueChanged(object sender, EventArgs e)
        {
            enigma.UpdateRingDegree(1, (int)numericUpDownRing2.Value);
            RefreshRingStatus();
        }

        private void numericUpDownRing3_ValueChanged(object sender, EventArgs e)
        {
            enigma.UpdateRingDegree(2, (int)numericUpDownRing3.Value);
            RefreshRingStatus();
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            if (enigmaRuning) return;
            enigmaRuning = true;

            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in ((TextBox)sender).Text)
            {
                try
                {
                    var outsd = enigma.GetOutput(c);
                    stringBuilder.Append(outsd);
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.ToString());
                }
            }

            textBoxOutput.Text += stringBuilder.ToString();
            ((TextBox)sender).Text = string.Empty;

            RefreshRingStatus();

            enigmaRuning = false;
        }
    }
}