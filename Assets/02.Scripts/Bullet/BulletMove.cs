using UnityEngine;

public abstract class BulletMove : MonoBehaviour, IBulletFun
{
    protected float _moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Init(Bullet bullet)
    {
        _moveSpeed = bullet.MoveSpeed;
    }

    protected abstract void Move();
}