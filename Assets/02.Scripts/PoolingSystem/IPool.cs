using UnityEngine;

public interface IPool
{
    PoolManager PoolManager { get; set; }
    public void Init();
    public void Activate(Vector3 pos);
    public void Deactivate();
}