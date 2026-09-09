using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlowZoneSkill : PlayerSkill
{
    [Header("슬로우 배율")] [SerializeField] private float _slowMultiplier;
    [Header("지속 시간")] [SerializeField] private float _duration;

    [Header("전방에 생기는 거리")] [SerializeField]
    private float _forwardDistance;

    [Header("생성 및 삭제 시간")] [SerializeField]
    private float _createTime;

    [Header("최대 크기")] [SerializeField] private Vector3 _maxScale;

    private float _curTime;
    private bool _isIncreaseStart;
    private bool _isDecreaseStart;
    private bool _isActivateStart;
    private List<Enemy> _effectedEnemy = new List<Enemy>();

    private void Update()
    {
        if (_isIncreaseStart)
        {
            Increase();
        }
        else if (_isActivateStart)
        {
            CheckActivateTime();
        }
        else if (_isDecreaseStart)
        {
            Decrease();
        }

        _curTime -= Time.deltaTime;
    }

    protected override void UseSkill()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        Vector3 position = transform.position + _forwardDistance * Vector3.up;
        TeleportCommand teleportCommand = new TeleportCommand(this.gameObject, position);
        CommandManager.Instance.ExecuteCommand(teleportCommand);
        InitIncrease();
    }

    private void InitIncrease()
    {
        _isIncreaseStart = true;
        _curTime = _createTime;
    }

    private void Increase()
    {
        float t = 1 - (_curTime / _createTime);

        transform.localScale =
            Vector3.Lerp(
                Vector3.zero,
                _maxScale,
                t
            );

        if (_curTime <= 0)
        {
            transform.localScale = _maxScale;

            _isIncreaseStart = false;

            InitActivate();
        }
    }

    private void InitActivate()
    {
        _isActivateStart = true;
        _curTime = _duration;
    }

    private void CheckActivateTime()
    {
        float t = 1 - (_curTime / _createTime);


        if (_curTime <= 0)
        {
            _isActivateStart = false;
            InitDecrease();
        }
    }

    private void InitDecrease()
    {
        _isDecreaseStart = true;
        _curTime = _createTime;
    }

    private void Decrease()
    {
        float t = 1 - (_curTime / _createTime);

        transform.localScale =
            Vector3.Lerp(
                Vector3.zero,
                _maxScale,
                t
            );

        if (_curTime <= 0)
        {
            transform.localScale = Vector3.zero;

            _isDecreaseStart = false;

            SkillEnd();
        }
    }

    private void SkillEnd()
    {
        UnEffect();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            EffectToEnemy(enemy);
        }
    }

    private void EffectToEnemy(Enemy enemy)
    {
        enemy.TakeSlowEffect(_slowMultiplier);
        _effectedEnemy.Add(enemy);
    }

    private void UnEffect()
    {
        foreach (var enemy in _effectedEnemy)
        {
            if (enemy != null)
            {
                enemy.ClearSlowEffect();
            }
        }
    }
}