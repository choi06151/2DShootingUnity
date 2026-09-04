using UnityEngine;

public class PlayerInfo : MonoBehaviour, IHP
{
    public float Hp { get; private set; }
    public float MaxHp { get; private set; } = 100;

    private void Start()
    {
        Hp = MaxHp;
    }

    public void GetDamage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Death();
        }
    }

    public void GetHp(float hp)
    {
        Hp += hp;
        if (Hp > MaxHp)
            Hp = MaxHp;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}