using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1HealthBar : MonoBehaviour
{
    public Animator HealthAnim;
    private Player1Damage Health;
    private float HealthMarker;
    private int count;
    private RoundControl RRestart;
    private Player2Damage OpponentHealth;
    // Start is called before the first frame update
    void Start()
    {
        
        count = 1;
        Health = GameObject.FindWithTag("Player1").GetComponent<Player1Damage>();
        RRestart = GameObject.Find("Center Text").GetComponent<RoundControl>();
        OpponentHealth = GameObject.FindWithTag("Player2").GetComponent<Player2Damage>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        HealthUpdate2();
        if (RRestart.ControlActive == false && Health.P1Deaths != 0)
        {
            HealthAnim.SetTrigger("Health2,4,0");
            HealthAnim.SetTrigger("RoundRestart1"); HealthAnim.SetTrigger("RoundRestart2"); HealthAnim.SetTrigger("RoundRestart3"); HealthAnim.SetTrigger("RoundRestart4");
            HealthMarker = 100;
            count = 1;
        }
        if (RRestart.ControlActive == false )
        {
            if (OpponentHealth.P2Deaths != 0)
            {
                //Resets all health UI at start of a round no matter the win or not
                HealthAnim.Rebind();
                HealthAnim.ResetTrigger("Health2,1,75"); HealthAnim.ResetTrigger("Health2,1,50"); HealthAnim.ResetTrigger("Health2,1,25"); HealthAnim.ResetTrigger("Health2,1,0");
                HealthAnim.ResetTrigger("Health2,2,75"); HealthAnim.ResetTrigger("Health2,2,50"); HealthAnim.ResetTrigger("Health2,2,25"); HealthAnim.ResetTrigger("Health2,2,0");
                HealthAnim.ResetTrigger("Health2,3,75"); HealthAnim.ResetTrigger("Health2,3,50"); HealthAnim.ResetTrigger("Health2,3,25"); HealthAnim.ResetTrigger("Health2,3,0");
                HealthAnim.ResetTrigger("Health2,4,75"); HealthAnim.ResetTrigger("Health2,4,50"); HealthAnim.ResetTrigger("Health2,4,25"); HealthAnim.ResetTrigger("Health2,4,0");
                HealthAnim.ResetTrigger("RoundRestart1"); HealthAnim.ResetTrigger("RoundRestart2"); HealthAnim.ResetTrigger("RoundRestart3"); HealthAnim.ResetTrigger("RoundRestart4");

                Health.CurrentHealth = 100;
                count = 1;
                HealthMarker = 100;
            }
        }





    }
    public void HealthUpdate2()
    {
        
        if (Health.CurrentHealth == 100)
        {   
            HealthMarker = 100;
            

        }
        //If statements to change the health animation and sync it between the bars as health goes down and then removes the bars once they are empty
        if (Health.CurrentHealth < HealthMarker - 6.25)
        {
            HealthAnim.ResetTrigger("RoundRestart1"); HealthAnim.ResetTrigger("RoundRestart2"); HealthAnim.ResetTrigger("RoundRestart3"); HealthAnim.ResetTrigger("RoundRestart4");
            HealthAnim.ResetTrigger("Health2," + count + ",0");
            HealthAnim.SetTrigger("Health2," + count + ",75");
            
        }
        if (Health.CurrentHealth < HealthMarker - 12.5)
        {
            HealthAnim.ResetTrigger("Health2," + count + ",75");
            HealthAnim.SetTrigger("Health2," + count + ",50");
            
        }
        if (Health.CurrentHealth < HealthMarker - 18.75)
        {
            HealthAnim.ResetTrigger("Health2," + count + ",50");
            HealthAnim.SetTrigger("Health2," + count + ",25");
            
        }
        if (Health.CurrentHealth < HealthMarker - 25)
        {
            //HealthAnim.ResetTrigger("Health2," + count + ",25");
            HealthAnim.SetTrigger("Health2," + count + ",0");
            
            count += 1;
            HealthMarker -= 25;

        }



    }
}
