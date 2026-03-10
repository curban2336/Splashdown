using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    public Transform player;

    public float minY;
    public float maxY;

    void LateUpdate()
    {
        float newY = Mathf.Clamp(player.position.y, minY, maxY);

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}