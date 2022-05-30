using UnityEngine;

public class PauseScreen : Screen
{
    public override void DoThingsAtClose()
    {
        Debug.Log("Pause Screen is Off");
        screenHandler.gameManager.isGamePaused = false;
        Time.timeScale = 1f;
    }

    public override void DoThingsAtShow()
    {
        Debug.Log("Pause Screen is On");
        screenHandler.gameManager.isGamePaused = true;
        Time.timeScale = 0f;
    }
    
    public void GoMenuOnClick()
    {
        CloseScreen();
        screenHandler.gameManager.gameObject.SetActive(false);
        screenHandler.menuScreen.ShowScreen();
    }

    public void ContinueOnClick()
    {
        CloseScreen();
        screenHandler.gameScreen.ShowScreen();
    }


}