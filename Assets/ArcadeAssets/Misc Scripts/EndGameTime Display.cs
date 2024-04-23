using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameTimeDisplay : MonoBehaviour
{
    private GameTimer GameTime;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        GameTime = GameObject.Find("GameTiming").GetComponent<GameTimer>();
        text = GetComponent<Text>();
        text.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Your Time: " + (GameTime.YourTime).ToString() + " Seconds";
    }
}
