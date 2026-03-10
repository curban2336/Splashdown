using UnityEngine;

public class ColorSwitcherWater : MonoBehaviour
{
    public bool canJumpTrigger = false;
    public static bool isJumping = false;

    public float thresholdY = -3f;
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

        if (transform.position.y >= thresholdY)
        {
            canJumpTrigger = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJumpTrigger)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isJumping = true;
    }
}
