using UnityEngine;

public interface IPool
{
    public void Init();
    public void Activate(Vector3 pos);
    public void Deactivate();
}