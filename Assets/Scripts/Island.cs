using UnityEngine;

public class Island : MonoBehaviour
{
    public GameObject island;
    public GameObject echoLocation;
    public Vector3 startPos = new Vector3(67.9800034f, -1.22815001f, -0.1f);
    public BG_MoveLeft background;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        echoLocation.SetActive(false);
        transform.position = startPos;
        Undertow.sendIsland = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Undertow.sendIsland)
        {
            echoLocation.SetActive(true);
            if (transform.position.x < startPos.x - 120)
            {
                transform.position = startPos;
                Undertow.sendIsland = false;
                echoLocation.SetActive(false);
            }

            if (!ColorSwitcherWater.isJumping && !MineTwoScript.jumpStart)
            {
                transform.Translate(Vector3.left * Time.deltaTime * background.speed);
            }

            if (ColorSwitcherWater.isJumping && MineTwoScript.jumpStart)
            {
                transform.Translate(Vector3.left * Time.deltaTime * background.speed / 2);
            }

            if (ColorSwitcherWater.isJumping && !MineTwoScript.jumpStart)
            {
                transform.Translate(Vector3.left * Time.deltaTime * background.speed);
            }
        }
    }
}
