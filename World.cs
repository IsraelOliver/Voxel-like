using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoxelLike;

public class World
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int[,] Terrain { get; set; }

    public World(int width, int height)
    {
        Width = width;
        Height = height;
        Terrain = new int[Width, Height];
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        Random rand = new Random();
        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                Terrain[x, y] = rand.Next(0, 2); 
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D blockTexture) 
    {
        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                Color cellColor = Terrain[x, y] == 1 ? Color.Brown : Color.White;
                spriteBatch.Draw(blockTexture, new Rectangle(x * 32, y * 32, 32, 32), cellColor);
            }
        }
    }

    public void Mine(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            Terrain[x, y] = 0;
        }
    }

    public void Place(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            Terrain[x, y] = 1; // Define o bloco como preenchido
        }
    }

}
