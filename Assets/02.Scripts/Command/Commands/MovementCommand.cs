using UnityEngine;

public class MovementCommand : CommandParent
{
    private Vector3 _moveinfo;
    
    public MovementCommand(GameObject executedObject,Vector3 moveInfo) : base(executedObject)
    {
        _moveinfo = moveInfo;
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
        _executedObject.transform.Translate(_moveinfo);
    }
}
