using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class MazeArray : MonoBehaviour
{
    public int mazeSize = 25;

    private MazeGenerate _mazeGenerate;
    public GameObject player;

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

            GameVariables.gameSize += 4;
            Debug.Log(GameVariables.gameSize);
            GenerateNewMaze();
        }
    }

    private void GenerateNewMaze() {
        var mazeArray = GenerateMazeArray();
        int x = setExit(mazeArray);

        _mazeGenerate.GenerateMaze(mazeArray);
        
        //Reset the player's position to (0,0,0)
        player.transform.position = new Vector3(0,0,0);
    }

    private char[,] GenerateMazeArray() {

        //mazeSize = 51;
        mazeSize = GameVariables.gameSize;

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

    private int setExit(char[,] mazeArray){
        int low = 1;
        int high = mazeArray.GetLength(0) - 1;
        int height = mazeArray.GetLength(0);

        int doorPosition = Random.Range(high, low);
        bool doorSet = false;

        while(doorSet.Equals(false)){
            if(mazeArray[doorPosition, height-2] == '#' && mazeArray[doorPosition+1, height-1] == '@'){
                doorPosition = low;
                mazeArray[doorPosition, height-1] =  '|';
                doorSet = true;
            }
            else if(mazeArray[doorPosition, height-2] == '#'){
                doorPosition = low;
                mazeArray[doorPosition, height-1] = '|';
                doorSet = true;

            }
            else{
                mazeArray[doorPosition, height-1] = '|';
                doorSet = true;
            }
        }
        int x = doorPosition;

        return x;
    }
}
