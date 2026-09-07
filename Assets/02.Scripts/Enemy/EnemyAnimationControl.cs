using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour, IEnemyFun
{
    private Enemy _enemy;
    private Animator _animator;

    private static readonly int ANIM_TakeDamage =
        Animator.StringToHash("TakeDamage");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void TriggerDamageAnimation()
    {
        _animator.SetTrigger(ANIM_TakeDamage);
    }
}