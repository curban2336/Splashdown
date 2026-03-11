using UnityEngine;

public class ColorSwitcherWater : MonoBehaviour
{
    public bool canJumpTrigger = false;
    public static bool isJumping = false;

    public TrickHandler trickHandler;

    public static float thresholdY = -0.5f;
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
            rb.simulated = false;
            isJumping = false;
            canJumpTrigger = false;
        }

        if (canJumpTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            BG_MoveLeft.jumpStart = false;
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
        rb.simulated = true;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        trickHandler.ActivateTrickTime(3);
    }
}
