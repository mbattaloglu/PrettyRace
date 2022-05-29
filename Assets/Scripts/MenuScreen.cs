using UnityEngine;

public class MenuScreen : Screen
{
    public override void DoThingsAtClose()
    {
        Debug.Log("Menu Screen Is Off");
    }

    public override void DoThingsAtShow()
    {
        Debug.Log("Menu Screen Is On");
    }

    public void PlayOnClick()
    {
        CloseScreen();
        screenManager.gameScreen.ShowScreen();

    }
}