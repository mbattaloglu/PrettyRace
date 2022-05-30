using System.Collections;
using UnityEngine;
using TMPro;

public class CreateGameScreen : Screen
{
    public Transform cars;
    private Transform activeCar;
    int index = 0;

    public TextMeshProUGUI carName;

    private void OnEnable()
    {
        for (int i = 0; i < cars.childCount; i++)
        {
            cars.GetChild(i).gameObject.SetActive(false);
        }
        cars.GetChild(index).gameObject.SetActive(true);
        activeCar = cars.GetChild(index);
        carName.text = activeCar.name;
    }

    public override void DoThingsAtClose()
    {
        Debug.Log("Create Game Screen is On");
    }

    public override void DoThingsAtShow()
    {
        Debug.Log("Create Game Screen is Off");
    }


    public void NextCarOnClick()
    {
        activeCar.gameObject.SetActive(false);
        if (index == cars.childCount - 1)
        {
            index = -1;
        }
        activeCar = cars.GetChild(++index);
        activeCar.gameObject.SetActive(true);
        carName.text = activeCar.name;
    }

    public void PreviousCarOnClick()
    {
        activeCar.gameObject.SetActive(false);
        if (index == 0)
        {
            index = cars.childCount;
        }
        activeCar = cars.GetChild(--index);
        activeCar.gameObject.SetActive(true);
        carName.text = activeCar.name;
    }

    public void PlayOnClick()
    {
        screenHandler.gameManager.playerCarName = activeCar.name;
        screenHandler.gameManager.gameObject.SetActive(true);
        TournamentHandler.GetInstance().CreateRaceEvent();
        CloseScreen();
        screenHandler.gameScreen.ShowScreen();
    }


}
