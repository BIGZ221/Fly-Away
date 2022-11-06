using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {
    
    public GameObject map;
    public float heightScale = 1.0f;
    public Square[,] squareGrid;
    private List<Vector3> vertices;
    private List<int> triangles;

    private int prevWidth;
    private int prevHeight;

    public void GenerateMesh(float[,] activeMap, float squareSize) {
        float altitude = map.transform.position.y;
        Vector3 newPos = new Vector3(transform.position.x, altitude, transform.position.z);
        map.transform.position = newPos;
        squareGrid = GenerateSquareGrid(activeMap, squareSize);
        triangles = new List<int>();
        vertices = new List<Vector3>();

        int width = squareGrid.GetLength(0);
        int height = squareGrid.GetLength(1);
        for (int x = 0; x < width; ++x) {
            for (int y = 0; y < height; ++y) {
                TriangulateSquare(squareGrid[x,y]);
            }
        }
        
        Mesh mesh = new Mesh();
        map.GetComponent<MeshFilter>().mesh = mesh;
        map.GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = ColorsFromVertices(vertices);
        mesh.RecalculateNormals();
    }

    Color[] ColorsFromVertices(List<Vector3> vertices) {
        
        Color[] colors = new Color[vertices.Count];
        float max = 0;
        for (int i = 0; i < vertices.Count; i++) {
            max = Mathf.Max(vertices[i].z, max);
            colors[i] = Color.Lerp(Color.green, Color.grey, vertices[i].z / 15);
        }
        return colors;
    }

    void TriangulateSquare(Square square) {
        MeshFromPoints(square.topLeft, square.topRight, square.bottomRight, square.bottomLeft);
    }

    void MeshFromPoints(params Node[] points) {
        AssignVertices(points);
        CreateTriangle(points[0], points[1], points[2]);
        CreateTriangle(points[0], points[2], points[3]);
    }

    void AssignVertices(Node[] points) {
        for (int i = 0; i < points.Length; i++) {
            if (points[i].index == -1) {
                points[i].index = vertices.Count;
                vertices.Add(points[i].position);
            }
        }
    }

    void CreateTriangle(Node a, Node b, Node c) {
        triangles.Add(a.index);
        triangles.Add(b.index);
        triangles.Add(c.index);
    }

    Square[,] GenerateSquareGrid(float[,] heightMap, float squareSize) {
        int x = heightMap.GetLength(0);
        int y = heightMap.GetLength(1);
        float mapWidth = x * squareSize;
        float mapHeight = y * squareSize;
        ControlNode[,] controlNodes = new ControlNode[x,y];
        for (int i = 0; i < x; i++) {
            for (int j = 0; j < y; j++) {
                Vector3 position = new Vector3(-mapWidth / 2f + i * squareSize + squareSize / 2, heightMap[i, j] * heightScale + squareSize / 2, -mapHeight / 2f + j * squareSize + squareSize / 2);
                controlNodes[i,j] = new ControlNode(position, squareSize);
            }
        }
        Square[,] squares = new Square[x - 1, y -1];
        for (int i = 0; i < x - 1; i++) {
            for (int j = 0; j < y - 1; j++) {
                squares[i, j] = new Square(controlNodes[i, j+1], controlNodes[i+1, j+1], controlNodes[i+1, j], controlNodes[i,j]);
            }
        }
        return squares;
    }

}
