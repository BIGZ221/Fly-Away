using UnityEngine;
using Random = System.Random;

public static class HeightMapGeneration {
    
    public static float[,] generateHeightMap(int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
        float[,] map = new float[width, height];
        Random rng  = new Random(seed);
        Vector2[] octaveOffset = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) {
            float offsetX = rng.Next(-100000, 100000) + offset.x;
            float offsetY = rng.Next(-100000, 100000) + offset.y;
            octaveOffset[i] = new Vector2(offsetX, offsetY);
        }
        if (scale < 0) scale = 0.0001f;
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                float amp = 1;
                float freq = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++) {
                    float scaledX = (x - halfWidth) / scale * freq + octaveOffset[i].x;
                    float scaledY = (y - halfHeight) / scale * freq + octaveOffset[i].y;
                    float perlin = Mathf.PerlinNoise(scaledX, scaledY);

                    noiseHeight += perlin * amp;
                    amp *= persistance;
                    freq *= lacunarity;
                }
                maxNoiseHeight = Mathf.Max(maxNoiseHeight, noiseHeight);
                minNoiseHeight = Mathf.Min(minNoiseHeight, noiseHeight);
                map[x,y] = noiseHeight;
            }
        }
        return normalizeMap(map, maxNoiseHeight, minNoiseHeight);
    }

    private static float[,] normalizeMap(float[,] map, float max, float min) {
        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                map[x,y] = Mathf.InverseLerp(min, max, map[x,y]);
            }
        }
        return map;
    }

}
