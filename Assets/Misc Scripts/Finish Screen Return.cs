using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class FinishScreenReturn : MonoBehaviour
{
    private GameTimer Time;
    // Start is called before the first frame update
    void Start()
    {
        Time = GameObject.Find("GameTiming").GetComponent<GameTimer>();
        string path = "Assets/PlayerTimes.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(Time.YourTime.ToString());
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitButton(){

        SceneManager.LoadScene(sceneName: "Martial Fighter Main Menu");
    }
}
