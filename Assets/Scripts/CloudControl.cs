using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [HideInInspector] public Transform camTransform;
    [HideInInspector] public BG_MoveLeft bgMover;

    public float destroyXOffset = 17f;

    void Start()
    {
        if (camTransform == null && Camera.main != null)
            camTransform = Camera.main.transform;

        if (bgMover == null)
            bgMover = FindObjectOfType<BG_MoveLeft>();
    }

    void Update()
    {
        float speed = (bgMover != null) ? bgMover.speed : 10f;
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (camTransform != null && transform.position.x < camTransform.position.x - destroyXOffset)
            Destroy(gameObject);
    }
}
