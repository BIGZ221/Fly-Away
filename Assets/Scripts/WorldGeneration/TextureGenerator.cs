using UnityEngine;

public static class TextureGenerator {

    public static Texture2D generateTexture(float[,] map, TerrainType[] regions) {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        Texture2D texture = new Texture2D(width, height);
        Color[] colorMap = new Color[width * height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Color c = Color.white;
                float noiseHeight = map[x,y];
                foreach (TerrainType type in regions) {
                    if (noiseHeight < type.height) {
                        c = type.color;
                        break;
                    }
                }
                colorMap[y * width + x] = c;
            }
        }
        texture.filterMode = FilterMode.Trilinear;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

}
