using SpaceInvaders.Entities;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.ViewModel;

public class InvadersViewModel
{

        public List<Invader> invaders;
        private int direction = 1;
        private Random random = new Random();
        private DateTime lastFireTime = DateTime.MinValue; 
        public System.Windows.Forms.Timer shootTimer; 


        public InvadersViewModel()
        {
            invaders = new List<Invader>();
            InitializeInvaders();

           
            FireRandomInvader();

            
            shootTimer = new System.Windows.Forms.Timer();
            shootTimer.Interval = 3000; 
            shootTimer.Tick += (sender, e) => FireRandomInvader(); 
            shootTimer.Start(); 
        }

        private void InitializeInvaders()
        {
            int spacing = 60;
            int invaderWidth = 32;

            int basicInvaderCount = 6;
            int basicInvaderRowCount = 2;
            int basicInvaderTotalWidth = (basicInvaderCount - 1) * spacing + basicInvaderCount * invaderWidth;
            int basicInvaderStartX = (BoundsChecker.PanelWidth - basicInvaderTotalWidth) / 2;

            for (int i = 0; i < basicInvaderRowCount; i++)
            {
                for (int j = 0; j < basicInvaderCount; j++)
                {
                    int x = basicInvaderStartX + j * (invaderWidth + spacing);
                    int y = BoundsChecker.PanelHeight - 180 + i * 40;
                    invaders.Add(new BasicInvader(x, y));
                }
            }

            int mediumInvaderCount = 7;
            int mediumInvaderRowCount = 2;
            int mediumInvaderTotalWidth = (mediumInvaderCount - 1) * spacing + mediumInvaderCount * invaderWidth;
            int mediumInvaderStartX = (BoundsChecker.PanelWidth - mediumInvaderTotalWidth) / 2;

            for (int i = 0; i < mediumInvaderRowCount; i++)
            {
                for (int j = 0; j < mediumInvaderCount; j++)
                {
                    int x = mediumInvaderStartX + j * (invaderWidth + spacing);
                    int y = BoundsChecker.PanelHeight - 280 + i * 40;
                    invaders.Add(new MediumInvader(x, y));
                }
            }

            int bossInvaderWidth = 64;
            int bossInvaderStartX = (BoundsChecker.PanelWidth - bossInvaderWidth) / 2;
            invaders.Add(new BossInvader(bossInvaderStartX, 50));
        }

        public void Update()
        {
            bool shouldInvert = false;

            foreach (var invader in invaders)
            {
                if (direction == 1)
                    invader.MoveRight();
                else
                    invader.MoveLeft();

                if (BoundsChecker.ShouldInvertDirection(invader.X, invader.Width))
                    shouldInvert = true;

                invader.UpdateAmmo();
            }

            if (shouldInvert)
                direction *= -1;
        }

    private void FireRandomInvader()
    {
        var invaderToShoot = invaders.OrderBy(x => random.Next()).FirstOrDefault();

        if (invaderToShoot != null)
        {
            Console.WriteLine($"Invasor en X:{invaderToShoot.X}, Y:{invaderToShoot.Y} disparó");
            invaderToShoot.Shoot();
        }
    }

    public void Draw(Graphics g)
        {
            foreach (var invader in invaders)
            {
                invader.Draw(g);
                invader.DrawAmmo(g);
            }
        }

    public void RemoveDestroyedInvaders()
    {
        invaders.RemoveAll(invader => invader.IsDestroyed);
    }

}



