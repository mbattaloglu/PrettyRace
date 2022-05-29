using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager gameManager;
    public Transform player;

    public float powerUp;
    public bool isGameStarted;
    public bool isGamePaused;

    private GameManager()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    }

    public static GameManager GetInstance()
    {
        return gameManager;
    }


    private void Start()
    {
        powerUp = 1;
        isGameStarted = false;
        isGamePaused = false;
    }
}
