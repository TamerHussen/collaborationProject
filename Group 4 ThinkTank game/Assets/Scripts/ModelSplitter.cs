using UnityEngine;
using System.Collections.Generic;

public class ModelSplitter : MonoBehaviour
{
    public Material pieceMaterial;
    private List<GameObject> pieces = new List<GameObject>();
    private bool isSplit = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isSplit)
                SplitModel();
            else
                ReassembleModel();
        }
    }

    void SplitModel()
    {
        if (isSplit) return;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("No MeshFilter found on object!");
            return;
        }

        Mesh originalMesh = meshFilter.mesh;
        int numPieces = 4;

        // Split into 4 parts
        int partSize = originalMesh.triangles.Length / (numPieces * 3); // Number of triangles per piece

        for (int i = 0; i < numPieces; i++)
        {
            CreatePiece(originalMesh, i * partSize, partSize, $"Piece_{i}");
        }

        isSplit = true;
    }

    void CreatePiece(Mesh originalMesh, int startTriangle, int triangleCount, string pieceName)
    {
        GameObject piece = new GameObject(pieceName);
        piece.transform.position = transform.position;
        piece.transform.rotation = transform.rotation;
        piece.transform.localScale = transform.localScale;

        MeshFilter pieceMeshFilter = piece.AddComponent<MeshFilter>();
        MeshRenderer pieceRenderer = piece.AddComponent<MeshRenderer>();
        pieceRenderer.material = pieceMaterial;

        Mesh pieceMesh = new Mesh();

        // Get vertices and triangles for the piece
        Vector3[] vertices;
        int[] triangles;
        ExtractMeshSection(originalMesh, startTriangle, triangleCount, out vertices, out triangles);

        pieceMesh.vertices = vertices;
        pieceMesh.triangles = triangles;
        pieceMesh.RecalculateNormals();

        pieceMeshFilter.mesh = pieceMesh;

        // Add collider for interaction
        piece.AddComponent<MeshCollider>().convex = true;

        pieces.Add(piece);
    }

    void ExtractMeshSection(Mesh originalMesh, int startTriangle, int triangleCount, out Vector3[] vertices, out int[] triangles)
    {
        List<Vector3> newVertices = new List<Vector3>();
        List<int> newTriangles = new List<int>();

        int[] originalTriangles = originalMesh.triangles;
        Vector3[] originalVertices = originalMesh.vertices;

        Dictionary<int, int> vertexMap = new Dictionary<int, int>(); // Map old vertex index to new index

        for (int i = 0; i < triangleCount * 3; i++)
        {
            int vertexIndex = originalTriangles[startTriangle * 3 + i];

            if (!vertexMap.ContainsKey(vertexIndex))
            {
                vertexMap[vertexIndex] = newVertices.Count;
                newVertices.Add(originalVertices[vertexIndex]);
            }

            newTriangles.Add(vertexMap[vertexIndex]);
        }

        vertices = newVertices.ToArray();
        triangles = newTriangles.ToArray();
    }

    void ReassembleModel()
    {
        if (!isSplit) return;

        foreach (GameObject piece in pieces)
        {
            Destroy(piece);
        }

        pieces.Clear();
        isSplit = false;
    }
}
