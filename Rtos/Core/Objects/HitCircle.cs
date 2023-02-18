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
                File.Open("Skins/default/hitcircle.png", FileMode.Open, FileAccess.Read));
        _hitCircleOverlayTexture =
            Texture2D.FromStream(game.GraphicsDevice, 
                File.Open("Skins/default/hitcircleoverlay.png", FileMode.Open, FileAccess.Read));
        _approachCircleTexture =
            Texture2D.FromStream(game.GraphicsDevice, 
                File.Open("Skins/default/approachcircle.png", FileMode.Open, FileAccess.Read));
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

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    
    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        _game.SpriteBatch.Draw(_approachCircleTexture, Position, null, Color.White, 0f, _origin, 1f, SpriteEffects.None, 0f);
        _game.SpriteBatch.Draw(_hitCircleTexture, Position, null, Color.White, 0f, _origin, 1f, SpriteEffects.None, 0f);
        _game.SpriteBatch.Draw(_hitCircleOverlayTexture, Position, null, Color.White, 0f, _origin, 1f, SpriteEffects.None, 0f);
    }
}