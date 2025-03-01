using SpaceInvaders.Interface;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Entities;
public abstract class Invader : BaseEntity, IDraw
{
    public int Health { get; set; } 
    public int Damage { get; protected set; } 
    public int Score { get; protected set; }
    private readonly List<InvaderAmmo> ammoList = [];
    public List<InvaderAmmo> AmmoList => ammoList;




    public int Speed { get; protected set; } 
    protected Image Image { get; set; }


    public event Action<int>? OnDestroyed;


    protected Invader(int x, int y, int width, int height) : base(x, y, width, height)
    {
        Speed = 1; 
    }

    public  void Draw(Graphics g)
    {
        g.DrawImage(Image, X, Y, Width, Height); 
    }

    public void Move()
    {
        X += Speed; 
    }

    public void MoveRight()
    {
        X += Speed;
    }
    public void MoveLeft()
    {
        X -= Speed;
    }

    public void ChangeDirection()
    {
        Speed *= -1; 
    }

    public void Shoot()
    {
        SoundManager.PlaySound("enemy-shoot.mp3");

        ammoList.Add(new InvaderAmmo(X + Width / 2, Y, this.Damage));
        Console.WriteLine($"Bala creada");

    }

    public void UpdateAmmo()
    {
        ammoList.RemoveAll(ammo => ammo.Move() || !ammo.IsOnScreen());
    }

    public void DrawAmmo(Graphics g)
    {
        foreach (var ammo in ammoList)
        {
            ammo.Draw(g);
        }
    }

   


    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy();
            OnDestroyed?.Invoke(Score); 
        }
    }

    public void MoveDown()
    {
        Y += 10; 
    }
}
