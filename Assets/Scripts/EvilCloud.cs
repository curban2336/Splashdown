using UnityEngine;

public class EvilCloud : MonoBehaviour
{
    public BG_MoveLeft WavesVer3_0;
    public GameObject camera;
    public float Startpos = -11;
    public float Warnpos = -6;
    public float Endpos = -2;
    public Vector3 Currentpos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(-11, camera.transform.position.y, -1);
        Currentpos = new Vector3(-11, camera.transform.position.y, -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (WavesVer3_0.speed > 10)
        {
            Currentpos = new Vector3(-11, camera.transform.position.y, -1);
        }
        if (WavesVer3_0.speed > 10)
        {
            transform.position = new Vector3(-11, camera.transform.position.y, -1);
        }

        if (WavesVer3_0.speed < 10 && WavesVer3_0.speed > 5)
        {
            transform.position = new Vector3(-6, camera.transform.position.y, -1);
        }

        if (WavesVer3_0.speed < 5)
        {
            transform.position = new Vector3(-2, camera.transform.position.y, -1);
        }
    }
}