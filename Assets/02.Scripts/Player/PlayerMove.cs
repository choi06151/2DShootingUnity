using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고싶다 



     public float MoveSpeed = 3f;
     public float[] constraintYRange = new float[2]{-5f,-1.5f}; //-1.5 , -5 //이동 가능범위
     public float[] constraintXRange = new float[2]{-2.5f,2.5f};

     void Update()
    {
        float h=Input.GetAxisRaw("Horizontal"); //키보드 왼/ 오른쪽 입력상태에 따라 -1f ~ 1f
        float v = Input.GetAxisRaw("Vertical"); //키보드 위 /아래 -1f~1f
        //Raw를 추가하면 중간값 없어짐 
        //Debug.Log($"h:{h} v:{v}");
        
        Vector2 normalDirection = new Vector2(h,v);         //현재 방향과 속도에 따라 이동한다
        Vector2 normalizedDirection = normalDirection.normalized; //정규화
        Vector3 nextPosition=transform.position+(Vector3)normalizedDirection * Time.deltaTime * MoveSpeed;

        if (nextPosition.y >= constraintYRange[0] && nextPosition.y <= constraintYRange[1]) //범위 내부여야만 이동
        {
            transform.Translate(normalizedDirection * Time.deltaTime * MoveSpeed);

        }
        
        if (nextPosition.x <= constraintXRange[0] || nextPosition.x >= constraintXRange[1]) //범위 내부여야만 이동
        {
            float convertedX=nextPosition.x*-1; //x좌표 위치 바꾸기
            Vector3 convertedPosition = new Vector3(convertedX, transform.position.y, transform.position.z);
            transform.position=convertedPosition; //해당 좌표로 순간이동
        }

        
          
          
      
      
    }
}
