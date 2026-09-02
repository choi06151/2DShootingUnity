using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour
{
    private List<Command> _collectedCommands = new List<Command>(); //동적 생성을 위한 리스트

    private float _replayStartTime;
    private float _currentReplayTime;
    private int _replayIdx=0;
    private bool _isReplaying=false;
    
    //추후 싱글톤 패턴 구현 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.T))
        {
            DeleteRecord();
        }
        
        if(Input.GetKey(KeyCode.R)&&!_isReplaying)
        {
            ReplayStart();
        }

        if (_isReplaying)
        {
            UpdateReplay();
        }
        
    }

    public void Collect(Command command)
    {
        _collectedCommands.Add(command); //명령에 기록
    }


    public void DeleteRecord()
    {
        Debug.Log("기록된 레코드를 삭제합니다");
        _collectedCommands.Clear();
    }
    public void ReplayStart()
    {
        Debug.Log($"리플레이를 시작합니다 저장된 리플레이 개수:{_collectedCommands.Count} ");
        _replayStartTime=Time.time; //replay 초기값 설정
        _replayIdx=0;
        _isReplaying = true;
        
        
    }

    private void UpdateReplay()
    {
        
        _currentReplayTime = Time.time - _replayStartTime;

        
        if (_replayIdx < _collectedCommands.Count&&_currentReplayTime >= _collectedCommands[_replayIdx].GetExecutionTime()) // 저장된 리플레이를 다 진행 할 동안 
        {
                _collectedCommands[_replayIdx].Execute(); //저장된 커맨드를 실행 
                _replayIdx++;//실행후 다음 인덷ㄱ스 검사
            
        }

        if (_replayIdx >= _collectedCommands.Count)  //저장된리플레이 다 사용한다면
        {
            Debug.Log($"리플레이가 완료되었습니다");
            _isReplaying = false;
        }
        
    }
}
