using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.ViewModel;

public class BarriersViewModel
{
    public List<Shield> barriers { get; private set; }
    public IReadOnlyList<Shield> Barriers => barriers;


    public BarriersViewModel()
    {
        barriers = new List<Shield>();
        InitializeBarriers();
    }

    private void InitializeBarriers()
    {
        int spacing = 120;
        int width = 40;
        int startX = (779 - (5 * width + 4 * spacing)) / 2;
        int y = 420 - 100;

        for (int i = 0; i < 5; i++)
        {
            int x = startX + i * (width + spacing);
            barriers.Add(new Shield(x, y));
        }
    }

    public void Draw(Graphics g)
    {
        foreach (var barrier in barriers)
            barrier.Draw(g);
    }

    public void RemoveDestroyedBarriers()
    {
        barriers.RemoveAll(barrier => barrier.IsDestroyed);
    }

}
