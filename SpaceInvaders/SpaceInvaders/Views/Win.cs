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
    public partial class Win : UserControl
    {
        private Form1 parentForm;

        public Win(Form1 form)
        {
            InitializeComponent();
            parentForm = form;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            parentForm.GoToMainMenu();
        }
    }

