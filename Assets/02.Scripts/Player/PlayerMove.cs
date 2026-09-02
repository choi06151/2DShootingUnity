using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고싶다 


    
     private Transform _recordStartTransform;
     private float _currentSpeed;
     
     public float MoveSpeed = 3f;
     public float MoveSpeedDownMultiplier = 0.5f;
     public float MoveSpeedUpMultiplier = 1.5f;
     public float[] constraintYRange = new float[2]{-5f,-1.5f}; //-1.5 , -5 //이동 가능범위
     public float[] constraintXRange = new float[2]{-2.5f,2.5f};


     private void Start()
     {
         InitForRecord();
     }

     void Update()
     {
         PlayerMovementCheck();
         PlayerSpeedCheck();
         

     }

     private void PlayerSpeedCheck()
     {
         if (Input.GetKey(KeyCode.Q))
         {
             _currentSpeed=MoveSpeed*MoveSpeedDownMultiplier;
         }
         else if (Input.GetKey(KeyCode.E))
         {
             _currentSpeed=MoveSpeed*MoveSpeedUpMultiplier;
         }
         else
         {
             _currentSpeed=MoveSpeed;
         }
         
     }
     
    private void PlayerMovementCheck()
    {
        float h=Input.GetAxisRaw("Horizontal"); //키보드 왼/ 오른쪽 입력상태에 따라 -1f ~ 1f
        float v = Input.GetAxisRaw("Vertical"); //키보드 위 /아래 -1f~1f
        
        if (h != 0 || v != 0) //이동 인풋이 들어온다면
        {
            Vector2 normalDirection = new Vector2(h,v);         //현재 방향과 속도에 따라 이동한다
            Vector2 normalizedDirection = normalDirection.normalized; //정규화
            Vector3 nextPosition=transform.position+(Vector3)normalizedDirection * Time.deltaTime * _currentSpeed;

            if (nextPosition.y >= constraintYRange[0] && nextPosition.y <= constraintYRange[1]) //범위 내부여야만 이동
            {
                MovementCommand movementCommand=new MovementCommand(this.gameObject,(Vector3)normalizedDirection * Time.deltaTime * _currentSpeed);
                CommandManager.Instance.ExecuteCommand(movementCommand);

            }
        
            if (nextPosition.x <= constraintXRange[0] || nextPosition.x >= constraintXRange[1]) //범위 내부여야만 이동
            {
                float convertedX=nextPosition.x*-1; //x좌표 위치 바꾸기
                Vector3 convertedPosition = new Vector3(convertedX, transform.position.y, transform.position.z);
                
                
                TeleportCommand teleportCommand=new TeleportCommand(this.gameObject,convertedPosition);
                CommandManager.Instance.ExecuteCommand(teleportCommand);
            }

     
            
        }
    }


    public void InitForRecord()
    {
        TeleportCommand teleportCommand=new TeleportCommand(this.gameObject,transform.position);
        CommandManager.Instance.ExecuteCommand(teleportCommand);
    }
    public void Move(Vector3 moveDirection)
    {
        transform.Translate(moveDirection);
    }

    public void Teleport(Vector3 teleportPosition)
    {
        transform.position = teleportPosition;
    }
    
    
}
