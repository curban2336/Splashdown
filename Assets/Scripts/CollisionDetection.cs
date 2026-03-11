using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fast"))
        {
            gameObject.SendMessage("SpeedUp", 5.0);
            Debug.Log("Speed Up!");
        }
        
        if (collision.gameObject.CompareTag("Slow"))
        {
            gameObject.SendMessage("SpeedDown", 5.0);
            Debug.Log("Ouch!");
        }
    }
}
