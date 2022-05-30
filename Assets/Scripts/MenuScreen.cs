using UnityEngine;

public class MenuScreen : Screen
{
    public override void DoThingsAtClose()
    {
        Debug.Log("Menu Screen Is Off");
    }

    public override void DoThingsAtShow()
    {
        for (int i = 0; i < screenHandler.gameManager.AICars.childCount; i++)
        {
            Destroy(screenHandler.gameManager.AICars.GetChild(i).gameObject);
        }
        Camera.main.transform.parent = null;
        Destroy(GameObject.Find("Player"));
    }

    public void GoCreateGameMenuOnClick()
    {
        CloseScreen();
        screenHandler.createGameScreen.ShowScreen();

    }
}