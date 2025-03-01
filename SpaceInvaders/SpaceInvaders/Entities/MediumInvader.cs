using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Entities;
internal class MediumInvader : Invader
{
    private static readonly Image mediumInvaderImage;

    static MediumInvader()
    {
        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "MiddleInvader.png");

        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException($"No se encontró la imagen en la ruta: {imagePath}");
        }

        var originalImage = Image.FromFile(imagePath);
        mediumInvaderImage = new Bitmap(originalImage, new Size(32, 32));
    }

    public MediumInvader(int x, int y) : base(x, y, 32, 32)
    {
        Speed = 3;
        Health = 3;
        Damage = 2;
        Score = 40;
        Image = mediumInvaderImage; 
    }
}

