using UnityEngine;

public class PlayerInfo : MonoBehaviour, IHP, IPlayerFun
{
    public float Hp { get; private set; }
    public float MaxHp { get; private set; } = 100;


    public void Init(Player player)
    {
        MaxHp = player.MaxHp;
        Hp = MaxHp;
    }


    public void GetDamage(float damage)
    {
        if ((damage < 0))
        {
            return;
        }

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
        //Destroy(gameObject);
        GetComponent<PoolingObject>().Deactivate();
    }
}