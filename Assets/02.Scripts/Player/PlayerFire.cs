using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerFire : MonoBehaviour, IPlayerFun
{
    private float _currentFireCooldown = 999f;

    private Player _player;

    [Header("플레이어 메인 총알 발사 지점")]
    [SerializeField] private Transform _bulletSpawnPoint;

    [Header("플레이어 총알 종류")]
    [SerializeField] private List<Bullet> _bulletList;

    [Header("플레이어 총알 발사 소리")]
    [SerializeField] private AudioClip _fireClip;


    public void Init(Player player)
    {
        if (player == null)
            return;

        _player = player;
    }


    void Update()
    {
        CheckFire();
        CheckAutoFire();
    }

    private void CheckFire()
    {
        _currentFireCooldown += Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space) && CheckFireCoolTime())
        {
            FireAllBullet();
        }
    }

    private void FireAllBullet()
    {
        _currentFireCooldown = 0;
        int bulletIndex = 0;

        foreach (Bullet bullet in _bulletList)
        {
            if (bullet == null)
                continue;

            for (int i = 0; i < _player.Stat.BulletCount; i++)
            {
                CreateCommand createCommand =
                    new CreateCommand(this.gameObject, bullet.gameObject, GetFirePoint(i, bulletIndex));
                CommandManager.Instance.ExecuteCommand(createCommand);
                if (createCommand.GetCreatedObeject != null &&
                    createCommand.GetCreatedObeject.TryGetComponent(out Bullet spawnedBullet))
                {
                    spawnedBullet.Init(_player);
                }
            }

            bulletIndex++;
        }

        FireFollowerBullet();
        PlayFireSound();
    }

    private void PlayFireSound()
    {
        if (AudioManager.Instance != null && _fireClip != null)
            AudioManager.Instance.ActivateEffectAudioClip(_fireClip);
    }

    private void FireFollowerBullet()
    {
        if (_player != null && _player.PlayerFollowerManager != null)
            _player.PlayerFollowerManager.FireAllFollowers();
    }

    private Vector3 GetFirePoint(int createIndex, int bulletIndex)
    {
        float centerIndex = (_player.Stat.BulletCount - 1) / 2f;
        float offsetX = (createIndex - centerIndex) * _player.Stat.BulletInterval;

        Vector3 controlledXPos = _bulletSpawnPoint.position
                                 + Vector3.right * offsetX;

        float offsetY = bulletIndex * _player.Stat.BulletInterval;

        Vector3 controlledYPos = controlledXPos + Vector3.up * offsetY;
        return controlledYPos;
    }

    private void CheckAutoFire()
    {
        if (_player.Stat.IsBulletAutoFireOn)
        {
            if (_currentFireCooldown > _player.Stat.BulletCoolTime)
            {
                FireAllBullet();
            }
        }
    }

    private bool CheckFireCoolTime()
    {
        return _currentFireCooldown > _player.Stat.BulletCoolTime;
    }


    public void BulletTypePlus(Bullet bullet)
    {
        _bulletList.Add(bullet);
        CheckFireUpgrade();
    }

    public void CheckFireUpgrade()
    {
        if (_player.Stat.BulletCount >= _player.Stat.BulletUpgradeCount ||
            _bulletList.Count >= _player.Stat.BulletUpgradeCount)
        {
            Bullet mainBullet = _bulletList[0];
            _bulletList.Clear();
            _bulletList.Add(mainBullet);

            _player.Stat.BulletCount = 1;
            _player.Stat.Damage *= 1.2f;
            _player.Stat.BulletUpgradeCount += 1;
            _player.PlusPlayerFollower();
        }
    }
}