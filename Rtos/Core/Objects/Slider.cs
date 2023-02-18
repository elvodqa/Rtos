using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rtos.Core.Objects;


public enum CurveType : byte
{
    Catmull = 0,
    Bezier = 1,
    Linear = 2,
    Perfect = 3,
    Circle = 4,
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

    public Slider()
    {
        CurvePoints = new();
    }

    public override void Load(GameBase game)
    {
        base.Load(game);
        _game = game;
        _hitCircleTexture =
            Texture2D.FromStream(game.GraphicsDevice, File.Open("Skins/default/hitcircle.png", FileMode.Open));
        _hitCircleOverlayTexture =
            Texture2D.FromStream(game.GraphicsDevice, File.Open("Skins/default/hitcircleoverlay.png", FileMode.Open));
        _approachCircleTexture =
            Texture2D.FromStream(game.GraphicsDevice, File.Open("Skins/default/approachcircle.png", FileMode.Open));
        _sliderTexture =
            Texture2D.FromStream(game.GraphicsDevice, File.Open("Skins/default/fruit-apple.png", FileMode.Open));
        string numberStr = ComboNumber.ToString();
        _numberTexture = new Texture2D[numberStr.Length];
        for (int i = 0; i < numberStr.Length; i++)
        {
            _numberTexture[i] =
                Texture2D.FromStream(game.GraphicsDevice,
                    File.Open($"Skins/default/default-{numberStr[i]}.png", FileMode.Open));
        }
        _origin = new Vector2(_hitCircleTexture.Width / 2f, _hitCircleTexture.Height / 2f);

    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        // draw slider body
        for (int i = 0; i < CurvePoints.Count - 1; i++)
        {
            var start = CurvePoints[i].Position;
            var end = CurvePoints[i + 1].Position;
            var distance = Vector2.Distance(start, end);
            var angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            var scale = new Vector2(distance / _sliderTexture.Width, 1f);
            _game.SpriteBatch.Draw(_sliderTexture, start, null, Color.White, angle, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}