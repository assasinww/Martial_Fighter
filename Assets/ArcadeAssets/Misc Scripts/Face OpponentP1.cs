using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceOpponentP1 : MonoBehaviour
{
    private GameObject Opponent;
    public bool flip = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Opponent = GameObject.FindWithTag("Player2");
        //allows for the player and opponent to always be facing each other by multiplying the X scale by -1 which also allows all hitboxes to rotate
        Vector3 characterScale = transform.localScale;
        if (Opponent.transform.position.x < transform.position.x && flip == false)
        {
            Debug.Log("turn");
            characterScale.x *= -1;
            flip = true;
        }
        else if (Opponent.transform.position.x > transform.position.x && flip == true)
        {
            flip = false;
            characterScale.x *= -1;
        }
        transform.localScale = characterScale;



    }
}
