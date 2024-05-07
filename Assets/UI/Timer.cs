using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text text;
    public float RoundTimer;
    private RoundControl TimerControl;
    // Start is called before the first frame update
    void Start()
    {
        TimerControl = GameObject.Find("Center Text").GetComponent<RoundControl>();
        RoundTimer = 99;
        text = GetComponent<Text>();
        text.text = RoundTimer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       if (TimerControl.ControlActive == true)
        {
            RoundTimer -= Time.deltaTime;
            text.text = Mathf.RoundToInt(RoundTimer).ToString();
        }
        else
        {
            text.text = "99";
        }
       if (RoundTimer <= 0)
        {
            text.text = "0";
            RoundTimer = 99;
        }
       
        
        
    }
}
