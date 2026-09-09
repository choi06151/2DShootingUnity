using System;
using UnityEngine;


[RequireComponent(typeof(BulletMove))]
public class Bullet : PoolingObject
{
    [Header("총알 속도")] [SerializeField] private float _moveSpeed = 3f;
    [Header("총알 기본 데미지")] [SerializeField] private float _damage = 35f;

    [Header("총알 피격시 이펙트 프리팹")] [SerializeField]
    private GameObject _hitEffectPrefab;

    [Header("총알 피격시 소리")] [SerializeField] private AudioClip _hitAudioClip;

    public float MoveSpeed => _moveSpeed;
    public float Damage => _damage;
    public GameObject HitEffectPrefab => _hitEffectPrefab;
    private BulletMove _bulletMove;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bulletMove = GetComponent<BulletMove>();

        _bulletMove.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Init(Player player)
    {
        _damage = _damage * player.PlayerDamageMultiplier;
        _moveSpeed = _moveSpeed + _moveSpeed * player.MoveSpeedMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
            Enemy enemy = other.GetComponent<Enemy>();

            AudioManager.Instance.ActivateEffectAudioClip(_hitAudioClip);

            enemy.TakeDamage(_damage);
            PoolManager.Instance.InputToPool(this.gameObject);
        }
    }
}