using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineOneScript : MonoBehaviour
{
    public float speed = 10;
    public float deathSpeedThreshold = 2f; 
    public static bool jumpStart = false;

    [SerializeField] private GameObject deathUIPanel; // assign the Canvas object (panel/text) in Inspector

    Vector3 startPos;
    float repeatWidth;
    bool isDead = false;

    void Awake()
    {
        // Reset static state so previous run won't interfere after a reload
        jumpStart = false;
        ColorSwitcherWater.isJumping = false;
        Time.timeScale = 1f;
    }

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;

        // Ensure the death UI is disabled at start (safe-guard)
        if (deathUIPanel != null)
        {
            deathUIPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        if (transform.position.x < startPos.x - 30)
        {
            transform.position = startPos;
            gameObject.SendMessage("MineReachEnd", 5.0);
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
        
        if (speed < deathSpeedThreshold)
        {
            StartCoroutine(HandleDeath());
        }
        
    }
    
    public void SpeedUp()
    {
        speed += 1;
    }

    public void SpeedDown()
    {
        speed -= 1;
    }

    public void Restart()
    {
        // Reset static state before reloading to avoid persisted state issues
        jumpStart = false;
        ColorSwitcherWater.isJumping = false;
        Time.timeScale = 1f;
    }

    public void QuitMenu()
    {
        // Reset static state before switching scenes
        jumpStart = false;
        ColorSwitcherWater.isJumping = false;
        Time.timeScale = 1f;
    }

    public void RollHeightOneMove(int RollHeightOne)
    {
        if(RollHeightOne == 1)
        {
            
        }
    }

    IEnumerator HandleDeath()
    {
        isDead = true;

        if (deathUIPanel != null)
            deathUIPanel.SetActive(true);

        // Optional: pause game (uncomment if desired)
        // Time.timeScale = 0f;

        yield return null;
    }
}
