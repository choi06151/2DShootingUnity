using UnityEngine;


[RequireComponent(typeof(BulletMove))]
public class Bullet : MonoBehaviour
{
    [Header("총알 속도")] [SerializeField] private float _moveSpeed = 3f;
    [Header("총알 기본 데미지")] [SerializeField] private float _damage = 35f;
    public float MoveSpeed => _moveSpeed;
    public float Damage => _damage;

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
    }
}