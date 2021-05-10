using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVariables : MonoBehaviour
{

    public static int gameSize = 15;
    public static int level = 1;

    //boundary variables
    public static int pBoundary;
    public static int nBoundary;

    public Text levelText;

    public static float timeElapsed = 0;
    public Text stopwatchText;

    // Start is called before the first frame update
    void Start()
    {
        levelText.color = Color.red;
        stopwatchText.color = Color.red;
    }


    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        stopwatchText.text = "Time Elapsed: " + timeElapsed.ToString("F2");
        levelText.text = "Level: " + level;
    }
}