using UnityEngine;
using UnityEngine.UIElements;

public class ColorSwitcherWater : MonoBehaviour
{
    public bool canJumpTrigger = false;
    public static bool isJumping = false;

    public TrickHandler trickHandler;

    public static float thresholdY = 0.75f;
    public float jumpForce = 10f;
    public bool below;

    public Color normalColor = Color.white;
    public Color belowColor = Color.blue;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void BoostJump()
    {
        if (trickHandler.background.speed <= 10)
        {
            jumpForce = 20f;
        }
        else
        {
            jumpForce = 20f + (10 * Mathf.FloorToInt((trickHandler.background.speed-10) / 4));
        }
    }

    void Update()
    {
        if (transform.position.y < thresholdY)
        {
            below = true;
        }
        else if (transform.position.y > thresholdY)
        {
            below = false;
        }

        sr.color = below ? belowColor : normalColor;

        if (transform.position.y <= -4)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isJumping = false;
        }

        if (canJumpTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            BG_MoveLeft.jumpStart = false;
            BoostJump();
            Jump();
        }
        
        if (!below && !isJumping && !canJumpTrigger)
        {
            isJumping = true;
            canJumpTrigger = true;
            BG_MoveLeft.jumpStart = true;
        }
    }

    void Jump()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        trickHandler.ActivateTrickTime();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        canJumpTrigger = false;
    }
}
