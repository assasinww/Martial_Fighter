

using UnityEngine;

public class SekiControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movespeed;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    public Animator animator;
    public Transform attackPoint1; 
    public Transform attackPoint2;
    public Transform GrabPoint;
    private float attackRange1 = 1.65f;
    private float attackRange2 = 1.65f;
    private float GrabRange = 0.8f;
    public LayerMask OpponentLayer;
    public bool P1Blocking;
    private float Cooldown1 = 0f; private float Cooldown2 = 0f; private float DashCooldown; private float CooldownG = 0f;
    private float ButtonCool; private int ButtonCount;
    private FaceOpponentP1 Flipped;
    public bool P1Grabbing;
    private RoundControl ControlsActive;

    // Start is called before the first frame update
    void Start()
    {
        jumpForce = 45f;
        isJumping = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        Flipped = GetComponent<FaceOpponentP1>();
        P1Grabbing = false;
        ControlsActive = GameObject.Find("Center Text").GetComponent<RoundControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //only allows actions once the round text has passed
        if (ControlsActive.ControlActive == true && ControlsActive.MatchOver == false)
        {
            //All cooldowns (recovery frames) kept track here
            if (Cooldown1 > 0)
            {
                Cooldown1 -= Time.deltaTime;
            }
            if (Cooldown2 > 0)
            {
                Cooldown2 -= Time.deltaTime;
            }
            if (CooldownG > 0)
            {
                CooldownG -= Time.deltaTime;
                P1Grabbing = false;
            }
            if (DashCooldown > 0)
            {
                DashCooldown -= Time.deltaTime;
            }


            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");
            //attack1 Input
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (isJumping == false && P1Blocking == false && Cooldown1 <= 0 && Cooldown2 <= 0 && CooldownG <= 0)
                {

                    animator.SetTrigger("OnBUTTON1");
                    Cooldown1 = 0.5f;
                    //using invoke so the hitbox appears inline with the animation
                    Invoke("Attack1", 0.1f);
                }
            }
            //attack 2 Input
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (isJumping == false && P1Blocking == false && Cooldown1 <= 0 && Cooldown2 <= 0 && CooldownG <= 0)
                {
                    animator.SetTrigger("OnBUTTON2");
                    Cooldown2 = 1f;
                    //using invoke so the hitbox appears inline with the animation
                    Invoke("Attack2", 0.4f);
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (isJumping == false && P1Blocking == false && Cooldown1 <= 0 && Cooldown2 <= 0 && CooldownG <= 0)
                {
                    //using the two attack animations in tandem for a grab animation
                    CooldownG = 1.3f;
                    Invoke("Grab", 0.2f);

                }
            }
            if (ButtonCool >= 0)
            {

                ButtonCool -= 1 * Time.deltaTime;
            }
            else
            {
                ButtonCount = 0;

            }
            //double tap input read for dashing
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) && DashCooldown <= 0)
            {

                if (ButtonCool > 0 && ButtonCount == 1)
                {
                    //Has double tapped
                    DashCooldown = 0.3f;
                    rb.AddForce(new Vector2(moveHorizontal * movespeed * 13f, jumpForce / 2.2f), ForceMode2D.Impulse);
                    ButtonCount = 0;
                }
                else
                {
                    ButtonCool = 0.2f;
                    ButtonCount += 1;
                }

            }

        }

    }
    //fixed update for physics
    void FixedUpdate()
    {
        //only allows actions once the round text has passed
        if (ControlsActive.ControlActive == true && ControlsActive.MatchOver == false)
        {
            //checking if player is inputting left or right
            if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
            {
                //Causes the Boolean triggers for IsRunning1 and 2 to be true
                if (moveHorizontal > 0.1f && Flipped.flip == false)
                {
                    movespeed = 1.4f;
                    animator.SetTrigger("IsRunning1");
                }
                if (moveHorizontal < -0.1f && Flipped.flip == false)
                {

                    movespeed = 0.9f;
                    animator.SetTrigger("IsRunning2");
                    P1Blocking = true;
                }
                if (moveHorizontal > 0.1f && Flipped.flip == true)
                {
                    movespeed = 0.9f;
                    animator.SetTrigger("IsRunning2");
                }
                if (moveHorizontal < -0.1f && Flipped.flip == true)
                {

                    movespeed = 1.4f;
                    animator.SetTrigger("IsRunning1");
                    P1Blocking = true;
                }
                rb.AddForce(new Vector2(moveHorizontal * movespeed, 0f), ForceMode2D.Impulse);

            }
            //statement to reset the boolean trigger IsRunning1 and 2 so the run animation resets and bloccking trigger
            else
            {
                animator.ResetTrigger("IsRunning1");
                animator.ResetTrigger("IsRunning2");
                P1Blocking = false;
            }



            //checks if player is jumping
            if (moveVertical > 0.1f && isJumping == false)
            {
                rb.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            }
        }
        
        
    }
    //Checks if player is on the ground
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            animator.ResetTrigger("Jump");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
            animator.SetTrigger("Jump");
        }
    }
    void Attack1()
    {

        Cooldown1 = 0.5f;
        //plays attack animation
        
        //detects the opponent in range of the animation itself
        Collider2D[] HitOpponent = Physics2D.OverlapCircleAll(attackPoint1.position,attackRange1,OpponentLayer);
        //startup time for the hitbox so it matches the attack animation
        
        
            {
                foreach (Collider2D Opponent in HitOpponent)
                {
                    //deals the damage to opponent
                    Opponent.GetComponent<Player2Damage>().TakeDamage(2.5f);
                    Cooldown1 = 0.25f;
                }
            } 
        
        
        
        
        
    }
    void Attack2()
    {
        Cooldown2 = 1f;
        //plays attack animation
        
        //detects the opponent in range of the animation itself
        Collider2D[] HitOpponent = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange2, OpponentLayer);
        //deals the damage
        foreach (Collider2D Opponent in HitOpponent)
        {
            //deals the damage to opponent

            Opponent.GetComponent<Player2Damage>().TakeDamage(4);
            Cooldown2 -= 0.5f;

        }
    }
    void Grab()
    {
        animator.SetTrigger("GrabBtn");
        CooldownG = 1.3f;
        //plays attack animation

        //detects the opponent in range of the animation itself
        Collider2D[] HitOpponent = Physics2D.OverlapCircleAll(GrabPoint.position, GrabRange, OpponentLayer);
        //deals the damage
        foreach (Collider2D Opponent in HitOpponent)
        {
            //deals the damage to opponent
            Opponent.GetComponent<Player2Damage>().TakeDamage(7.5f);
            P1Grabbing = true;
            CooldownG -= 0.3f;

        }
    }
    //for testing attack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint1 == null)
            return;
        if (attackPoint2 == null)
            return;
        if (GrabPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint1.position, attackRange1);
        Gizmos.DrawWireSphere(attackPoint2.position, attackRange2);
        Gizmos.DrawWireSphere(GrabPoint.position, GrabRange);
    }
    
}
