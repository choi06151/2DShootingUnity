using UnityEngine;

public class PoolingObject : MonoBehaviour
{
    private GameObject _poolKey;


    public void InitPoolObject(GameObject poolKey)
    {
        _poolKey = poolKey;
    }


    public GameObject GetPoolKey()
    {
        return _poolKey;
    }


    public void Activate()
    {
        gameObject.SetActive(true);
    }


    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}