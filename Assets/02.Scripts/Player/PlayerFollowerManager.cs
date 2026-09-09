using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowerManager : MonoBehaviour, IPlayerFun
{
    [Header("팔로워 프리팹")] [SerializeField] private GameObject _followerPrefab;
    [Header("팔로워 생성위치")] [SerializeField] private Transform _followerSpawnPoint;
    [Header("팔로워 생성간격")] [SerializeField] private float _followerSpawnInterval;
    private Player _player;
    private List<PlayerFollower> _followers = new List<PlayerFollower>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init(Player player)
    {
        _player = player;
    }

    public void CreateFollowers()
    {
        GameObject playerFollower = Instantiate(
            _followerPrefab,
            _followerSpawnPoint.position,
            Quaternion.identity,
            transform
        );

        _followers.Add(playerFollower.GetComponent<PlayerFollower>());
        SetFollowerPositions();
    }

    private void SetFollowerPositions()
    {
        float centerIndex = (_followers.Count - 1) / 2f;

        for (int i = 0; i < _followers.Count; i++)
        {
            float offsetX = (i - centerIndex) * _followerSpawnInterval;

            Vector3 position =
                _followerSpawnPoint.position +
                Vector3.right * offsetX;

            _followers[i].transform.position = position;
        }
    }

    public void FireAllFollowers()
    {
        foreach (PlayerFollower follower in _followers)
        {
            follower.Fire(_player);
        }
    }
}