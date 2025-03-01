using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Entities;
public class InvaderAmmo : Ammo
{
    private static readonly Image InvaderAmmoImage;


    static InvaderAmmo()
    {
        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "InvadersAmmo.png");

        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException($"No se encontró la imagen en la ruta: {imagePath}");
        }

        var originalImage = Image.FromFile(imagePath);
        InvaderAmmoImage = new Bitmap(originalImage, new Size(10, 16));
    }

    public InvaderAmmo(int x, int y, int damage) : base(x, y, 10, 16)
    {
        Speed = 3;
        AmmoImage = InvaderAmmoImage;
        Damage = damage;

    }

    public override bool Move()
    {
        Y += Speed;  
        return Y + Height > BoundsChecker.PanelHeight;
    }
}

