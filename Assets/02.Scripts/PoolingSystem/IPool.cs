using UnityEngine;

public interface IPool
{
    PoolManager PoolManager { get; }
    public void Init(PoolManager poolManager);
    public void Activate(Vector3 pos);
    public void Deactivate();
}