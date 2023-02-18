using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rtos.Audio;
using Rtos.Core;

namespace Rtos;

public class GameBase : Game
{
    private GraphicsDeviceManager _graphics;
    public SpriteBatch SpriteBatch;
    public bool DebugMode = false;
    public Color ClearColor = new(88, 85, 83);
    private MusicPlayer _musicPlayer;
    private Beatmap _beatmap;

    public GameBase()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        _graphics.PreferredBackBufferWidth = 1000;
        _graphics.PreferredBackBufferHeight = 700;
        _graphics.ApplyChanges();
    }
    protected override void Initialize()
    {
        base.Initialize();
        _beatmap = new();
        _beatmap.Load(this, "Songs/beatmap-638122527559512603-surprise/Lyn Inaizumi - Last Surprise (elvodqa) [Test].osu");
        foreach(var hitObject in _beatmap.HitObjects)
            hitObject.Load(this);
        _musicPlayer = new($"Songs/beatmap-638122527559512603-surprise/{_beatmap.AudioFile}");
        _musicPlayer.Volume = 0.05f;
        _musicPlayer.Play();
    }
    
    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
       
    }
    
    protected override void Update(GameTime gameTime)
    {
        Input.GetKeyboardState();
        Input.GetMouseState();
        
        if (Input.IsScrolled(Orientation.Up))
        { 
            _musicPlayer.Volume += 0.05f;
        }
        else if (Input.IsScrolled(Orientation.Down))
        {
            _musicPlayer.Volume -= 0.05f;
        }
        
        base.Update(gameTime);
        Input.FixScrollLater();
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(ClearColor);
        SpriteBatch.Begin();
        _beatmap.HitObjects[9].Draw(gameTime);
        SpriteBatch.End();
        base.Draw(gameTime);
    }
}