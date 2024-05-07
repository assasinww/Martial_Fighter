using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenu;
   
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            Time.timeScale = 0;
            pausemenu.SetActive(true);
        }  

    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        pausemenu.SetActive(false);
    }
    public void ExitButton()
    {
        SceneManager.LoadScene(sceneName: "Martial Fighter Main Menu");
    }
}
