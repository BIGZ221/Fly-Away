using UnityEngine;

public class MeshData {

    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    private int index;

    public MeshData(int width, int height) {
        vertices = new Vector3[width * height];
        uvs = new Vector2[width * height];
        triangles = new int[(width - 1) * (height - 1) * 6];
    }

    public void addTriangle(int a, int b, int c) {
        triangles[index] = a;
        triangles[index + 1] = b;
        triangles[index + 2] = c;
        index += 3;
    }

    public Mesh createMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
    
}
