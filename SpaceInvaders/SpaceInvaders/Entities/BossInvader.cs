using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Entities;
internal class BossInvader : Invader
{
    private static readonly Image bossInvaderImage;

    static BossInvader()
    {
        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "StrongInvader.png");

        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException($"No se encontró la imagen en la ruta: {imagePath}");
        }

        var originalImage = Image.FromFile(imagePath);
        bossInvaderImage = new Bitmap(originalImage, new Size(64, 64));
    }

    public BossInvader(int x, int y) : base(x, y, 64, 64)
    {
        Speed = 4;
        Health = 20;
        Damage = 3;
        Score = 100;
        Image = bossInvaderImage; 
    }
}
