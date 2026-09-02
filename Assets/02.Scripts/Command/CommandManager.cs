using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    
    private List<CommandParent> _commandsHistory= new List<CommandParent>();
    
    private float _replayStartTime;
    private float _currentReplayTime;
    private int _replayIdx=0;
    private bool _isReplaying=false;
    
    
    
    public static CommandManager Instance { get; private set; } //어디에서든 호출 가능

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {
            DeleteHistory();
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

    public void ExecuteCommand(CommandParent command)
    {
        command.Execute();
        _commandsHistory.Add(command);
        
    }

    public void ReplayStart()
    {
        Debug.Log($"리플레이를 시작합니다 저장된 리플레이 개수:{_commandsHistory.Count} ");
        _replayStartTime=Time.time; //replay 초기값 설정
        _replayIdx=0;
        _isReplaying = true;
    }

    private void UpdateReplay()
    {
        _currentReplayTime = Time.time - _replayStartTime;

        
        if (_replayIdx < _commandsHistory.Count&&_currentReplayTime >= _commandsHistory[_replayIdx].GetExecutionTime()) // 저장된 리플레이를 다 진행 할 동안 
        {
            _commandsHistory[_replayIdx].Execute(); //저장된 커맨드를 실행 
            _replayIdx++;//실행후 다음 인덷ㄱ스 검사
            
        }

        if (_replayIdx >= _commandsHistory.Count)  //저장된리플레이 다 사용한다면
        {
            Debug.Log($"리플레이가 완료되었습니다");
            _isReplaying = false;
        }
    }
    
    public void DeleteHistory()
    {
        Debug.Log("누적된 명령을 제거합니다");
        _commandsHistory.Clear();
    }
}
