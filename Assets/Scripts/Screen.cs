using UnityEngine;

public abstract class Screen : MonoBehaviour 
{   
    public ScreenHandler screenHandler { get; private set; }
    public abstract void DoThingsAtShow();
    public abstract void DoThingsAtClose();

    private void Awake()
    {
        screenHandler = FindObjectOfType<ScreenHandler>();
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