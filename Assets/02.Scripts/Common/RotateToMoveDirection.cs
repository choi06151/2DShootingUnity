using UnityEngine;

public class RotateToMoveDirection : MonoBehaviour
{
    private Vector3 _prePosition;

    private Vector3 _curPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetRotateToMoveDirection();
    }


    private void SetRotateToMoveDirection()
    {
        _prePosition = _curPosition;
        _curPosition = transform.position;
        Vector3 dir = _curPosition - _prePosition;
        dir.Normalize();
        transform.rotation = Quaternion.LookRotation(dir);
    }
}