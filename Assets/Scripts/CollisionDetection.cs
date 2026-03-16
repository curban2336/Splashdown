using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public List<BG_MoveLeft> parallax;
    public MineTwoScript mineTwoScript;
    public MineOneScript mineOneScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Fast"))
        {
            foreach (BG_MoveLeft bg in parallax)
            {
                bg.SpeedUp();
            }
            mineOneScript.SendMessage("SpeedUp", 5.0);
            mineTwoScript.SendMessage("SpeedUp", 5.0);
        }
        
        if (collision.gameObject.CompareTag("Slow"))
        {
            foreach (BG_MoveLeft bg in parallax)
            {
                bg.SpeedDown();
            }
            mineOneScript.SendMessage("SpeedDown", 5.0);
            mineTwoScript.SendMessage("SpeedDown", 5.0);
        }

        if (collision.gameObject.CompareTag("Island"))
        {
            foreach (BG_MoveLeft bg in parallax)
            {
                bg.speed = 1;
            }
            mineOneScript.SendMessage("SpeedDown", 5.0);
            mineTwoScript.SendMessage("SpeedDown", 5.0);
        }
    }
}
