using UnityEngine;

public class PlayerBombSkill : PlayerSkill
{
    [Header("생성 후 폭탄 터지는 시간")] [SerializeField]
    private float _bombExplodeTime;

    [Header("폭탄 이동 속도")] [SerializeField] private float _moveSpeed;

    [Header("폭탄 지속 시간")] [SerializeField] private float _explosionTime;

    [Header("폭탄 최대 크기")] [SerializeField] private float _maxScale;

    [Header("폭탄 최소 크기")] [SerializeField] private float _minScale;

    [Header("폭탄 피격 된 적 이펙트")] [SerializeField]
    private GameObject _explodeEffectPrefab;

    [Header("폭탄 확대/축소 시간")] [SerializeField]
    private float _increaseTime;

    private float _curTime;

    private bool _isMoveStart;
    private bool _isExplodeStart;
    private bool _isIncreaseStart;
    private bool _isDecreaseStart;

    private Vector3 _minScaleVector;
    private Vector3 _maxScaleVector;

    private void Update()
    {
        if (_isMoveStart)
        {
            MoveForward();
        }

        if (_isIncreaseStart)
        {
            Increase();
        }
        else if (_isExplodeStart)
        {
            Explosion();
        }
        else if (_isDecreaseStart)
        {
            Decrease();
        }

        _curTime -= Time.deltaTime;
    }


    protected override void UseSkill()
    {
        _minScaleVector = Vector3.one * _minScale;
        _maxScaleVector = Vector3.one * _maxScale;


        transform.localScale = _minScaleVector;


        MoveStartToExplosionPoint();
    }


    private void MoveStartToExplosionPoint()
    {
        _curTime = _bombExplodeTime;
        _isMoveStart = true;
    }

    private void MoveForward()
    {
        if (_curTime <= 0)
        {
            _isMoveStart = false;

            IncreaseStart();
            return;
        }

        Vector3 direction =
            Vector2.up * _moveSpeed * Time.deltaTime;

        MovementCommand movementCommand =
            new MovementCommand(gameObject, direction);

        CommandManager.Instance.ExecuteCommand(movementCommand);
    }


    private void IncreaseStart()
    {
        _isIncreaseStart = true;
        _curTime = _increaseTime;
    }

    private void Increase()
    {
        float t = 1 - (_curTime / _increaseTime);

        transform.localScale =
            Vector3.Lerp(
                _minScaleVector,
                _maxScaleVector,
                t
            );

        if (_curTime <= 0)
        {
            transform.localScale = _maxScaleVector;

            _isIncreaseStart = false;

            ExplodeStart();
        }
    }


    private void ExplodeStart()
    {
        _isExplodeStart = true;
        _curTime = _explosionTime;
    }

    private void Explosion()
    {
        if (_curTime <= 0)
        {
            _isExplodeStart = false;

            DecreaseStart();
        }
    }


    private void DecreaseStart()
    {
        _isDecreaseStart = true;
        _curTime = _increaseTime;
    }

    private void Decrease()
    {
        float t = 1 - (_curTime / _increaseTime);

        transform.localScale =
            Vector3.Lerp(
                _maxScaleVector,
                _minScaleVector,
                t
            );

        if (_curTime <= 0)
        {
            transform.localScale = _minScaleVector;

            _isDecreaseStart = false;

            SkillEnd();
        }
    }


    private void SkillEnd()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_explodeEffectPrefab != null)
            {
                Instantiate(
                    _explodeEffectPrefab,
                    other.transform.position,
                    Quaternion.identity
                );
            }

            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(999999);
            }
        }
    }
}