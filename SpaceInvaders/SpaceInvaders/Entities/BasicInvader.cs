using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Entities;

internal class BasicInvader : Invader
{
    private static readonly Image basicInvaderImage;

    static BasicInvader()
    {
        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "BasicInvader.png");

        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException($"No se encontró la imagen en la ruta: {imagePath}");
        }

        var originalImage = Image.FromFile(imagePath);
        basicInvaderImage = new Bitmap(originalImage, new Size(24, 24));
    }

    public BasicInvader(int x, int y) : base(x, y, 24, 24)
    {
        Health = 1;
        Damage = 1;
        Score = 20;
        Image = basicInvaderImage; 
    }
}

