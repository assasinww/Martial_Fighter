using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float YourTime;
    public GameObject Final;
    // Start is called before the first frame update
    void Start()
    {
        YourTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Final.active == false)
        {
            YourTime += Time.deltaTime;
        }
        
    }
}
