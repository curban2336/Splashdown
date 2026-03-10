using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] TrickHandler trickHandler;
    [SerializeField] float bouyancy = 1f;
    [SerializeField] float downSpeed = -2f;
    private Rect cameraRect;
    public bool isInWater = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        cameraRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFrameUpdate();
        if (ColorSwitcherWater.isJumping)
        {
            isInWater = false;
        }
        else
        {
            isInWater = true;
        }
        if (isInWater)
        {
            WaterUpdate();
        }
    }

    void CameraFrameUpdate()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        cameraRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }

    void WaterUpdate()
    {
        Vector2 desiredPosition = this.transform.position;
        desiredPosition += Vector2.up * bouyancy * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            desiredPosition += Vector2.up * downSpeed * Time.deltaTime;
        }
        Vector2 allowedPosition = new Vector2(Mathf.Clamp(desiredPosition.x, cameraRect.xMin, cameraRect.xMax), Mathf.Clamp(desiredPosition.y, cameraRect.yMin, cameraRect.yMax));

        this.transform.position = allowedPosition;
    }

    public void RaiseBouyancy()
    {
        bouyancy += 3f;
    }

    public void LowerBouyancy()
    {
        bouyancy -= 3f;
    }

    public void RaiseDownSpeed()
    {
        downSpeed -= 3f;
    }

    public void LowerDownSpeed()
    {
        downSpeed += 3f;
    }
}
