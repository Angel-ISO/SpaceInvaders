using SpaceInvaders.Interface;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Entities;

public abstract class Ammo : BaseEntity, IDraw
{
    public int Damage { get; protected set; } 

    protected Image AmmoImage { get; set; }

    public int Speed { get; set; } = 10;

    protected Ammo(int x, int y, int width, int height) : base(x, y, width, height) { }

    public abstract bool Move();

    public bool IsOnScreen()
    {
        return Y >= 0 && Y <= BoundsChecker.PanelHeight && X >= 0 && X <= BoundsChecker.PanelWidth;
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(AmmoImage, X, Y);
    }
}



