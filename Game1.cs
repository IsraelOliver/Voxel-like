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
        // TODO: Add your initialization logic here
        world = new World(20, 15); // Mundo de 20x15 blocos
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        blockTexture = new Texture2D(GraphicsDevice, 1, 1);
        blockTexture.SetData(new[] { Color.White });

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState keyboardState = Keyboard.GetState();
        MouseState mouseState = Mouse.GetState();

        // Alternar ferramenta ao pressionar a tecla "T"
        if (keyboardState.IsKeyDown(Keys.T))
        {
            currentTool = currentTool == Tool.Mine ? Tool.Place : Tool.Mine;
        }

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            int cellX = mouseState.X / 32;
            int cellY = mouseState.Y / 32;

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

        _spriteBatch.Begin();
        world.Draw(_spriteBatch, blockTexture);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
