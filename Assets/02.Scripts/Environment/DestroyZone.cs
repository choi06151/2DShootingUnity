using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D  other)
    {
        Debug.Log("충돌발생");
        Destroy(other.gameObject);
    }
}