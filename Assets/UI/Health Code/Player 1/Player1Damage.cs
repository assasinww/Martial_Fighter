using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player1Damage : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public bool P1Death = false;
    private SekiControl Blocking;
    public int P1Deaths = 0;
    private Player2Damage P2Health;
    private Timer RoundTime;
    private Animator SekiAnimation;
    private RoundControl RRestart;
    // Start is called before the first frame update
    void Start()
    {
        SekiAnimation = GameObject.Find("SekiChar").GetComponent<Animator>();
        Blocking = gameObject.GetComponent<SekiControl>();
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;
        P2Health = GameObject.FindWithTag("Player2").GetComponent<Player2Damage>();
        RoundTime = GameObject.Find("Timer(Black)").GetComponent <Timer>();
        RRestart = GameObject.Find("Center Text").GetComponent<RoundControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // compares health on round timer
        if (RoundTime.RoundTimer <= 0)
        {
            if (CurrentHealth > P2Health.CurrentHealth)
            {
                P2Health.P2Deaths++;
                P2Health.P2Death = true;
                
            }
            else if (CurrentHealth < P2Health.CurrentHealth)
            {
                P1Deaths++;
                P1Death = true;
            }
            else
            {
                P1Deaths++;
                P2Health.P2Deaths++;
                P1Death = true;
                P2Health.P2Death = true;

            }
        }


        if (CurrentHealth <= 0)
        {
            SekiAnimation.SetTrigger("IsDead");
            Die();
            CurrentHealth = 100;
            
            if (P1Deaths < 2)
            {
                SekiAnimation.SetTrigger("RoundRestart");
            }
        }

    }
    public void TakeDamage(float Damage)
    {
        Debug.Log("OOH ME LEG");
        if (Blocking.P1Blocking == false)
        {
            SekiAnimation.SetTrigger("IsHit");
            CurrentHealth -= Damage;
        }
 
    }
    private void Die()
    {
        P1Death = true;
        P1Deaths++;
        RRestart.RoundOver();
    }
    
}
