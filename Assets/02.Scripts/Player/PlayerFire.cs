using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerFire : MonoBehaviour, IPlayerFun
{
    private List<Bullet> _spawnBulletList;
    private Transform _mainBulletSpawnPoint;
    private int _bulletFireCount;
    private float _fireCoolTime;
    private bool _isAutoFire;
    private float _currentFireCooldown = 999f;
    private float _firePointInterval;
    private float _playerDamageMultiplier;

    private Player _player;

    private AudioSource _audioSource;
    private AudioClip _fireClip;

    public void Init(Player player)
    {
        if (player == null)
            return;

        _player = player;
        _isAutoFire = player.bulletAutoFire;
        _bulletFireCount = player.BulletFireCount;
        _fireCoolTime = player.FireCoolTime;
        _mainBulletSpawnPoint = player.BulletSpawnPoint;
        _firePointInterval = player.FirePointInterval;
        _spawnBulletList = player.BulletList;
        _fireClip = player.FireClip;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckFire();
        CheckAutoFire();
    }

    private void CheckFire()
    {
        UpdateFireCoolTime();


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoFire = !_isAutoFire;
            if (_isAutoFire)
            {
                Debug.Log("Auto Fire활성화");
            }
            else
            {
                Debug.Log("Auto Fire비활성화");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && CheckFireCoolTime())
        {
            FireAllBullet();
        }
    }

    private void FireAllBullet()
    {
        if (_spawnBulletList == null || _mainBulletSpawnPoint == null ||
            CommandManager.Instance == null || _player == null)
        {
            return;
        }

        _currentFireCooldown = 0;
        int bulletIndex = 0;

        foreach (Bullet bullet in _spawnBulletList)
        {
            if (bullet == null)
                continue;

            for (int i = 0; i < _bulletFireCount; i++)
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
        float centerIndex = (_bulletFireCount - 1) / 2f;
        float offsetX = (createIndex - centerIndex) * _firePointInterval;

        Vector3 controlledXPos = _mainBulletSpawnPoint.position
                                 + Vector3.right * offsetX;

        float offsetY = bulletIndex * _firePointInterval;

        Vector3 controlledYPos = controlledXPos + Vector3.up * offsetY;
        return controlledYPos;
    }

    private void CheckAutoFire()
    {
        if (_isAutoFire)
        {
            if (_currentFireCooldown > _fireCoolTime)
            {
                FireAllBullet();
            }
        }
    }

    private bool CheckFireCoolTime()
    {
        return _currentFireCooldown > _fireCoolTime;
    }

    private void UpdateFireCoolTime()
    {
        _currentFireCooldown += Time.deltaTime;
    }

    public void BulletUpgrade()
    {
    }
}