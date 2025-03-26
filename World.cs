using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoxelLike;

public class World
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int[,] Terrain { get; set; }
    private Texture2D dirtTileTexture;

    private FastNoiseLite noise;

    public World(int width, int height, Texture2D dirtTexture)
    {
        dirtTileTexture = dirtTexture;

        Width = width;
        Height = height;
        Terrain = new int[Width, Height];

        noise = new FastNoiseLite();
        noise.SetSeed(1338);
        noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        noise.SetFrequency(0.1f);

        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < Width; x++) 
        {
            float height = (noise.GetNoise(x, 0) + 1) * 0.5f * Height;
            int terrainHeight = (int)height;

            for (int y = 0; y < Height; y++) 
            {
                Terrain[x, y] = (y > terrainHeight) ? 0 : 1;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch) 
    {
        for (int x = 0; x < Width; x++) 
        {
            for (int y = 0; y < Height; y++) 
            {
                
                if (Terrain[x, y] == 0) 
                {
                    // Aqui você desenha a textura no local adequado
                    spriteBatch.Draw(dirtTileTexture, new Vector2(x * 32, y * 32), Color.White);
                }
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
            Terrain[x, y] = 1;
        }
    }

}
