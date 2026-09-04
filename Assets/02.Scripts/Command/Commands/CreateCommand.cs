using UnityEngine;

public class CreateCommand : CommandParent
{
    private GameObject _createTarget;
    private Vector3 _createPosition;

    private GameObject _createdObject;
    public GameObject GetCreatedObeject => _createdObject;

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
        GameObject createdObject = GameObject.Instantiate(_createTarget);
        createdObject.transform.position = _createPosition;
        _createdObject = createdObject;
    }
}