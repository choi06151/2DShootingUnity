using UnityEngine;

public class Command : ICommand
{
    protected float _executedTime;
    protected GameObject _executedObject;
    
    
    public Command(GameObject executedObject) //실행된 시간과 , 객체를 저장
    {
        _executedTime=Time.time; //지금까지의 시간 
        _executedObject = executedObject; // 현재 실행한 물체 
        
        Record(); //행동 기록
    }

    public float GetExecutionTime()
    {
        return _executedTime;
    }
    
    public virtual void Execute()
    {
        Debug.Log("저장된 명령을 실행합니다");
    }
    public virtual void Record()
    {
        Debug.Log("저장된 명령을 기록합니다");
    }
    

}
