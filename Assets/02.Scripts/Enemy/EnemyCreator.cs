using System.Collections.Generic;
using UnityEngine;


enum EnemyType
{
    Normal,
    Homing,
    ToPlayerDirection
}

public class EnemyCreator : MonoBehaviour
{
    [Header("적과의 간격")] public float distanceToAnotherEnemy = 0.5f;
    [Header("스폰될 적")] public List<EnemyMove> EnemyPrefabs;
    [Header("스폰 위치")] public GameObject SpawnPoint;
    [Header("스폰 간격 - 시작 끝")] public float[] RespawnCoolTimeSet = new float[2] { 1, 4 };


    private float _nextSpawnTime = 3;
    private float _currentCoolTime = 0;

    private Player _player;
    public Player GetPlayer => _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindFirstObjectByType<Player>();
        if (_player == null)
            Debug.Log("플레이어 없음");
    }

    // Update is called once per frame
    void Update()
    {
        CheckRespawnEnable();
    }

    private void CheckRespawnEnable()
    {
        if (_currentCoolTime >= _nextSpawnTime)
        {
            _currentCoolTime = 0;
            CreateEnemy();
        }

        {
            _currentCoolTime += Time.deltaTime;
        }
    }

    // TODO:Scriptable을 통한 확률 모듈화 

    public void CreateEnemy()
    {
        int spawnedCount = 0;
        int spawnAmount = Random.Range(1, 5);
        EnemyType spawnType = GetRandomSpawnType();
        bool[] isCreated = new bool[5];

        while (spawnedCount <= spawnAmount)
        {
            int spawnPointIndex = Random.Range(0, 5);
            while (isCreated[spawnPointIndex])
            {
                spawnPointIndex = Random.Range(0, 5);
            }

            CreateCommand createCommand =
                new CreateCommand(this.gameObject, EnemyPrefabs[(int)spawnType].gameObject,
                    GetSpawnPoint(spawnPointIndex));
            CommandManager.Instance.ExecuteCommand(createCommand);
            createCommand.GetCreatedObeject.GetComponent<Enemy>().InitEnemy(this);
            isCreated[spawnPointIndex] = true;
            spawnedCount++;
        }

        ChangeRespawnTime();
    }

    private Vector3 GetSpawnPoint(int index) // 0 1 2 3 4
    {
        int normalizeIndex = index - 2;
        Vector3 basicSpawnPoint = SpawnPoint.transform.position;
        float xPos = basicSpawnPoint.x + distanceToAnotherEnemy * normalizeIndex;
        Vector3 indexSpawnPoint = new Vector3(xPos, basicSpawnPoint.y, basicSpawnPoint.z);

        return indexSpawnPoint;
    }

    private EnemyType GetRandomSpawnType()
    {
        int randomIdx = Random.Range(0, 10);
        if (randomIdx >= 0 && randomIdx < 5)
        {
            return EnemyType.Normal;
        }
        else if (randomIdx >= 5 & randomIdx < 8)
        {
            return EnemyType.ToPlayerDirection;
        }
        else
        {
            return EnemyType.Homing;
        }
    }

    private void ChangeRespawnTime()
    {
        _nextSpawnTime = Random.Range(RespawnCoolTimeSet[0], RespawnCoolTimeSet[1]);
    }
}