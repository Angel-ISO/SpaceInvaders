using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Utils;
public static class BoundsChecker
{
    public static readonly int PanelWidth = 779;
    public static readonly int PanelHeight = 420;
    public static readonly int BarrierY = 300;

    public static int CheckX(int x, int width)
    {
        if (x < 0) return 0;
        if (x + width > PanelWidth) return PanelWidth - width;
        return x;
    }

    public static int CheckY(int y, int height)
    {
        if (y + height >= BarrierY)
            return BarrierY - height;
        return y;
    }

    public static bool ShouldInvertDirection(int x, int width)
    {
        return (x + width >= PanelWidth) || (x <= 0);
    }

}