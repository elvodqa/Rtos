using Microsoft.Xna.Framework;

namespace Rtos.Core.Math;

public class Bezier
{
    public List<Vector2> Points { get; set; }
    public float ControlLength { get; set; }
    public float ApproxLength { get; set; }

    public Bezier()
    {
        
    }
}