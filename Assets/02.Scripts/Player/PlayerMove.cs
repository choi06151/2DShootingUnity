using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IPlayerFun
{
    //목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고싶다 


    private Player _player;
    private Transform _recordStartTransform;
    private float _moveSpeedDownMultiplier;
    private float _moveSpeedUpMultiplier;
    public float[] constraintXRange = new float[2] { -2.5f, 2.5f };

    private void Start()
    {
    }

    public void Init(Player player)
    {
        _player = player;
        _moveSpeedDownMultiplier = 1.0f - _player.SpeedMultiplier;
        _moveSpeedUpMultiplier = 1.0f + _player.SpeedMultiplier;
    }


    void Update()
    {
        PlayerMovementCheck();
    }


    private void PlayerMovementCheck()
    {
        float h = Input.GetAxisRaw("Horizontal"); //키보드 왼/ 오른쪽 입력상태에 따라 -1f ~ 1f
        float v = Input.GetAxisRaw("Vertical"); //키보드 위 /아래 -1f~1f
        Vector2 normalDirection = new Vector2(0, 0);
        if (h != 0 || v != 0) //이동 인풋이 들어온다면
        {
            normalDirection = new Vector2(h, v); //현재 방향과 속도에 따라 이동한다
            Vector2 normalizedDirection = normalDirection.normalized; //정규화
            Vector3 nextPosition = transform.position +
                                   (Vector3)normalizedDirection * Time.deltaTime * _player.Stat.MoveSpeed;

            if (nextPosition.y >= _player.Stat.PlayerYRangeMin &&
                nextPosition.y <= _player.Stat.PlayerYRangeMax) //범위 내부여야만 이동
            {
                MovementCommand movementCommand = new MovementCommand(this.gameObject,
                    (Vector3)normalizedDirection * Time.deltaTime * _player.Stat.MoveSpeed);
                CommandManager.Instance.ExecuteCommand(movementCommand);
            }

            if (nextPosition.x <= constraintXRange[0] || nextPosition.x >= constraintXRange[1]) //범위 내부여야만 이동
            {
                float convertedX = nextPosition.x * -1; //x좌표 위치 바꾸기
                Vector3 convertedPosition = new Vector3(convertedX, transform.position.y, transform.position.z);


                TeleportCommand teleportCommand = new TeleportCommand(this.gameObject, convertedPosition);
                CommandManager.Instance.ExecuteCommand(teleportCommand);
            }
        }

        _player.UpdateAnimState((int)normalDirection.x, false, false);
    }

    public void PlayerMovementInput(float h, float v)
    {
        Vector2 normalDirection = new Vector2(0, 0);
        if (h != 0 || v != 0) //이동 인풋이 들어온다면
        {
            normalDirection = new Vector2(h, v); //현재 방향과 속도에 따라 이동한다
            Vector2 normalizedDirection = normalDirection.normalized; //정규화
            Vector3 nextPosition = transform.position +
                                   (Vector3)normalizedDirection * Time.deltaTime * _player.Stat.MoveSpeed;

            if (nextPosition.y >= _player.Stat.PlayerYRangeMin &&
                nextPosition.y <= _player.Stat.PlayerYRangeMax) //범위 내부여야만 이동
            {
                MovementCommand movementCommand = new MovementCommand(this.gameObject,
                    (Vector3)normalizedDirection * Time.deltaTime * _player.Stat.MoveSpeed);
                CommandManager.Instance.ExecuteCommand(movementCommand);
            }

            if (nextPosition.x <= constraintXRange[0] || nextPosition.x >= constraintXRange[1]) //범위 내부여야만 이동
            {
                float convertedX = nextPosition.x * -1; //x좌표 위치 바꾸기
                Vector3 convertedPosition = new Vector3(convertedX, transform.position.y, transform.position.z);


                TeleportCommand teleportCommand = new TeleportCommand(this.gameObject, convertedPosition);
                CommandManager.Instance.ExecuteCommand(teleportCommand);
            }
        }

        _player.UpdateAnimState((int)normalDirection.x, false, false);
    }
}