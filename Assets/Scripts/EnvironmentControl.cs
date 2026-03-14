using System.Collections;
using UnityEngine;

public class EnvironmentControl : MonoBehaviour
{
    public Sprite[] cloudSprites;
    public GameObject[] cloudPrefabs;
    public string cloudTemplateTag;

    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 1.75f;
    public float[] spawnHeights = new float[] { 20f, 25f, 30f };
    public float spawnXOffset = 18f;
    public float cloudScale = 0.3f;
    public float depthDeviation = 0.02f;
    public float destroyXOffset = 17f;

    Transform camTransform;
    BG_MoveLeft bgMover;
    GameObject cloudTemplate;

    void Start()
    {
        camTransform = Camera.main != null ? Camera.main.transform : null;
        bgMover = FindObjectOfType<BG_MoveLeft>();

        if ((cloudSprites == null || cloudSprites.Length == 0) && (cloudPrefabs == null || cloudPrefabs.Length == 0) && string.IsNullOrEmpty(cloudTemplateTag))
            Debug.LogWarning("EnvironmentControl: no cloud sprites or prefabs or template tag assigned.");

        if (spawnHeights == null || spawnHeights.Length == 0)
            spawnHeights = new float[] { 20f, 25f, 30f };

        if (!string.IsNullOrEmpty(cloudTemplateTag) && (cloudPrefabs == null || cloudPrefabs.Length == 0))
        {
            cloudTemplate = GameObject.FindWithTag(cloudTemplateTag);
            if (cloudTemplate == null)
                Debug.LogWarning($"EnvironmentControl: no GameObject found with tag '{cloudTemplateTag}'.");
            else
                cloudTemplate.SetActive(false);
        }

        StartCoroutine(SpawnCloudsLoop());
    }

    IEnumerator SpawnCloudsLoop()
    {
        while (true)
        {
            float wait = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(wait);
            SpawnCloud();
        }
    }

    void SpawnCloud()
    {
        float chosenHeight = spawnHeights[Random.Range(0, spawnHeights.Length)];
        float zOffset = Random.Range(-depthDeviation, depthDeviation);
        float spawnX = (camTransform != null) ? camTransform.position.x + spawnXOffset : transform.position.x + spawnXOffset;
        Vector3 spawnPos = new Vector3(spawnX, chosenHeight, zOffset);

        GameObject spawned = null;

        if (cloudPrefabs != null && cloudPrefabs.Length > 0)
        {
            GameObject prefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];
            if (prefab != null)
            {
                spawned = Instantiate(prefab, spawnPos, Quaternion.identity);
                spawned.transform.localScale = Vector3.one * cloudScale;
            }
        }
        else if (cloudTemplate != null)
        {
            spawned = Instantiate(cloudTemplate, spawnPos, Quaternion.identity);
            spawned.SetActive(true);
            spawned.transform.localScale = Vector3.one * cloudScale;
        }
        else if (cloudSprites != null && cloudSprites.Length > 0)
        {
            Sprite chosen = cloudSprites[Random.Range(0, cloudSprites.Length)];
            GameObject runtimeCloud = new GameObject("Cloud");
            runtimeCloud.transform.position = spawnPos;
            runtimeCloud.transform.localScale = Vector3.one * cloudScale;
            SpriteRenderer runtimeSr = runtimeCloud.AddComponent<SpriteRenderer>();
            runtimeSr.sprite = chosen;
            CloudMover runtimeMover = runtimeCloud.AddComponent<CloudMover>();
            runtimeMover.camTransform = camTransform;
            runtimeMover.bgMover = bgMover;
            runtimeMover.destroyXOffset = destroyXOffset;
            spawned = runtimeCloud;
        }

        if (spawned == null) return;

        CloudMover mover = spawned.GetComponent<CloudMover>();
        if (mover != null)
        {
            mover.camTransform = camTransform;
            mover.bgMover = bgMover;
            mover.destroyXOffset = destroyXOffset;
        }
    }
}