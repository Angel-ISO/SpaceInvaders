using SpaceInvaders.Utils;
using SpaceInvaders.ViewModel;
using SpaceInvaders.Views;
using System.Security.Cryptography;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            SoundManager.PlaySoundLoop("pixel-song.mp3");
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAndDisplayScores();
        }

        private void LoadAndDisplayScores()
        {
            panel4.Controls.Clear();

            Label headerLabel = new Label
            {
                Text = "Tus máximas marcas :)",
                AutoSize = true,
                Location = new System.Drawing.Point(10, 10),
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White
            };

            panel4.Controls.Add(headerLabel);

            List<int> scores = ScoreManager.LoadScores();

            int yOffset = 50;
            foreach (int score in scores)
            {
                Label scoreLabel = new Label
                {
                    Text = $"{score}",
                    AutoSize = true,
                    Location = new System.Drawing.Point(10, yOffset),
                    Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.White
                };

                panel4.Controls.Add(scoreLabel);
                yOffset += 30;
            }
        }







        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Estás seguro de que deseas cerrar el juego?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            UserControl1 control = new UserControl1(this);
            panel2.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panel2.Controls.Add(control);
            control.BringToFront();
            control.Focus();
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (panel2.Controls.Count > 0 && panel2.Controls[0] is UserControl1 userControl)
            {
                userControl.HandleKeyDown(new KeyEventArgs(keyData));
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (panel2.Controls.Count > 0 && panel2.Controls[0] is UserControl1 userControl)
            {
                userControl.HandleKeyUp(e);
            }

            base.OnKeyUp(e);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        public void GoToMainMenu()
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(pictureBox3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label6);


            pictureBox3.Visible = true;
            label2.Visible = true;
            label6.Visible = true;
            panel4.Visible = true;

            panel4.Controls.Clear(); 
            LoadAndDisplayScores();
            panel4.Refresh(); 
        }

       

        private void label6_Click(object sender, EventArgs e)
        {
            Credits control = new(this);
            panel2.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panel2.Controls.Add(control);
            control.BringToFront();
            control.Focus();
        }
    }
}
