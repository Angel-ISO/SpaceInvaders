using NAudio.Wave;
using SpaceInvaders.Interface;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;


namespace SpaceInvaders.Entities;
    public class Ship : BaseEntity, IDraw
    {

    private static readonly Image shipImage;
    public int Health = 3;
    public readonly List<ShipAmmo> ammoList = [];
    private DateTime lastShotTime = DateTime.MinValue;
    public int Damage { get; protected set; } = 1;
    public int Credits { get; set; }

    public List<ShipAmmo> AmmoList => ammoList;

    public event Action<int>? OnHealthChanged;


    private const int ShootCooldown = 500; 


    static Ship()
    {
        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "Ship.png");

        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException($"No se encontró la imagen en la ruta: {imagePath}");
        }

        var originalImage = Image.FromFile(imagePath);
        shipImage = new Bitmap(originalImage, new Size(32, 32));
    }

    public int Speed { get; set; } = 5;

    public Ship(int x, int y) : base(x, y, 32, 32) {
    }

    public void MoveLeft()
    {
        X -= Speed;
    }

    public void MoveRight()
    {
        X += Speed;
    }

    public void Shoot()
    {
        if ((DateTime.Now - lastShotTime).TotalMilliseconds < ShootCooldown)
            return;

        SoundManager.PlaySound("shoot-.mp3");

        ammoList.Add(new ShipAmmo(X + Width / 2, Y, this.Damage));



        lastShotTime = DateTime.Now;
    }

    public void UpdateAmmo()
    {
        ammoList.RemoveAll(ammo => ammo.Move() || !ammo.IsOnScreen());
    }


    public void Draw(Graphics g)
    {
        g.DrawImage(shipImage, X, Y);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy();
        }
        OnHealthChanged?.Invoke(Health); 
    }


    public void DrawAmmo(Graphics g)
    {
        foreach (var ammo in ammoList)
        {
            ammo.Draw(g);
        }
    }
}

