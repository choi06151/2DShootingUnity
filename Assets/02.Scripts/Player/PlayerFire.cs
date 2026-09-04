using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerFire : MonoBehaviour, IPlayerFun
{
    private List<BulletMove> _spawnBulletList;
    private Transform _mainBulletSpawnPoint;
    private int _bulletFireCount;
    private float _fireCoolTime;
    private bool _isAutoFire;
    private float _currentFireCooldown = 999f;
    private float _firePointInterval;

    public void Init(Player player)
    {
        _bulletFireCount = player.BulletFireCount;
        _fireCoolTime = player.FireCoolTime;
        _mainBulletSpawnPoint = player.BulletSpawnPoint;
        _firePointInterval = player.FirePointInterval;
        _spawnBulletList = player.BulletList;
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
        _currentFireCooldown = 0;
        int bulletIndex = 0;

        foreach (BulletMove bullet in _spawnBulletList)
        {
            for (int i = 0; i < _bulletFireCount; i++)
            {
                CreateCommand createCommand =
                    new CreateCommand(this.gameObject, bullet.gameObject, GetFirePoint(i, bulletIndex));
                CommandManager.Instance.ExecuteCommand(createCommand);
            }

            bulletIndex++;
        }
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
}