using UnityEngine;

public class MovementCommand :Command
{
    private Vector3 _position;
    private Quaternion _rotation;
    
    
    public MovementCommand(GameObject executedObject)
        : base(executedObject) // ?? 이부분은 잘 모르겠음 
    {
    }

    public override void Execute() // 실행
    {
        _executedObject.transform.position = _position;
        _executedObject.transform.rotation = _rotation;
        
    }

    public override void Record() //기록 
    {
        _position = _executedObject.transform.position;
        _rotation = _executedObject.transform.rotation;
    }
}
