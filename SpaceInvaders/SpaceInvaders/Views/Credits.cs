using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders.Views;
public partial class Credits : UserControl
{
    private System.Windows.Forms.Timer scrollTimer; 
    private Label creditsLabel; 
    private int scrollSpeed = 2;
    private Form1 parentForm;

    public Credits(Form1 form)
    {
        InitializeComponent();
        parentForm = form;
        SetupCredits();
        AddReturnButton();
    }


    private void AddReturnButton()
    {
        Button returnButton = new Button
        {
            Text = "Volver al menú",
            Size = new Size(150, 40),
            Location = new Point((this.Width / 2) - 75, this.Height - 60),
            BackColor = Color.Black,
            ForeColor = Color.White
        };

        returnButton.Click += ReturnButton_Click;
        this.Controls.Add(returnButton);
    }

    private void ReturnButton_Click(object sender, EventArgs e)
    {
        scrollTimer.Stop();
        parentForm.GoToMainMenu();
    }




    private void SetupCredits()
    {
        creditsLabel = new Label
        {
            AutoSize = true,
            Text = @"
                    Gracias por jugar Space Invaders!

                    Desarrollado por Angel Gabriel Ortega
                    Experiencia: 1 año desarrollando enterprise applications  
                   

                    Proyecto final del curso programacion 2
                    Profesor: juan zalbidumble
                    Practicioner: alvaro quispe
                    Espero que les guste el juego :).
                ",
            Font = new Font("Arial", 14, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Location = new Point(50, Height) 
        };

        this.Controls.Add(creditsLabel);

        scrollTimer = new System.Windows.Forms.Timer
        {
            Interval = 50 
        };
        scrollTimer.Tick += ScrollTimer_Tick;

        scrollTimer.Start();
    }

    private void ScrollTimer_Tick(object sender, EventArgs e)
    {
        creditsLabel.Top -= scrollSpeed;

        if (creditsLabel.Bottom < 0)
        {
            scrollTimer.Stop();
            MessageBox.Show("Gracias por jugar! :)");
        }
    }

    private void Credits_Load(object sender, EventArgs e)
    {
        this.BackColor = Color.Black;
    }
}

