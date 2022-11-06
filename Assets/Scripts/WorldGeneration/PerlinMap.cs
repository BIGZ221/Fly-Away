using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinMap : MonoBehaviour {

    public int width = 64;
    public int height = 64;
    public float xOffset = 0;
    public float yOffset = 0;
    public int noiseScale = 5;
    public float squareSize = 1f;
    
    private float[,] map;
    private float[,] oldMap;
    private MeshGenerator meshGen;
    private PlayerController player;

    public void Start() {
        map = new float[width,height];
        meshGen = GetComponent<MeshGenerator>();
        player = GetComponent<PlayerController>();
    }

    public void FixedUpdate() {
        if (player.isDead) return;
        // If the player is dead do not keep updating the map. If the updates are allowed to continue the player will phase through the map as the map will sometimes move to where the player
        // was which ultimately can put the player under the mesh's collider.
        xOffset = transform.localPosition.x / noiseScale;
        yOffset = transform.localPosition.z / noiseScale;
        GeneratePerlinMap();
        meshGen.GenerateMesh(map, squareSize);
    }

    public void GeneratePerlinMap() {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x,y] = CalculateNoise(x, y);
            }
        }
    }

    float CalculateNoise(int x, int y) {
        float scaledX = (((float) x + xOffset) / width) * noiseScale;
        float scaledY = (((float) y + yOffset) / height) * noiseScale;
        return Mathf.PerlinNoise(scaledX, scaledY);
    }

}
