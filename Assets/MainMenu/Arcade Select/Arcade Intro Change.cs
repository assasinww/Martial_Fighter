using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcadeIntroChange : MonoBehaviour
{
    public GameObject Martial2Text; public GameObject Martial3Text; public GameObject MedievalText;
    public bool Seki = false; public bool Draven = false; public bool Cedric = false;
    // Start is called before the first frame update
    void Start()
    {
        Martial2Text.SetActive(false); Martial3Text.SetActive(false); MedievalText.SetActive(false);
    }
    //adding function to the buttons of the characters themselves and the confirmation button
    public void SekiButton()
    {
        Martial2Text.SetActive(true); Martial3Text.SetActive(false); MedievalText.SetActive(false);
        Seki = true;
    }
    public void CedricButton()
    {
        Martial2Text.SetActive(false); Martial3Text.SetActive(false); MedievalText.SetActive(true);
        Cedric = true;
    }
    public void DravenButton()
    {
        Martial2Text.SetActive(false); Martial3Text.SetActive(true); MedievalText.SetActive(false);
        Draven = true;
    }
    public void ConfirmButton()
    {
        //Saves player's choice in arcade character select to load the corresponding arcade story
        if (Seki == true)
        {
            PlayerPrefs.SetString("ArcadeChar", "Seki");
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneName:"Martial Fighter Arcade");
        }
        if (Cedric == true)
        {
            PlayerPrefs.SetString("ArcadeChar", "Cedric");
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneName: "Martial Fighter Arcade");
        }
        if (Draven == true)
        {
            PlayerPrefs.SetString("ArcadeChar", "Draven");
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneName: "Martial Fighter Arcade");
        }
    }

}
