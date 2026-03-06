using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RoomSpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    public GameObject enemyPrefab;
    public int enemiesToSpawn = 3;
    public float spawnRadiusCheck = 1f;
    public LayerMask obstacleLayer; 

    private bool hasSpawned = false;
    private BoxCollider spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpawned) 
        {
            SpawnEnemies();
            hasSpawned = true;
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 randomPos = GetValidSpawnPosition();
            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        int attempts = 0;
        while (attempts < 30)
        {
            float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
            Vector3 pos = new Vector3(x, spawnArea.bounds.center.y, z);

            if (!Physics.CheckSphere(pos, spawnRadiusCheck, obstacleLayer))
            {
                return pos;
            }
            attempts++;
        }
        return spawnArea.bounds.center; 
    }
}