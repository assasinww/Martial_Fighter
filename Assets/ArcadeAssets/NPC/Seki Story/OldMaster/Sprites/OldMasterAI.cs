using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMasterAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    private int DecisionNumber;
    private GameObject Opponent;
    private FaceOpponent Flipped;
    private float Cooldown1; private float Cooldown2;
    private float movespeed; public bool P2Blocking;
    private float range;
    private bool Attacking = false; private bool Attacked = false;
    private float attackRange1 = 1.65f;
    private float attackRange2 = 1.65f;
    public Transform attackPoint1;
    public Transform attackPoint2;
    public LayerMask OpponentLayer;
    private SekiControl Grab;
    private Player2Damage Dead;
    private RoundControl ControlsActive;


    private void Awake()
    {
        Opponent = GameObject.FindWithTag("Player1");
        Flipped = GameObject.FindWithTag("Player2").GetComponent<FaceOpponent>();
        rb = GetComponent<Rigidbody2D>();
        Grab = Opponent.GetComponent<SekiControl>();
        P2Blocking = false;
        ControlsActive = GameObject.Find("Center Text").GetComponent<RoundControl>();
        Dead = GetComponent<Player2Damage>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ControlsActive.ControlActive == true && ControlsActive.MatchOver == false)
        {
            animator.SetTrigger("RoundStart");
            //cooldowns for attack and detecting whether ai is attacking or not
            if (Cooldown1 >= 0)
            {
                Cooldown1 -= Time.deltaTime;

            }
            if (Cooldown2 >= 0)
            {
                Cooldown2 -= Time.deltaTime;

            }
            
            if (Cooldown1 <= 0 && Cooldown2 <= 0)
            {
                Attacking = false;
            }
            //The Decision function is simply a random number generator which is the AI's basic decision making on top of situation this is so that the ai does not play perfectly
            Invoke("Decision", 1f);
            Vector3 Pos = transform.position;
            //calculates the range as a secondary condition for actions
            range = Vector2.Distance(Pos, Opponent.transform.position);
            //reads the player's inputs to make the ai block attacks depending on whether the Decision number and attacked bool match
            if (range < 2.6f && Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.J))
            {
                Attacked = true;
            }
            else if (Attacked == true && range > 2.6f)
            {
                Attacked = false;
            }
            //attacks 1 and 2 which activate depending on cooldown, current conditions e.g already attacking or being grabbed or if attack is on cooldown
            if (range <= 2.7f && Attacking == false && Grab.P1Grabbing == false && Attacked == false)
            {
                if (DecisionNumber == 7 && Cooldown1 <= 0)
                {
                    Cooldown1 = 0.8f;
                    animator.SetTrigger("Attack1");
                    Attacking = true;
                    Invoke("Attack1", 1.2f);
                }
                if (DecisionNumber == 6 && Cooldown2 <= 0)
                {
                    Cooldown1 = 1f;
                    animator.SetTrigger("Attack2");
                    Attacking = true;
                    Invoke("Attack2", 1.5f);

                }
            }
        }

    }
    //fixed update generally used for physics based movement
    private void FixedUpdate()
    {
        if (ControlsActive.ControlActive == true && ControlsActive.MatchOver == false)
        {
            Vector3 Pos = transform.position;
            //moving backwards/blocking set to 3 different Decision Numbers so that it doesnt happen 100% of attacks used by the player but still blocks enough for it to be challenging
            if (DecisionNumber == 2 || DecisionNumber == 4 || DecisionNumber == 6)
            {
                if (Attacked == true && Flipped.flip == false && Attacking == false && Grab.P1Grabbing == false)
                {
                    movespeed = 0.9f;
                    animator.SetTrigger("IsRunning2");
                    rb.AddForce(new Vector2(1.5f * movespeed, 0), ForceMode2D.Impulse);
                    P2Blocking = true;
                }
                else if (Attacked == true && Flipped.flip == true && Attacking == false && Grab.P1Grabbing == false)
                {
                    movespeed = 0.9f;
                    animator.SetTrigger("IsRunning2");
                    rb.AddForce(new Vector2(-1.5f * movespeed, 0), ForceMode2D.Impulse);
                    P2Blocking = true;
                }
                else if (range >= 2.6f)
                {
                    animator.ResetTrigger("IsRunning2");
                    P2Blocking = false;
                }

            }
            //if Statement based on Decision number and current conditions which control the AI's movement towards the player to use attack1 or attack2
            if (DecisionNumber == 1 || DecisionNumber == 3 || DecisionNumber == 5)
            {
                if (Attacked == false && Flipped.flip == false && Attacking == false && range > 2.7f && Grab.P1Grabbing == false)
                {
                    movespeed = 1.4f;
                    animator.SetTrigger("IsRunning1");
                    rb.AddForce(new Vector2(-1.5f * movespeed, 0), ForceMode2D.Impulse);
                }
                else if (Attacked == false && Flipped.flip == true && Attacking == false && range > 2.7f && Grab.P1Grabbing == false)
                {
                    movespeed = 1.4f;
                    animator.SetTrigger("IsRunning1");
                    rb.AddForce(new Vector2(1.5f * movespeed, 0), ForceMode2D.Impulse);
                }
                else if (range <= 2.7f)
                {
                    animator.ResetTrigger("IsRunning1");

                }
            }
            
            //if the AI gets grabbed it gets thrown a distance
            if (Grab.P1Grabbing == true)
            {
                animator.SetTrigger("Thrown");
                rb.AddForce(new Vector2(20, 10), ForceMode2D.Impulse);
            }
        }





    }
    private void Attack1()
    {
        Cooldown1 = 1f;
        //plays attack animation

        //detects the opponent in range of the animation itself
        Collider2D[] HitOpponent = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange1, OpponentLayer);
        //startup time for the hitbox so it matches the attack animation
        {
            foreach (Collider2D Opponent in HitOpponent)
            {
                //deals the damage to opponent
                Opponent.GetComponent<Player1Damage>().TakeDamage(2f);
                Cooldown1 = 0.5f;
            }
        }
    }
    private void Attack2()
    {
        Cooldown1 = 1.3f;
        //plays attack animation

        //detects the opponent in range of the animation itself
        Collider2D[] HitOpponent = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange2, OpponentLayer);
        //startup time for the hitbox so it matches the attack animation
        {
            foreach (Collider2D Opponent in HitOpponent)
            {
                //deals the damage to opponent
                Opponent.GetComponent<Player1Damage>().TakeDamage(3.5f);
                Cooldown1 = 0.7f;
            }
        }
    }
    private void Decision()
    {
        DecisionNumber = Random.Range(1, 9);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint1 == null)
            return;
        if (attackPoint2 == null)
            return;


        Gizmos.DrawWireSphere(attackPoint1.position, attackRange1);
        Gizmos.DrawWireSphere(attackPoint2.position, attackRange2);

    }
}
