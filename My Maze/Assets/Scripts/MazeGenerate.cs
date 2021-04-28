using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerate : MonoBehaviour
{
    public Material material;

    public GameObject plane;

    public void GenerateMaze(char[,] mazeArray) { 

        var meshObject = new GameObject();
        meshObject.name = "Mesh Object";
        meshObject.transform.parent = transform;

        var meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = material;

        var meshFilter = meshObject.AddComponent<MeshFilter>();
        
        var mesh = new Mesh();
        mesh.name = "Maze Mesh";
        meshFilter.sharedMesh = mesh;

        var mazeSize = mazeArray.GetLength(0);

        var vertices = new List<Vector3>();
        var triangles = new List<int>();

        var hasHitBox = new bool[mazeSize, mazeSize];
        
        var floor = Instantiate(plane);
        floor.transform.parent = transform;
        floor.transform.localPosition = new Vector3(0, 0, 0);
        floor.transform.localScale = new Vector3(mazeSize/2, 0.5F, mazeSize/2);

        for(int x = 0; x < mazeSize; x++){
            for(int z = 0; z < mazeSize; z++){
                
                if((mazeArray[x,z] == '@' || mazeArray[x,z] == '#') && !hasHitBox[x, z]){

                    var size = FindLargestBlock(mazeArray, hasHitBox, x, z);
                    AddBlock(mazeArray, vertices, triangles, x, z, size.width, size.height);

                }
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

    }

    private (int width, int height) FindLargestBlock(char[,] mazeArray, bool[,] hasHitBox, int x, int z){

        var mazeSize = mazeArray.GetLength(0);
        int zStart = z;
        int xStart = x;

        while(z < mazeSize - 1){
            if((mazeArray[x, z + 1] == '@' || mazeArray[x, z + 1] == '#') && !hasHitBox[x, z + 1]){
                z++;
            }
            else{
                break;
            }
        }

        while(x < mazeSize - 1){
            var canExpand = true;
            for(int i = zStart; i <= z; i++){
                if((mazeArray[x + 1, i] != '@' || mazeArray[x + 1, i] != '#') && !hasHitBox[x + 1, i]){
                    canExpand = false;
                    break;
                }
            }
            if(!canExpand){
                break;
            }
            x++;
        }

        for(int i = xStart; i <= x; i++){
            for(int j = zStart; j <= z; j++){
                hasHitBox[i,j] = true;
            }
        }

        int width = x - xStart + 1;
        int height = z - zStart + 1;

        return (width, height);

    }

    private void AddBlock(char[,] mazeArray, List<Vector3> vertices, List<int> triangles, int x, int z, int width, int height){
        
        var vertexOffset = vertices.Count;
        var mazeSize = mazeArray.GetLength(0);

        var centerOffset = mazeSize * 0.5F + 0.5F;

        vertices.Add(new Vector3(x - centerOffset, 2, z - centerOffset));
        vertices.Add(new Vector3(x + width - centerOffset, 2, z - centerOffset));
        vertices.Add(new Vector3(x + width - centerOffset, 2, z + height - centerOffset));
        vertices.Add(new Vector3(x - centerOffset, 2, z + height - centerOffset));

        vertices.Add(new Vector3(x - centerOffset, 0, z - centerOffset));
        vertices.Add(new Vector3(x + width - centerOffset, 0, z - centerOffset));
        vertices.Add(new Vector3(x + width - centerOffset, 0, z + height - centerOffset));
        vertices.Add(new Vector3(x - centerOffset, 0, z + height - centerOffset));

        //Top Face
        triangles.Add(0 + vertexOffset);
        triangles.Add(2 + vertexOffset);
        triangles.Add(1 + vertexOffset);

        triangles.Add(0 + vertexOffset);
        triangles.Add(3 + vertexOffset);
        triangles.Add(2 + vertexOffset);

        //Bottom Face
        triangles.Add(4 + vertexOffset);
        triangles.Add(5 + vertexOffset);
        triangles.Add(6 + vertexOffset);

        triangles.Add(4 + vertexOffset);
        triangles.Add(6 + vertexOffset);
        triangles.Add(7 + vertexOffset);
        
        //Side Faces
        triangles.Add(4 + vertexOffset);
        triangles.Add(1 + vertexOffset);
        triangles.Add(5 + vertexOffset);
        triangles.Add(4 + vertexOffset);
        triangles.Add(0 + vertexOffset);
        triangles.Add(1 + vertexOffset);

        triangles.Add(5 + vertexOffset);
        triangles.Add(2 + vertexOffset);
        triangles.Add(6 + vertexOffset);
        triangles.Add(5 + vertexOffset);
        triangles.Add(1 + vertexOffset);
        triangles.Add(2 + vertexOffset);

        triangles.Add(6 + vertexOffset);
        triangles.Add(3 + vertexOffset);
        triangles.Add(7 + vertexOffset);
        triangles.Add(6 + vertexOffset);
        triangles.Add(2 + vertexOffset);
        triangles.Add(3 + vertexOffset);

        triangles.Add(7 + vertexOffset);
        triangles.Add(0 + vertexOffset);
        triangles.Add(4 + vertexOffset);
        triangles.Add(7 + vertexOffset);
        triangles.Add(3 + vertexOffset);
        triangles.Add(0 + vertexOffset);

        var boxObject = new GameObject();
        boxObject.transform.parent = transform;
        boxObject.name = "Box Collider";
        //boxObject.transform.position = new Vector3(x + width/2f, 0, z + height/2f);
        boxObject.transform.position = new Vector3(x + width/2f - centerOffset, 0, z + height/2f - centerOffset);

        var boxCollider = boxObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(width, 1, height);

    }

}
