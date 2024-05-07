using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Damage : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    private HuntressAI Blocking;

    public bool P2Death = false;
    public int P2Deaths = 0;
    private SekiControl Grab;
    private GameObject ControlRound;
    private Animator P2Anim;
    private RoundControl RRestart;
    // Start is called before the first frame update
    void Start()
    {
        
        Blocking = gameObject.GetComponent<HuntressAI>();
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;
        Grab = GameObject.FindWithTag("Player1").GetComponent<SekiControl>();
        RRestart = GameObject.Find("Center Text").GetComponent<RoundControl>();

    }

    // Update is called once per frame
    void Update()
    {
        P2Anim = GameObject.FindWithTag("Player2Anim").GetComponent<Animator>();
        if (CurrentHealth <= 0)
        {
            P2Anim.SetTrigger("IsDead");
            Die();
            CurrentHealth = 100;

            if (P2Deaths < 2)
            {
                P2Anim.SetTrigger("RoundRestart");
            }
        }
    }
    public void TakeDamage(float Damage)
    {
        if (Blocking.P2Blocking == false|| Grab.P1Grabbing == true)
        {
            P2Anim.SetTrigger("IsHit");
            CurrentHealth -= Damage;
        }  
    }
    void Die()
    {
        P2Death = true;
        P2Deaths++;
        RRestart.RoundOver();

    }
}
