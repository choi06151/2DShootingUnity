using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerFire), typeof(PlayerMove), typeof(PlayerInfo))]
public class Player : MonoBehaviour
{
    [Header("현재 스탯 정보")]
    [SerializeField] private Stat _stat;

    public Stat Stat => _stat;

    private PlayerFire _playerFire;
    private PlayerMove _playerMove;
    public PlayerMove PlayerMove => _playerMove;

    private PlayerInfo _playerInfo;
    private PlayerAnimationControl _playerAnimationControl;
    private PlayerSkillManager _playerSkillManager;
    private PlayerFollowerManager _playerFollowerManager;
    public PlayerFollowerManager PlayerFollowerManager => _playerFollowerManager;

    private PlayerAutoMove _playerAutoMove;


    private float _speedMultiplier;
    public float SpeedMultiplier => _speedMultiplier;


    public void ApplyPlayerData(Stat stat)
    {
        if (stat != null)
        {
            _stat = stat;
            Init();
            Debug.Log("데이터 불러오기 성공");
        }
        else
        {
            Debug.Log("데이터 불러오기 실패");
        }
    }

    private void Init()
    {
        IPlayerFun[] funs = GetComponentsInChildren<IPlayerFun>();
        foreach (var fun in funs)
        {
            fun.Init(this);
        }


        _playerInfo = GetComponent<PlayerInfo>();
        _playerMove = GetComponent<PlayerMove>();
        _playerFire = GetComponent<PlayerFire>();
        _playerAnimationControl = GetComponent<PlayerAnimationControl>();
        _playerSkillManager = GetComponent<PlayerSkillManager>();
        _playerFollowerManager = GetComponent<PlayerFollowerManager>();
        _playerAutoMove = GetComponent<PlayerAutoMove>();


        UpgradeApplier[] upgradeAppliers = FindObjectsByType<UpgradeApplier>(FindObjectsSortMode.None);
        foreach (var applier in upgradeAppliers)
        {
            applier.Init(this);
        }

        UI_ButtonParent[] ui_buttons = FindObjectsByType<UI_ButtonParent>(FindObjectsSortMode.None);
        foreach (var button in ui_buttons)
        {
            button.Init(this);
        }
    }


    public void UpdateAnimState(int xVector, bool isfire, bool isHit)
    {
        _playerAnimationControl.UpdateMoveAnimation(xVector);
    }

    public void TakeDamage(float damage)
    {
        _playerInfo.GetDamage(damage);
    }

    public void TakeSpeedUp(float speed)
    {
        _speedMultiplier = speed;

        _stat.MoveSpeed = _stat.MoveSpeed + speed * _stat.MoveSpeed;

        _stat.BulletCoolTime = _stat.BulletCoolTime - _stat.BulletCoolTime * _speedMultiplier;
    }


    public void TakeHp(float hp)
    {
        _playerInfo.GetHp(hp);
    }

    public void TakeMaxHp(float hp)
    {
        _stat.Hp += hp;
    }

    public void TakeDamageUp(float input)
    {
        _stat.Damage += input;
    }

    public void UpgradeDamage(float up)
    {
        _stat.Damage *= up;
    }

    public void UpgradeSkillCoolTime(float up)
    {
        _stat.Damage *= up;
    }

    public void TakeBulletCountUp(int input)
    {
        _stat.BulletCount += input;
        _playerFire.CheckFireUpgrade();
    }

    public void TakeBulletTypePlusItem(Bullet bullet)
    {
        _playerFire.BulletTypePlus(bullet);
    }


    public void PlusPlayerFollower()
    {
        _playerFollowerManager.CreateFollowers();
    }

    public void SwitchAutoPlayMode()
    {
        _stat.IsBulletAutoFireOn = !_stat.IsBulletAutoFireOn;
        _stat.IsPlayerAutoMoveOn = !_stat.IsPlayerAutoMoveOn;
        _stat.IsPlayerAutoSkillOn = !_stat.IsPlayerAutoSkillOn;

        Debug.Log("자동재생부분 변환");
    }
}