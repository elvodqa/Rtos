using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rtos.Core.Objects;


/*
Hit object types are stored in an 8-bit integer where each bit is a flag with special meaning. The base hit object type is given by bits 0, 1, 3, and 7 (from least to most significant):
0: Hit circle
1: Slider
3: Spinner
7: osu!mania hold
The remaining bits are used for distinguishing new combos and optionally skipping combo colours (commonly called "colour hax"):

2: New combo
4–6: A 3-bit integer specifying how many combo colours to skip, if this object starts a new combo.
* 
*/

[Flags]
public enum ObjectType
{
    Circle = 1,
    Slider = 2,
    NewCombo = 4,
    Spinner = 8,
    ColourHax = 16,
    ManiaHold = 128
}

public class HitObject
{
    public Vector2 Position;
    public double Time;
    public ObjectType Type;
    public Color Color;
    public int HitSound;
    public int ComboNumber;

    public virtual void Load(GameBase game)
    {
    }
    public virtual void Update(GameTime gameTime)
    {
    }
    
    public virtual void Draw(GameTime gameTime)
    {
    }
/*
    public Texture2D LoadFromStream(GameBase game, string path)
    {
        FileStream stream =
        Texture2D.FromStream(game.GraphicsDevice, 
            File.Open("Skins/default/hitcircle.png", FileMode.Open, FileAccess.Read));
    } */
}