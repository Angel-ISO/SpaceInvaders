using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.Utils;

namespace SpaceInvaders.ViewModel;
public class PlayerViewModel
{
    public Ship PlayerShip { get; private set; }
    private bool movingLeft;
    private bool movingRight;


    public event Action<int>? OnHealthChanged;


   


    public PlayerViewModel()
    {
        int shipWidth = 32;
        int shipHeight = 32;

        int startX = (BoundsChecker.PanelWidth - shipWidth) / 2;
        int startY = BoundsChecker.PanelHeight - shipHeight - 10;

        PlayerShip = new Ship(startX, startY);
        PlayerShip.OnHealthChanged += (health) => OnHealthChanged?.Invoke(health); // Suscribe el evento
    }

    public void MoveLeft() => movingLeft = true;
    public void MoveRight() => movingRight = true;
    public void StopMovingLeft() => movingLeft = false;
    public void StopMovingRight() => movingRight = false;
    public void Shoot() => PlayerShip.Shoot();

    public void Update()
    {
        if (movingLeft)
            PlayerShip.MoveLeft();
        if (movingRight)
            PlayerShip.MoveRight();

        PlayerShip.X = BoundsChecker.CheckX(PlayerShip.X, PlayerShip.Width);

        PlayerShip.UpdateAmmo();
    }

    public void Draw(Graphics g)
    {
        PlayerShip.Draw(g);
        PlayerShip.DrawAmmo(g);
    }
}