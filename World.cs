using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoxelLike;

public class World
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int[,] Terrain { get; set; }

    private FastNoiseLite noise;

    public World(int width, int height)
    {
        Width = width;
        Height = height;
        Terrain = new int[Width, Height];

        noise = new FastNoiseLite(); // Criando o gerador de ruído
        noise.SetSeed(1337); // Define uma semente fixa (para gerar sempre o mesmo mundo)
        noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin); // Define o tipo de ruído
        noise.SetFrequency(0.1f); // Define a frequência do ruído

        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < Width; x++) 
        {
            float height = (noise.GetNoise(x, 0) + 1) * 0.5f * Height; // Normaliza o valor para 0 a Height
            int terrainHeight = (int)height;

            for (int y = 0; y < Height; y++) 
            {
                Terrain[x, y] = (y > terrainHeight) ? 0 : 1; // Preenche com terra abaixo da altura gerada
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
