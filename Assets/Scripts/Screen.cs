using UnityEngine;

public abstract class Screen : MonoBehaviour 
{
    
    public ScreenManager screenManager { get; private set; }
    public abstract void DoThingsAtShow();
    public abstract void DoThingsAtClose();

    private void Awake()
    {
        screenManager = FindObjectOfType<ScreenManager>();
    }

    public void ShowScreen()
    {
        gameObject.SetActive(true);
        DoThingsAtShow();
    }
    public void CloseScreen()
    {
        gameObject.SetActive(false);
        DoThingsAtClose();
    }

    public void QuitOnClick()
    {
        Application.Quit();
    }

}