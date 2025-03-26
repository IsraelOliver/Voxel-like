using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VoxelLike;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private World world;
    private Camera camera;

    private enum Tool
    {
        Mine,
        Place
    }

    private Tool currentTool = Tool.Mine;

    private const int Width = 100;  // Defina o tamanho do mundo
    private const int Height = 100; // Defina o tamanho do mundo


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        
        camera = new Camera(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        Texture2D dirtTileTexture = Content.Load<Texture2D>("dirt");
        world = new World(Width, Height, dirtTileTexture);

    }

    Vector2 GetMouseWorldPosition()
    {
        MouseState mouseState = Mouse.GetState();
        Vector2 screenPosition = new Vector2(mouseState.X, mouseState.Y);

        Matrix inverseTransform = Matrix.Invert(camera.GetTransform());
        return Vector2.Transform(screenPosition, inverseTransform);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState keyboardState = Keyboard.GetState();
        MouseState mouseState = Mouse.GetState();
        camera.Update(gameTime);

        if (keyboardState.IsKeyDown(Keys.T))
        {
            currentTool = currentTool == Tool.Mine ? Tool.Place : Tool.Mine;
        }

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            Vector2 worldMousePos = GetMouseWorldPosition();
            
            int cellX = (int)(worldMousePos.X / 32);
            int cellY = (int)(worldMousePos.Y / 32);

            if (currentTool == Tool.Mine)
            {
                world.Mine(cellX, cellY);
            }
            else if (currentTool == Tool.Place)
            {
                world.Place(cellX, cellY);
            }
        }

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin(transformMatrix: camera.GetTransform());
        world.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
