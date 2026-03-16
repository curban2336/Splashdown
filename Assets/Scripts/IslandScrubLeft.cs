using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandScrubLeft : MonoBehaviour
{
    [Header("Prefabs / Pool")]
    public GameObject[] islandPrefabs;
    public int poolSize = 8;
    public int maxActiveIslands = 4;

    public float spawnX = 19f;
    public float despawnX = -17f;
    public float baseY = -1.5f;
    public float depthVariation = 0.25f;

    [Header("Timing & visual")]
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;
    public float minScale = 0.85f;
    public float maxScale = 1.1f;

    [Header("Speed fallback")]
    public float fallbackSpeed = 5f;

    private List<GameObject> pool = new List<GameObject>();
    private BG_MoveLeft bgMover;
    private Coroutine spawnRoutine;

    void Awake()
    {
        if (islandPrefabs == null || islandPrefabs.Length == 0)
        {
            Debug.LogWarning("IslandScrubLeft: no islandPrefabs assigned.");
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            var prefab = islandPrefabs[Random.Range(0, islandPrefabs.Length)];
            var go = Instantiate(prefab, new Vector3(despawnX - 10f, baseY, 0f), Quaternion.identity, transform);
            go.SetActive(false);
            pool.Add(go);
        }
    }

    void Start()
    {
        bgMover = FindObjectOfType<BG_MoveLeft>();
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        float speed = (bgMover != null) ? bgMover.speed : fallbackSpeed;
        float move = speed * Time.deltaTime;

        for (int i = 0; i < pool.Count; i++)
        {
            var go = pool[i];
            if (!go.activeSelf) continue;

            go.transform.Translate(Vector3.left * move, Space.World);

            if (go.transform.position.x < despawnX)
            {
                Recycle(go);
            }
        }
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(Random.Range(0.2f, 1f));

        while (true)
        {
            while (ActiveCount() >= maxActiveIslands)
                yield return null;

            SpawnOne();

            float wait = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(wait);
        }
    }

    private void SpawnOne()
    {
        var slot = GetInactiveFromPool();
        if (slot == null)
        {
            slot = pool[0];
        }

        slot.SetActive(true);

        float y = baseY + Random.Range(-depthVariation, depthVariation);

        var variant = slot.GetComponent<IslandVariant>();
        if (variant != null)
        {
            y += variant.yOffset;
        }

        slot.transform.position = new Vector3(spawnX, y, 7.9f);

        float s = Random.Range(minScale, maxScale);
        slot.transform.localScale = new Vector3(s, s, 1f);

        // Ensure the spawned island keeps its assigned Sorting Layer, but force Order in Layer to 0
        // (this prevents spawner logic from changing layer membership while ensuring Order is fixed)
        var renderers = slot.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var r in renderers)
        {
            // keep r.sortingLayerName as-is, set order to 0
            r.sortingOrder = 0;
        }
    }

    private GameObject GetInactiveFromPool()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
                return pool[i];
        }
        return null;
    }

    private int ActiveCount()
    {
        int c = 0;
        for (int i = 0; i < pool.Count; i++) if (pool[i].activeSelf) c++;
        return c;
    }

    private void Recycle(GameObject go)
    {
        go.SetActive(false);
        // move it off-screen to avoid accidental collisions while inactive
        go.transform.position = new Vector3(despawnX - 17f, baseY, 7.9f);
    }

    // optional helper: force spawn (callable by other scripts)
    public void ForceSpawn()
    {
        SpawnOne();
    }

    void OnDisable()
    {
        if (spawnRoutine != null) StopCoroutine(spawnRoutine);
    }
}
