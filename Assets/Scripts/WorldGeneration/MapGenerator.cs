using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public int width;
    public int height;
    public float noiseScale;
    public int octaves;
    public float persistance;
    public float lacunarity;
    public int seed;
    public float heightMultiplier;
    public AnimationCurve heightCurve;
    public Vector2 offset;
    public TerrainType[] regions;
    public GameObject player;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    public MeshRenderer meshRenderer;

    public void Start() {
        float[,] heightMap = HeightMapGeneration.generateHeightMap(width, height, seed, noiseScale, octaves, persistance, lacunarity, offset);
        MeshData meshData = MeshGen.generateTerrainMeshData(heightMap, heightMultiplier, heightCurve);
        Texture2D texture = TextureGenerator.generateTexture(heightMap, regions);
        Mesh mesh = meshData.createMesh();
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
        meshRenderer.material.mainTexture = texture;
    }

}
