using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace VoxelLike;

public class Camera
{
    public Vector2 Position;
    public float Zoom { get; private set; }
    public float Speed { get; set; } = 200f; // Velocidade de movimento
    public float ZoomSpeed { get; set; } = 0.1f; // Velocidade do zoom
    public float MinZoom { get; set; } = 0.5f; // Zoom mínimo
    public float MaxZoom { get; set; } = 2.0f; // Zoom máximo

    private Matrix _transform;
    private int _viewportWidth;
    private int _viewportHeight;

    public Camera(int viewportWidth, int viewportHeight)
    {
        _viewportWidth = viewportWidth;
        _viewportHeight = viewportHeight;
        Zoom = 1.0f;
        Position = Vector2.Zero;
        UpdateTransform();
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Movimento com WASD
        if (keyboardState.IsKeyDown(Keys.W))
            Position.Y -= Speed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.S))
            Position.Y += Speed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.A))
            Position.X -= Speed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.D))
            Position.X += Speed * deltaTime;

        if (keyboardState.IsKeyDown(Keys.OemPlus) || keyboardState.IsKeyDown(Keys.Add))
            Zoom = MathHelper.Clamp(Zoom + ZoomSpeed, MinZoom, MaxZoom);
        if (keyboardState.IsKeyDown(Keys.OemMinus) || keyboardState.IsKeyDown(Keys.Subtract))
            Zoom = MathHelper.Clamp(Zoom - ZoomSpeed, MinZoom, MaxZoom);

        UpdateTransform();
    }

    private void UpdateTransform()
    {
        _transform = Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                     Matrix.CreateScale(Zoom, Zoom, 1) *
                     Matrix.CreateTranslation(_viewportWidth / 2f, _viewportHeight / 2f, 0);
    }

    public Matrix GetTransform()
    {
        return _transform;
    }
}