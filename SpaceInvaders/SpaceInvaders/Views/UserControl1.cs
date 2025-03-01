using SpaceInvaders.Entities;
using SpaceInvaders.Utils;
using SpaceInvaders.ViewModel;
using SpaceInvaders.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SpaceInvaders;

public partial class UserControl1 : UserControl
{
    private GameViewModel gameViewModel;
    private Form1 parentForm;  


    public UserControl1(Form1 form)
    {
        InitializeComponent();
        DoubleBuffered = true;
        parentForm = form;  

        gameViewModel = new GameViewModel();
        gameViewModel.OnGameUpdated += () => Invalidate();
        UpdateHealthDisplay(3); 
        gameViewModel.OnHealthUpdated += UpdateHealthDisplay;
        gameViewModel.OnScoreUpdated += UpdateScoreDisplay;


        gameViewModel.OnGameOver += HandleGameOver;
        gameViewModel.OnGameWon += HandleGameWon;
    }



    private void UpdateHealthDisplay(int health)
    {
        if (parentForm == null) return; 

        parentForm.panel3.Controls.Clear();


        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "heart.png");

        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException($"No se encontró la imagen en la ruta: {imagePath}");
        }

        var originalImage = Image.FromFile(imagePath);
        var shipIcon = new Bitmap(originalImage, new Size(32, 32));

        for (int i = 0; i < health; i++)
        {
            var pictureBox = new PictureBox
            {
                Image = shipIcon,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(i * 35, 0)
            };
            parentForm.panel3.Controls.Add(pictureBox);
            parentForm.panel3.Refresh();
        }
    }

    private void UpdateScoreDisplay(int score)
    {
        if (parentForm == null) return;

        parentForm.label4.Text = $"{score}";
    }





    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        gameViewModel.Player.Draw(e.Graphics);
        gameViewModel.Invaders.Draw(e.Graphics);
        gameViewModel.Barriers.Draw(e.Graphics);
    }

    public void HandleKeyDown(KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:
                gameViewModel.Player.MoveLeft();
                break;
            case Keys.Right:
                gameViewModel.Player.MoveRight();
                break;
            case Keys.D1:
                gameViewModel.Player.Shoot();
                break;
        }
    }

    public void HandleKeyUp(KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:
                gameViewModel.Player.StopMovingLeft();
                break;
            case Keys.Right:
                gameViewModel.Player.StopMovingRight();
                break;
        }
    }

    private void HandleGameOver()
    {
        ScoreManager.SaveScore(gameViewModel.Player.PlayerShip.Credits);
        gameViewModel.StopGame();

        if (parentForm == null) return;

        lose loseScreen = new(this.parentForm);
        parentForm.panel2.Controls.Clear();
        loseScreen.Dock = DockStyle.Fill;
        parentForm.panel2.Controls.Add(loseScreen);
        parentForm.label3.Visible = false;
        parentForm.label4.Visible = false;
        parentForm.label5.Visible = false;
        parentForm.panel3.Visible = false;
        loseScreen.BringToFront();
    }

    private void HandleGameWon()
    {
        ScoreManager.SaveScore(gameViewModel.Player.PlayerShip.Credits);
        gameViewModel.StopGame();
        if (parentForm == null) return;

        Win winScreen = new(this.parentForm);
        parentForm.panel2.Controls.Clear();
        winScreen.Dock = DockStyle.Fill;
        parentForm.panel2.Controls.Add(winScreen);
        parentForm.label3.Visible = false;
        parentForm.label4.Visible = false;
        parentForm.label5.Visible = false;
        parentForm.panel3.Visible = false;
        winScreen.BringToFront();
    }
   

    public void UserControl1_Load(object sender, EventArgs e)
    {
        Focus();
    }
}
   


