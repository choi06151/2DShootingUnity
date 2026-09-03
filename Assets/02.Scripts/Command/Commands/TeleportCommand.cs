using UnityEngine;

public class TeleportCommand : CommandParent
{
    private Vector3 _targetPosition;

    public TeleportCommand(GameObject executedObject, Vector3 teleportPosition) : base(executedObject)
    {
        _targetPosition = teleportPosition;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    public override void Execute()
    {
        _executedObject.transform.position = _targetPosition;
    }
}