using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BG_MoveLeft : MonoBehaviour
{
    public static float speed = 10;
    public float deathSpeedThreshold = 2f; 
    public static bool jumpStart = false;

    [SerializeField] private GameObject deathUIPanel; // assign the Canvas object (panel/text) in Inspector

    Vector3 startPos;
    float repeatWidth;
    bool isDead = false;

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;

        // Ensure the death UI is disabled at start (safe-guard)
        if (deathUIPanel != null)
            deathUIPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

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
        SceneManager.LoadScene("LevelTest Scene");
    }

    public void QuitMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
