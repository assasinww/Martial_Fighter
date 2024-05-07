using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RoundControl : MonoBehaviour
{
    private Text text;
    public int RoundCount;
    public bool Roundstart;
    private float DisplayTimer;
    public bool ControlActive;
    public bool MatchOver;
    private Player1Damage P1Info;
    private Player2Damage P2Info;
    private Timer RoundTimer;
    public GameObject Map1; public GameObject Map2; public GameObject Map3; public GameObject FinishScreen;
    public GameObject Opponent1; public GameObject Opponent2; public GameObject Opponent3; private GameObject Player;
    private GameObject UIHIDE;
    // Start is called before the first frame update
    void Start()
    {
        UIHIDE = GameObject.Find("Top Screen UI");
        Player = GameObject.FindWithTag("Player1");
        RoundTimer = GameObject.Find("Timer(Black)").GetComponent<Timer>();
        P1Info = GameObject.FindWithTag("Player1").GetComponent<Player1Damage>();
        text = GetComponent<Text>();
        RoundCount = 1;
        Roundstart = true;
    }

    // Update is called once per frame
    void Update()
    {
        P2Info = GameObject.FindWithTag("Player2").GetComponent<Player2Damage>();
        // Causing Roundstart Text with timing
        if (Roundstart == true && MatchOver == false)
        {
            
            ControlActive = false;
            text.text = "Round" + RoundCount.ToString();
            Invoke("RoundStart",1f);
        }
        DisplayTimer -= Time.deltaTime;
        if (DisplayTimer <= 0 && Roundstart == false && MatchOver == false)
        {
            ControlActive = true;
            text.text = "";
        }
              
    }
    private void RoundStart()
    {
        ControlActive = false;
        DisplayTimer = 0.5f;
        text.text = "FIGHT!";
        Roundstart = false;
        
    }
    private void MatchEnd()
    {
        
        //Depending on player deaths shows different text
        if (P1Info.P1Deaths == 2 || P2Info.P2Deaths == 2) {
            if (P1Info.P1Deaths < P2Info.P2Deaths)
            {
                text.text = "Player 1 Wins";

            if (Map1.active == true)
            {
                Map1.SetActive(false);
                Opponent1.SetActive(false);
                Map2.SetActive(true);
                Opponent2.SetActive(true);
                RoundCount = 1;
                Roundstart = true;
                MatchOver = false;
                P1Info.P1Deaths = 0;
                P2Info.P2Deaths = 0;
            }
            else if (Map2.active == true)
            {
                Map2.SetActive(false);
                Opponent2.SetActive(false);
                Map3.SetActive(true);
                Opponent3.SetActive(true);
                RoundCount = 1;
                Roundstart = true;
                MatchOver = false;
                P1Info.P1Deaths = 0;
                P2Info.P2Deaths = 0;
            }
            else
            {
                UIHIDE.SetActive(false);
                Map3.SetActive(false);
                Opponent3.SetActive(false);
                Player.SetActive(false);
                FinishScreen.SetActive(true);
            }

        }
        else
        {
            //on loss return to main menu
            text.text = "Player 2 Wins";
            SceneManager.LoadScene(sceneName: "Martial Fighter Main Menu");
        }
    }
        
        
    }
    public void RoundOver()
    {
        //disables controls for both player 1 and player 2 and shows round end text or match end text
        ControlActive = false;
            text.text = "SLASH!";
            
            DisplayTimer = 1f;
        if (DisplayTimer <= 0 && RoundCount < 3 && P1Info.P1Deaths != 2 && P2Info.P2Deaths != 2)
        {
            Roundstart = true;
            P1Info.P1Death = false;
            P2Info.P2Death = false;
            RoundCount++;

        }
        if (RoundCount == 3 ||P1Info.P1Deaths == 2|P2Info.P2Deaths == 2)
        {
            MatchOver = true;
            Invoke("MatchEnd", 1f);
        }


    }
    
}
