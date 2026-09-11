using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IHP, IPlayerFun
{
    public float Hp { get; private set; }
    public float MaxHp { get; }

    private Player _player;


    [Header("플레이어 피격시 이펙트 프리팹")]
    [SerializeField] private GameObject _damagedPrefab;

    [Header("플레이어 죽을시 이펙트 프리팹")]
    [SerializeField] private GameObject _deathPrefab;

    [Header("플레이어 죽을시 소리")]
    [SerializeField] private AudioClip _playerDeathClip;

    [Header("플레이어 데미지 받을때 소리")]
    [SerializeField] private AudioClip _playerDamageClip;


    public void Init(Player player)
    {
        _player = player;
        Hp = _player.Stat.Hp;
    }


    public void GetDamage(float damage)
    {
        if ((damage < 0))
        {
            return;
        }

        AudioManager.Instance.ActivateEffectAudioClip(_playerDamageClip);
        Instantiate(_damagedPrefab, transform.position, Quaternion.identity);

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
        if (Hp > _player.Stat.Hp)
            Hp = _player.Stat.Hp;
    }

    public void Death()
    {
        AudioManager.Instance.ActivateEffectAudioClip(_playerDeathClip);

        Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}