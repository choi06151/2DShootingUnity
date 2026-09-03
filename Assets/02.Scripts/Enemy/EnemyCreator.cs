using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public float distanceToAnotherEnemy = 0.5f;
    public List<Enemy> EnemyPrefabs;
    public GameObject SpawnPoint;
    public float RespawnCoolTime = 2;

    private float _currentCoolTime = 0;

    public PoolingManager PoolingManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckRespawn();
    }

    private void CheckRespawn()
    {
        if (_currentCoolTime >= RespawnCoolTime)
        {
            _currentCoolTime = 0;
            CreateEnemy();
        }

        {
            _currentCoolTime += Time.deltaTime;
        }
    }

    public void CreateEnemy()
    {
        int spawnedCount = 0;
        int spawnAmount = Random.Range(1, 5);
        int randomSpawnType = Random.Range(0, EnemyPrefabs.Count);
        bool[] isCreated = new bool[5];

        while (spawnedCount <= spawnAmount)
        {
            int spawnPointIndex = Random.Range(0, 5);
            while (isCreated[spawnPointIndex])
            {
                spawnPointIndex = Random.Range(0, 5);
            }

            CreateCommand createCommand =
                new CreateCommand(this.gameObject, EnemyPrefabs[randomSpawnType].gameObject,
                    GetSpawnPoint(spawnPointIndex));
            CommandManager.Instance.ExecuteCommand(createCommand);
            isCreated[spawnPointIndex] = true;
            spawnedCount++;
        }
    }

    private Vector3 GetSpawnPoint(int index) // 0 1 2 3 4
    {
        int normalizeIndex = index - 2;
        Vector3 basicSpawnPoint = SpawnPoint.transform.position;
        float xPos = basicSpawnPoint.x + distanceToAnotherEnemy * normalizeIndex;
        Vector3 indexSpawnPoint = new Vector3(xPos, basicSpawnPoint.y, basicSpawnPoint.z);

        return indexSpawnPoint;
    }
}