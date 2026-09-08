using UnityEngine;

public class PlayerInfo : MonoBehaviour, IHP, IPlayerFun
{
    public float Hp { get; private set; }
    public float MaxHp { get; private set; } = 100;

    private GameObject _hitPrefab;
    private GameObject _deathPrefab;

    public void Init(Player player)
    {
        MaxHp = player.MaxHp;
        Hp = MaxHp;
        _hitPrefab = player.DamagedPrefab;
        _deathPrefab = player.DeathPrefab;
    }


    public void GetDamage(float damage)
    {
        if ((damage < 0))
        {
            return;
        }

        Instantiate(_hitPrefab, transform.position, Quaternion.identity);

        Hp -= damage;
        if (Hp <= 0)
        {
            Death();
        }
    }

    public void GetHp(float hp)
    {
        if ((hp < 0))
        {
            return;
        }

        Hp += hp;
        if (Hp > MaxHp)
            Hp = MaxHp;
    }

    public void GetMaxHp(float input)
    {
        MaxHp += input;
    }

    public void Death()
    {
        Instantiate(_deathPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}