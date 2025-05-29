using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 5f;
    public int maxMonsters = 20;
    private float nextSpawnTime = 0f;

    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("enemyPrefab is not assigned! Please drag Enemy.prefab into the Inspector.");
        }
        else if (!PrefabUtility.IsPartOfPrefabAsset(enemyPrefab))
        {
            Debug.LogWarning("enemyPrefab is not a prefab asset! Please use the prefab from Project window.");
        }
        else
        {
            Debug.Log("enemyPrefab is assigned and valid: " + enemyPrefab.name);
        }
    }

    void Update()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log($"Current enemy count: {monsters.Length}, Max limit: {maxMonsters}");
        if (monsters.Length >= maxMonsters) return;

        Debug.Log($"Time.time: {Time.time}, nextSpawnTime: {nextSpawnTime}");
        if (Time.time >= nextSpawnTime)
        {
            SpawnMonster();
            nextSpawnTime = Time.time + spawnInterval;
            Debug.Log($"Spawned monster, next spawn at: {nextSpawnTime}");
        }
    }

    void SpawnMonster()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("enemyPrefab is null! Cannot spawn monster.");
            return;
        }
        Debug.Log("Spawning monster with prefab: " + enemyPrefab.name);
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector2 spawnPos = (Vector2)transform.position + randomOffset;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        Debug.Log("Spawned enemy at position: " + newEnemy.transform.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}