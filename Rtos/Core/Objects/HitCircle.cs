using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rtos.Core.Objects;

public class HitCircle : HitObject
{
    private Texture2D _hitCircleTexture;
    private Texture2D _hitCircleOverlayTexture;
    private Texture2D _approachCircleTexture;
    private Texture2D[] _numberTexture;
    private GameBase _game;
    private Vector2 _origin;

    public override void Load(GameBase game)
    {
        base.Load(game);
        _game = game;
        _hitCircleTexture =
            Texture2D.FromStream(game.GraphicsDevice, 
                File.OpenRead("Skins/default/hitcircle.png"));
        _hitCircleOverlayTexture =
            Texture2D.FromStream(game.GraphicsDevice, 
                File.OpenRead("Skins/default/hitcircleoverlay.png"));
        _approachCircleTexture =
            Texture2D.FromStream(game.GraphicsDevice, 
                File.OpenRead("Skins/default/approachcircle.png"));
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

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    
    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}