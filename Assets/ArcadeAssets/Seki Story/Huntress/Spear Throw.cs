using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SpearThrow : MonoBehaviour
{
    public GameObject Opponent;
    public GameObject Huntress;
    private bool flip = false;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Huntress = GameObject.Find("Huntress");
        Timer = 3f;
        Opponent = GameObject.FindWithTag("Player1");
        Vector3 characterScale = transform.localScale;
        Vector3 temp = transform.position;
        transform.position = new Vector3(Huntress.transform.position.x - 2f, temp.y, temp.z);
        if (Opponent.transform.position.x > transform.position.x && flip == false)
        {
            Debug.Log("turn");
            characterScale.x *= -1;
            flip = true;
            transform.position = new Vector3(Huntress.transform.position.x + 2f,temp.y,temp.z);
            
        }
        else if (Opponent.transform.position.x < transform.position.x && flip == true)
        {
            flip = false;
            characterScale.x *= -1;
        }
        transform.localScale = characterScale;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0) 
        {
            Destroy(gameObject);
        }
        if (flip == true)
        {
            transform.Translate(-7f*Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(7f*Time.deltaTime, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Opponent.GetComponent<Player1Damage>().TakeDamage(5f);
        Destroy(gameObject);
    }
}
