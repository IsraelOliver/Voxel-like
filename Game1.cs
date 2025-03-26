using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VoxelLike;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private World world;
    private Texture2D blockTexture;
    private Camera camera;

    private enum Tool
    {
        Mine, // Remove blocos
        Place // Adiciona blocos
    }

private Tool currentTool = Tool.Mine;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        world = new World(50, 30); // Mundo maior
        camera = new Camera(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        blockTexture = new Texture2D(GraphicsDevice, 1, 1);
        blockTexture.SetData(new[] { Color.White });

        // TODO: use this.Content to load your game content here
    }

    Vector2 GetMouseWorldPosition()
    {
        MouseState mouseState = Mouse.GetState();
        Vector2 screenPosition = new Vector2(mouseState.X, mouseState.Y);

        // Converter para coordenadas do mundo usando a matriz inversa da câmera
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
        world.Draw(_spriteBatch, blockTexture);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
