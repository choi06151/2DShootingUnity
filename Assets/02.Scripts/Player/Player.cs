using UnityEngine;


[RequireComponent(typeof(PlayerFire), typeof(PlayerMove), typeof(PlayerInfo))]
public class Player : MonoBehaviour
{
    private PlayerFire _playerFire;
    private PlayerMove _playerMove;
    private PlayerInfo _playerInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerInfo = GetComponent<PlayerInfo>();
        _playerMove = GetComponent<PlayerMove>();
        _playerFire = GetComponent<PlayerFire>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        _playerInfo.GetDamage(damage);
    }
}