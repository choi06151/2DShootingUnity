using Unity.VisualScripting;
using UnityEngine;

public class PoolObject : MonoBehaviour, IPool
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    public PoolManager PoolManager { get; private set; }

    public void Init(PoolManager poolManager)
    {
        PoolManager = poolManager;
        gameObject.SetActive(false);
    }

    public void Activate(Vector3 pos)
    {
        gameObject.SetActive(true);
        TeleportCommand teleportCommand = new TeleportCommand(gameObject, pos);
        CommandManager.Instance.ExecuteCommand(teleportCommand);

        throw new System.NotImplementedException();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}