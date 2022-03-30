using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager gameManager;

    [HideInInspector]
    public InputManager inputManager;

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
    }



}
