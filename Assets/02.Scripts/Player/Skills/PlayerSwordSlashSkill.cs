using System;
using UnityEngine;

public class PlayerSwordSlashSkill : PlayerSkill
{
    [Header("이동 속도")] [SerializeField] private float _moveSpeed;
    [Header("틱당 데미지")] [SerializeField] private float _tickDamage;
    [Header("크기 성장 배율")] [SerializeField] private float _upScaleMultiplier;

    [Header("때릴시 이펙트 프리팹")] [SerializeField]
    private GameObject _effectPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = Vector3.up * _moveSpeed * Time.deltaTime;
        MovementCommand movementCommand = new MovementCommand(this.gameObject, moveDirection);

        CommandManager.Instance.ExecuteCommand(movementCommand);
    }

    protected override void UseSkill()
    {
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.CheckDeathDamage(_tickDamage))
            {
                SkillGrow();
            }

            Instantiate(_effectPrefab, other.transform.position, Quaternion.identity);
            enemy.TakeDamage(_tickDamage);
        }
    }

    private void SkillGrow()
    {
        transform.localScale *= _upScaleMultiplier;
    }
}