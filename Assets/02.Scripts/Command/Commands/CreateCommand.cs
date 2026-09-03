using UnityEngine;

public class CreateCommand : CommandParent
{
    private GameObject _createTarget;
    private Vector3 _createPosition;

    public CreateCommand(GameObject executedObject, GameObject createTarget, Vector3 createPosition) : base(
        executedObject)
    {
        _createTarget = createTarget;
        _createPosition = createPosition;
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
        GameObject bullet = GameObject.Instantiate(_createTarget);
        bullet.transform.position = _createPosition;
    }
}