using UnityEngine;

public class PauseScreen : Screen
{
    public override void DoThingsAtClose()
    {
        Debug.Log("Pause Screen is Off");
        screenManager.gameManager.isGamePaused = false;
    }

    public override void DoThingsAtShow()
    {
        Debug.Log("Pause Screen is On");
        screenManager.gameManager.isGamePaused = true;
    }
    
    public void GoMenuOnClick()
    {
        CloseScreen();
        screenManager.menuScreen.ShowScreen();
    }

    public void ContinueOnClick()
    {
        CloseScreen();
        screenManager.gameScreen.ShowScreen();
    }


}