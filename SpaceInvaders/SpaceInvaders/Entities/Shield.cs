using SpaceInvaders.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Entities;
public class Shield : BaseEntity, IDraw
{
    private static readonly Image barrierImage;
    public int Health = 4;

    static Shield()
    {
        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "asteroid.png");

        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException($"No se encontró la imagen en la ruta: {imagePath}");
        }

        var originalImage = Image.FromFile(imagePath);
        barrierImage = new Bitmap(originalImage, new Size(32, 32));
    }

    public Shield(int x, int y) : base(x, y, 32, 32) { }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy();
        }
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(barrierImage, X, Y, Width, Height);
    }
}


