using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rtos.Core.Objects;


public enum CurveType : byte
{
    Catmull = 0,
    Bezier = 1,
    Linear = 2,
    Perfect = 3,
}

public class CurvePoint
{
    public Vector2 Position;
    public CurveType Type;
}
public class Slider : HitObject
{
    public int Repeat;
    // Time already inherited from HitObject
    public ObjectType Type;
    public int HitSound;
    public List<CurvePoint> CurvePoints;
    public int SlideRepeatCount;
    public double Length;
    public int EdgeSounds;
    public int EdgeSets;
    
    private Texture2D _hitCircleTexture;
    private Texture2D _hitCircleOverlayTexture;
    private Texture2D _approachCircleTexture;
    private Texture2D _sliderTexture; // fruit-apple
    private Texture2D[] _numberTexture;
    private GameBase _game;
    private Vector2 _origin;
    private List<Vector2> _bodyPoints;

    public Slider()
    {
        CurvePoints = new();
    }

    public override void Load(GameBase game)
    {
        base.Load(game);
        _game = game;
        _hitCircleTexture =
            Texture2D.FromStream(game.GraphicsDevice, File.OpenRead("Skins/default/hitcircle.png"));
        _hitCircleOverlayTexture =
            Texture2D.FromStream(game.GraphicsDevice, File.OpenRead("Skins/default/hitcircleoverlay.png"));
        _approachCircleTexture =
            Texture2D.FromStream(game.GraphicsDevice, File.OpenRead("Skins/default/approachcircle.png"));
        _sliderTexture =
            Texture2D.FromStream(game.GraphicsDevice, File.OpenRead("Skins/default/fruit-apple.png"));
        string numberStr = ComboNumber.ToString();
        _numberTexture = new Texture2D[numberStr.Length];
        for (int i = 0; i < numberStr.Length; i++)
        {
            _numberTexture[i] =
                Texture2D.FromStream(game.GraphicsDevice,
                    File.OpenRead($"Skins/default/default-{numberStr[i]}.png"));
        }
        _origin = new Vector2(_hitCircleTexture.Width / 2f, _hitCircleTexture.Height / 2f);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
    }
    
    
    private List<Vector2> PointsBetween2Linear(Vector2 p0, Vector2 p1, int count)
    {
        List<Vector2> points = new();
        Vector2 diff = p1 - p0;
        for (int i = 0; i < count; i++)
        {
            points.Add(p0 + (diff * (i / (float)count)));
        }

        return points;
    }

    private List<Vector2> PointsBetweenXBezier(List<Vector2> positions, int count)
    {
        return null;
    }


    public static float Magnitude(Vector2 vector)
    {
        //return (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
        return null;
    }
}