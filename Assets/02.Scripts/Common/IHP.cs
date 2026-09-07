using UnityEngine;

public interface IHP
{
    float Hp { get; }
    float MaxHp { get; }

    void GetDamage(float damage);
    void GetHp(float hp);

    void Death();
}