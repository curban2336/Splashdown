using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] TrickHandler trickHandler;
    [SerializeField] float bouyancy = 6f;
    [SerializeField] float downSpeed = -11f;
    [SerializeField] float currentMovement = 0f;
    [SerializeField] float rotationDegree = 0f;
    private Rect cameraRect;
    public static bool isInWater = true;

    public GameObject dolphoMove;
    public GameObject dolphoTrick;
    public GameObject dolphoFly;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        cameraRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
        dolphoFly.SetActive(false);
        dolphoTrick.SetActive(false);
        dolphoMove.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFrameUpdate();
        if (ColorSwitcherWater.isJumping)
        {
            isInWater = false;
            if(trickHandler.water.canJumpTrigger)
            {
                dolphoMove.SetActive(false);
                dolphoFly.SetActive(true);
            }
            if(GetComponent<Rigidbody2D>().linearVelocity.y < 0f)
            {
                rotationDegree = Mathf.Lerp(rotationDegree, -45f, Time.deltaTime * 2.5f);
                this.transform.rotation = Quaternion.Euler(0f, 0f, rotationDegree);
            }
        }
        else
        {
            isInWater = true;
            dolphoMove.SetActive(true);
            dolphoFly.SetActive(false);
            dolphoTrick.SetActive(false);
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

    IEnumerator TrickCoroutine()
    {
        dolphoMove.SetActive(false);
        dolphoFly.SetActive(false);
        dolphoTrick.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        dolphoTrick.SetActive(false);
        dolphoFly.SetActive(true);
    }

    void WaterUpdate()
    {
        Vector2 desiredPosition = this.transform.position;
        if (Input.GetKey(KeyCode.Space))
        {
            currentMovement -= 60 * Time.deltaTime;
            rotationDegree = Mathf.Lerp(rotationDegree, -45f, Time.deltaTime * 2.5f);
            this.transform.rotation = Quaternion.Euler(0f, 0f, rotationDegree);
            if (currentMovement < downSpeed)
            {
                currentMovement = downSpeed;
            }
        }
        else
        {
            currentMovement += 60 * Time.deltaTime;
            rotationDegree = Mathf.Lerp(rotationDegree, 45f, Time.deltaTime * 2.5f);
            this.transform.rotation = Quaternion.Euler(0f, 0f, rotationDegree);
            if (currentMovement > bouyancy)
            {
                currentMovement = bouyancy;
            }
        }
        desiredPosition += new Vector2(0f, currentMovement * Time.deltaTime);
        //desiredPosition = Vector2.Lerp(desiredPosition, new Vector2(desiredPosition.x, currentMovement), Time.deltaTime);
        Vector2 allowedPosition = new Vector2(Mathf.Clamp(desiredPosition.x, cameraRect.xMin, cameraRect.xMax), Mathf.Clamp(desiredPosition.y, cameraRect.yMin, cameraRect.yMax));

        this.transform.position = allowedPosition;
        //currentMovement = 0f;
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
