using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public float distanceToAnotherEnemy = 0.5f;
    public GameObject EnemyPrefab;
    public GameObject SpawnPoint;
    public float RespawnCoolTime = 2;

    private float _currentCoolTime = 0;

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
        CreateCommand createCommand = new CreateCommand(this.gameObject, EnemyPrefab, GetRandomSpawnPoint());
        CommandManager.Instance.ExecuteCommand(createCommand);
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 basicSpawnPoint = SpawnPoint.transform.position;
        int randomIndex = Random.Range(-2, 2);
        float xPos = basicSpawnPoint.x + distanceToAnotherEnemy * randomIndex;
        Vector3 randomSpawnPoint = new Vector3(xPos, basicSpawnPoint.y, basicSpawnPoint.z);

        return randomSpawnPoint;
    }
}