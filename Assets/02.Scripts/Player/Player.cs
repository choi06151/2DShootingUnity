using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerFire), typeof(PlayerMove), typeof(PlayerInfo))]
public class Player : MonoBehaviour
{
    private PlayerFire _playerFire;
    private PlayerMove _playerMove;
    private PlayerInfo _playerInfo;
    private PlayerAnimationControl _playerAnimationControl;

    [Header("플레이어 메인 총알 발사 지점")] [SerializeField]
    private Transform _bulletSpawnPoint;

    [Header("플레이어 이동 속도")] [SerializeField]
    private float _moveSpeed;

    [Header("플레이어 이동 증감 배율")] [SerializeField]
    private float _moveSpeedMultiplier;

    [Header("플레이어 최대 체력")] [SerializeField]
    private float _maxHp;

    [Header("플레이어 공격력")] [SerializeField] private float _playerDamageMultiplier;

    [Header("플레이어 총알 자동 발사")] [SerializeField]
    private bool _bulletAutoFire;

    [Header("플레이어 총알 쿨타임")] [SerializeField]
    private float _fireCoolTime;

    [Header("플레이어 총알 발사 위치 간격")] [SerializeField]
    private float _firePointInterval;

    [Header("플레이어 총알 발사 개수")] [SerializeField]
    private int _bulletFireCount;

    [Header("플레이어 총알 종류")] [SerializeField]
    private List<Bullet> _bulletList;


    public Transform BulletSpawnPoint => _bulletSpawnPoint;
    public float MoveSpeed => _moveSpeed;
    public float MoveSpeedMultiplier => _moveSpeedMultiplier;
    public float MaxHp => _maxHp;
    public float PlayerDamageMultiplier => _playerDamageMultiplier;
    public bool bulletAutoFire => _bulletAutoFire;
    public float FireCoolTime => _fireCoolTime;
    public float FirePointInterval => _firePointInterval;
    public int BulletFireCount => _bulletFireCount;
    public List<Bullet> BulletList => _bulletList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerInfo = GetComponent<PlayerInfo>();
        _playerMove = GetComponent<PlayerMove>();
        _playerFire = GetComponent<PlayerFire>();
        _playerAnimationControl = GetComponent<PlayerAnimationControl>();

        _playerInfo.Init(this);
        _playerMove.Init(this);
        _playerFire.Init(this);
        _playerAnimationControl.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
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
        _moveSpeed = speed;
        _playerMove.Init(this);
    }

    public void TakeSpeedMultiply(float speedMultiplier)
    {
        _moveSpeedMultiplier = speedMultiplier;
        _playerMove.Init(this);
    }

    public void TakeHp(float hp)
    {
        _playerInfo.GetHp(hp);
    }

    public void TakeMaxHp(float hp)
    {
        _playerInfo.GetMaxHp(hp);
    }

    public void TakeDamageUp(float input)
    {
        _playerDamageMultiplier += input;
    }

    public void TakeBulletCountUp(int input)
    {
        _bulletFireCount += input;
        _playerFire.Init(this);
    }

    public void TakeBulletTypePlusItem(Bullet bullet)
    {
        _bulletList.Add(bullet);
    }
}