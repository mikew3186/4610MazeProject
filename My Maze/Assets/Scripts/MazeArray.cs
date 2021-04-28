using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class MazeArray : MonoBehaviour
{
    public int mazeSize = 25;

    private MazeGenerate _mazeGenerate;

    // Start is called before the first frame update
    void Start()
    {
        _mazeGenerate = GetComponent<MazeGenerate>();

        GenerateNewMaze();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            for (int i = transform.childCount - 1; i >= 0; i--){
                Destroy(transform.GetChild(i).gameObject);
            }
            
            GenerateNewMaze();
        }
    }

    private void GenerateNewMaze() {
        var mazeArray = GenerateMazeArray();
        _mazeGenerate.GenerateMaze(mazeArray);
    }

    private char[,] GenerateMazeArray() {

        char[,] mazeArray = new char[mazeSize, mazeSize];

        for (int x = 0; x < mazeSize; x++) {
            for (int z = 0; z < mazeSize; z++) {
                mazeArray[x, z] = '@';
            }
        }

        for (int x = 1; x < mazeSize-1; x++)
        {
            for (int z = 1; z < mazeSize-1; z++)
            {
                mazeArray[x, z] = '#';
            }
        }

        for (int x = 1; x < mazeSize; x+=2)
        {
           for (int z = 1; z < mazeSize; z+=2)
           {
               //Starting point [0,0]
               if (mazeArray[x - 1, z] == '@' && mazeArray[x, z - 1] == '@') {
                   mazeArray[x, z] = '|';
                   continue;
               }

               //First Row of Maze 
               if (mazeArray[x - 1, z] != '@' && mazeArray[x, z - 1] == '@'){
                   mazeArray[x,z] = '|';
                   mazeArray[x - 1, z] = '|';
               }

               //First Column of Maze
               if (mazeArray[x - 1, z] == '@' && mazeArray[x, z - 1] != '@')
               {
                   mazeArray[x, z] = '|';
                   mazeArray[x, z - 1] = '|';
               }

               if (mazeArray[x - 1, z] == '#' || mazeArray[x, z - 1] == '#') {
                   int coinFlip = Random.Range(0,2);
                   //North
                   if (coinFlip == 0) {
                       mazeArray[x, z] = '|';
                       mazeArray[x, z - 1] = '|';
                   }
                   //West
                   if (coinFlip == 1)
                   {
                       mazeArray[x, z] = '|';
                       mazeArray[x-1, z] = '|';
                   }
               }
           }
        }
        return mazeArray;
    }
}
