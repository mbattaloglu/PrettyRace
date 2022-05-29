using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public Transform screens;

    public MenuScreen menuScreen;
    public TournamentScreen tournamentScreen;
    public GameScreen gameScreen;
    public PauseScreen pauseScreen;

    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public TournamentHandler tournamentHandler;

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        tournamentHandler = TournamentHandler.GetInstance();
    }

    private void Start()
    {
        foreach (Transform screen in screens)
        {
            screen.gameObject.SetActive(false);
        }

        //Activate Menu
        screens.GetChild(0).gameObject.SetActive(true);
    }

}
