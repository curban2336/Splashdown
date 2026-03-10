using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BG_MoveLeft : MonoBehaviour
{
    public float speed = 10;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate (Vector3.left * Time.deltaTime * speed);

        if (transform.position.x < startPos.x - 16)
        {
            transform.position = startPos;
        }
            
    }
}
