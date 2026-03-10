using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BG_MoveLeft : MonoBehaviour
{
    public float speed = 10;
    public static bool jumpStart = false;
    Vector3 startPos;
    float repeatWidth;

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.x < startPos.x - 16)
        {
            transform.position = startPos;
        }
        if(!ColorSwitcherWater.isJumping && !jumpStart)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    
        if(ColorSwitcherWater.isJumping && jumpStart)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed / 2);
        }

        if (ColorSwitcherWater.isJumping && !jumpStart)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
