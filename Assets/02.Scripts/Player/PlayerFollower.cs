using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("총알 발사 위치")] [SerializeField] private Transform _firePoint;
    [Header("총알 프리팹")] [SerializeField] private GameObject _firePrefab;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Fire(Player player)
    {
        CreateCommand createCommand = new CreateCommand(this.gameObject, _firePrefab, _firePoint.position);
        CommandManager.Instance.ExecuteCommand(createCommand);
        createCommand.GetCreatedObeject.GetComponent<Bullet>().Init(player);
    }
}