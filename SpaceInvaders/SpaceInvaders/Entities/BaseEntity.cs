using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Entities;

    public abstract class BaseEntity
    {
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; protected set; }
    public int Height { get; protected set; }
    public bool IsDestroyed { get; private set; } = false;

    public Rectangle Bounds => new Rectangle(X, Y, Width, Height);


    public BaseEntity(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    

    public void Destroy()
    {
        IsDestroyed = true;
    }
}

