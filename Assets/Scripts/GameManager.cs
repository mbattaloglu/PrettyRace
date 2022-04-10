using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager gameManager;
    public Transform player;

    [HideInInspector]
    public InputManager inputManager;

    public float powerUp;

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
        inputManager = InputManager.GetInstance();
        powerUp = 1;
    }



}
