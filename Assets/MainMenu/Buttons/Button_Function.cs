using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Function : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SelectScreenArcade;
    public GameObject SelectScreenVersus;
    public GameObject SettingsScreen;

    // Start is called before the first frame update
    void Start()
    {
        
        MainMenuButton();
    }

    public void MainMenuButton()
    {
        MainMenu.SetActive(true);
        SettingsScreen.SetActive(false);
        SelectScreenArcade.SetActive(false);
        SelectScreenVersus.SetActive(false);
    }
    public void ArcadeButton()
    {
        MainMenu.SetActive(false);
        SelectScreenArcade.SetActive(true);
    }
    public void VersusButton()
    {
        MainMenu.SetActive(false);
        SelectScreenVersus.SetActive(true);
    }
    public void SettingsButton()
    {
        MainMenu.SetActive(false);
        SettingsScreen.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
