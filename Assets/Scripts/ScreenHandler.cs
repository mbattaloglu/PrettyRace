using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHandler : MonoBehaviour
{
    public Transform screens;

    public MenuScreen menuScreen;
    public TournamentScreen tournamentScreen;
    public GameScreen gameScreen;
    public PauseScreen pauseScreen;
    public CreateGameScreen createGameScreen;
    public GameEndScreen gameEndScreen;

    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public PointHandler pointHandler;
    
    private static ScreenHandler instance;

    private ScreenHandler()
    {
        if(instance == null) instance = this;
    }

    public static ScreenHandler GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        pointHandler = PointHandler.GetInstance();
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

    private void Update()
    {
        if (gameManager.isRaceOver)
        {
            foreach (Transform screen in screens)
            {
                screen.gameObject.SetActive(false);
            }
            gameEndScreen.ShowScreen();
        }
    }

}
