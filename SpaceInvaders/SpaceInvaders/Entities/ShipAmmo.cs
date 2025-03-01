using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Entities;
    public class ShipAmmo : Ammo
    {
    private static readonly Image ShipAmmoImage;


    static ShipAmmo()
    {
        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "ammo.png");

        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException($"No se encontró la imagen en la ruta: {imagePath}");
        }

        var originalImage = Image.FromFile(imagePath);
        ShipAmmoImage = new Bitmap(originalImage, new Size(5, 20));
    }

    public ShipAmmo(int x, int y, int damage) : base(x, y, 5, 20)
    {
        AmmoImage = ShipAmmoImage;
        Damage = damage;
    }

    public override bool Move()
    {
        Y -= Speed;  
        return Y + Height < 0;  
    }

}

