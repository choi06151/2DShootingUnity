using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PlayerSkill : MonoBehaviour
{
    [Header("플레이어 스킬 쿨타임")] [SerializeField]
    protected float _skillCoolTime;

    public float SkillCoolTime => _skillCoolTime;

    private void Start()
    {
        UseSkill();
    }

    protected abstract void UseSkill();
}