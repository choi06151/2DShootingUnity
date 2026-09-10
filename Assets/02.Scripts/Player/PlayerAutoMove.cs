using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMove : MonoBehaviour, IPlayerFun
{
    [Header("접근 완료 처리 임계값")] [SerializeField]
    private float _threshold;

    [Header("탐색 실패시 재탐색 대기시간")] [SerializeField]
    private float _delayTime;

    private float _curTime;
    private bool _isTargetSettings;
    private bool _isAutoActivated;
    private Vector3 _targetPosition;
    private Player _player;

    private PlayerMove _playerMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAutoActivated)
        {
            if (_isTargetSettings)
            {
                InputMovementToPlayer();
            }
            else
            {
                _curTime -= Time.deltaTime;

                if (_curTime <= 0)
                {
                    SetNewTargetPosition();
                }
            }
        }
    }

    public void Init(Player player)
    {
        _player = player;
        _playerMove = _player.PlayerMove;
        _isAutoActivated = _player.PlayerAutoMove;

        if (_isAutoActivated)
        {
            StartAutoMove();
        }
    }


    private void StartAutoMove()
    {
        _isAutoActivated = true;
        SetNewTargetPosition();
    }

    private void StopAutoMove()
    {
        _isAutoActivated = false;
    }

    private void ReFindNewTargetDelay()
    {
        _isTargetSettings = false;
        _curTime = _delayTime;
    }

    private void SetNewTargetPosition()
    {
        GameObject[] finedEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (finedEnemies.Length == 0)
        {
            ReFindNewTargetDelay();
            return;
        }

        List<Enemy> enemies = new List<Enemy>();
        foreach (GameObject obj in finedEnemies)
        {
            Enemy enemy = obj.GetComponent<Enemy>();
            enemies.Add(enemy);
        }

        Enemy lowest = GetLowestHpEnemy(enemies);
        _targetPosition = transform.position;
        _targetPosition.x = lowest.transform.position.x;
        _isTargetSettings = true;
    }

    private Enemy GetLowestHpEnemy(List<Enemy> enemies)
    {
        float lowestHp = enemies[0].EnemyHp;
        Enemy lowestEnemy = enemies[0];
        foreach (var enemy in enemies)
        {
            if (lowestHp > enemy.EnemyHp)
            {
                lowestHp = enemy.EnemyHp;
                lowestEnemy = enemy;
            }
        }

        return lowestEnemy;
    }

    private void InputMovementToPlayer()
    {
        Vector3 direction = _targetPosition - transform.position;
        direction.Normalize();
        float h = direction.x;
        float v = direction.y;

        _playerMove.PlayerMovementInput(h, v);
        CheckDistanceToTargetPosition();
    }


    private void CheckDistanceToTargetPosition()
    {
        float playerPosX = _player.transform.position.x;
        float targetPosX = _targetPosition.x;

        float distance = Mathf.Abs(playerPosX - targetPosX);

        if (distance <= _threshold)
        {
            SetNewTargetPosition();
        }
    }
}