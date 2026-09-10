using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMove : MonoBehaviour, IPlayerFun
{
    [Header("목표 X 좌표 도달 임계값")] [SerializeField]
    private float _targetThreshold = 0.1f;

    [Header("적과 유지할 최소 거리")] [SerializeField]
    private float _safeDistance = 1.5f;

    [Header("재탐색 대기 시간")] [SerializeField] private float _delayTime = 0.5f;

    private float _curTime;

    private bool _isTargetSettings;
    private bool _isAutoActivated;

    private Enemy _targetEnemy;

    private float _targetX;

    private Player _player;
    private PlayerMove _playerMove;

    private void Update()
    {
        if (!_isAutoActivated)
        {
            return;
        }

        if (_isTargetSettings)
        {
            InputMovementToPlayer();
        }
        else
        {
            UpdateAvoidAndRefind();
        }
    }

    public void Init(Player player)
    {
        _player = player;
        _playerMove = player.PlayerMove;
        _isAutoActivated = player.PlayerAutoMove;

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

        ClearTarget();
    }


    private void SetNewTargetPosition()
    {
        GameObject[] findEnemies =
            GameObject.FindGameObjectsWithTag("Enemy");

        if (findEnemies.Length == 0)
        {
            ReFindNewTargetDelay();
            return;
        }

        List<Enemy> enemies = new List<Enemy>();

        foreach (GameObject obj in findEnemies)
        {
            if (obj.TryGetComponent(out Enemy enemy))
            {
                enemies.Add(enemy);
            }
        }

        if (enemies.Count == 0)
        {
            ReFindNewTargetDelay();
            return;
        }

        Enemy lowestEnemy = GetLowestHpEnemy(enemies);

        if (lowestEnemy == null)
        {
            ReFindNewTargetDelay();
            return;
        }

        _targetEnemy = lowestEnemy;

        _targetX = lowestEnemy.transform.position.x;

        _isTargetSettings = true;
    }

    private Enemy GetLowestHpEnemy(List<Enemy> enemies)
    {
        if (enemies.Count == 0)
        {
            return null;
        }

        Enemy lowestEnemy = enemies[0];

        foreach (Enemy enemy in enemies)
        {
            if (enemy.EnemyHp < lowestEnemy.EnemyHp)
            {
                lowestEnemy = enemy;
            }
        }

        return lowestEnemy;
    }

    private void ClearTarget()
    {
        _targetEnemy = null;
        _isTargetSettings = false;
    }


    private void InputMovementToPlayer()
    {
        if (!IsTargetValid())
        {
            ReFindNewTargetDelay();
            return;
        }

        if (!CheckIsSafeToMove())
        {
            ReFindNewTargetDelay();
            return;
        }

        if (CheckXDistanceToTargetPosition())
        {
            ReFindNewTargetDelay();
            return;
        }

        float directionX =
            _targetX - transform.position.x;

        directionX = Mathf.Sign(directionX);

        _playerMove.PlayerMovementInput(
            directionX,
            0f
        );
    }


    private void UpdateAvoidAndRefind()
    {
        if (IsTargetValid())
        {
            MoveAvoidDirection();
        }

        _curTime -= Time.deltaTime;

        if (_curTime <= 0f)
        {
            SetNewTargetPosition();
        }
    }

    private void MoveAvoidDirection()
    {
        if (!IsTargetValid())
        {
            return;
        }

        Vector3 playerPosition =
            transform.position;

        Vector3 enemyPosition =
            _targetEnemy.transform.position;

        // 적 반대 방향
        Vector3 avoidDirection =
            playerPosition - enemyPosition;

        avoidDirection.Normalize();

        _playerMove.PlayerMovementInput(
            avoidDirection.x,
            avoidDirection.y
        );
    }

    private bool CheckXDistanceToTargetPosition()
    {
        float playerPosX =
            transform.position.x;

        float distance =
            Mathf.Abs(playerPosX - _targetX);

        return distance <= _targetThreshold;
    }

    private bool CheckIsSafeToMove()
    {
        if (!IsTargetValid())
        {
            return false;
        }

        Vector3 playerPos =
            transform.position;

        Vector3 targetPos =
            _targetEnemy.transform.position;

        float distance =
            Vector3.Distance(playerPos, targetPos);

        return distance > _safeDistance;
    }

    private bool IsTargetValid()
    {
        return _targetEnemy != null &&
               _targetEnemy.gameObject.activeInHierarchy;
    }

    private void ReFindNewTargetDelay()
    {
        _isTargetSettings = false;

        _curTime = _delayTime;
    }
}