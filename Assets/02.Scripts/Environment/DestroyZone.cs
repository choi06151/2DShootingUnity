using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(other.gameObject);
        PoolManager.Instance.InputToPool(other.gameObject);
    }
}