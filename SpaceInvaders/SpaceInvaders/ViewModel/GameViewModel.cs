using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.ViewModel;
    public class GameViewModel
    {
        private readonly System.Windows.Forms.Timer gameTimer;
        public PlayerViewModel Player { get; private set; }
        public InvadersViewModel Invaders { get; private set; }
        public BarriersViewModel Barriers { get; private set; }

        public event Action? OnGameUpdated;
        public event Action<int>? OnHealthUpdated;
        public event Action<int>? OnScoreUpdated;
        public event Action? OnGameOver;
        public event Action? OnGameWon;


        public GameViewModel()
        {
            Player = new PlayerViewModel();
            Invaders = new InvadersViewModel();
            Barriers = new BarriersViewModel();

            gameTimer = new System.Windows.Forms.Timer { Interval = 16 };
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
            Player.OnHealthChanged += (health) => OnHealthUpdated?.Invoke(health);

        }

        private void GameLoop(object? sender, EventArgs e)
        {
            Player.Update();
            Invaders.Update();
            CheckCollisions();
            OnGameUpdated?.Invoke();
        }



    private void CheckCollisions()
    {
        foreach (var ammo in Player.PlayerShip.AmmoList.ToList())
        {
            foreach (var invader in Invaders.invaders.ToList())
            {
                if (ammo.Bounds.IntersectsWith(invader.Bounds))
                {
                    SoundManager.PlaySound("collision.mp3");
                    invader.TakeDamage(Player.PlayerShip.Damage);
                    Player.PlayerShip.AmmoList.Remove(ammo);

                    if (invader.IsDestroyed)
                    {
                        Player.PlayerShip.Credits += invader.Score;
                        OnScoreUpdated?.Invoke(Player.PlayerShip.Credits);
                    }
                    break;
                }
            }
        }

        Invaders.RemoveDestroyedInvaders();

        if (!Invaders.invaders.Any())
        {
            Invaders.shootTimer.Stop();
            OnGameWon?.Invoke();
            return;
        }

        foreach (var invader in Invaders.invaders)
        {
            invader.UpdateAmmo();
        }

        foreach (var barrier in Barriers.Barriers.ToList())
        {
            foreach (var ammo in Player.PlayerShip.AmmoList.ToList())
            {
                if (ammo.Bounds.IntersectsWith(barrier.Bounds))
                {
                    SoundManager.PlaySound("collision.mp3");
                    barrier.TakeDamage(ammo.Damage);
                    Player.PlayerShip.AmmoList.Remove(ammo);
                    break;
                }
            }

            foreach (var invader in Invaders.invaders)
            {
                foreach (var ammo in invader.AmmoList.ToList())
                {
                    if (ammo.Bounds.IntersectsWith(barrier.Bounds))
                    {
                        SoundManager.PlaySound("collision.mp3");
                        barrier.TakeDamage(ammo.Damage);
                        invader.AmmoList.Remove(ammo);
                        break;
                    }
                }
            }
        }

        Barriers.RemoveDestroyedBarriers();

        foreach (var invader in Invaders.invaders)
        {
            foreach (var ammo in invader.AmmoList.ToList())
            {
                if (ammo.Bounds.IntersectsWith(Player.PlayerShip.Bounds))
                {
                    SoundManager.PlaySound("damage.mp3");
                    Player.PlayerShip.TakeDamage(ammo.Damage);
                    invader.AmmoList.Remove(ammo);
                    break;
                }
            }
        }

        if (Player.PlayerShip.IsDestroyed)
        {
            Invaders.shootTimer.Stop();
            OnGameOver?.Invoke();
        }
    }

    public void StopGame()
    {
        gameTimer.Stop();
    }



}

