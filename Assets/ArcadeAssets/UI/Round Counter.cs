using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoundCounter : MonoBehaviour
{
    public Text P1text;
    public Text P2text;
    private Player1Damage P1Count;
    private Player2Damage P2Count;
    private RoundControl Reset;
    // Start is called before the first frame update
    void Start()
    {
        Reset = GameObject.Find("Center Text").GetComponent<RoundControl>();
        P1Count = GameObject.FindWithTag("Player1").GetComponent<Player1Damage>();

    }

    // Update is called once per frame
    void Update()
    {
        
        P2Count = GameObject.FindWithTag("Player2").GetComponent<Player2Damage>();
        if (P1Count.P1Deaths == 2)
        {
            P2text.text = "II";
        }
        else if (P1Count.P1Deaths == 1)
        {
            P2text.text = "I";
        }
        else
        {
            P2text.text = "";
        }
        if (P2Count.P2Deaths == 2)
        {
            P1text.text = "II";
        }
        else if (P2Count.P2Deaths == 1)
        {
            P1text.text = "I";
        }
        else
        {
            P1text.text = "";
        }
    }
}
