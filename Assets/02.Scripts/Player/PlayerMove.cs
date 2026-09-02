using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고싶다 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    // Update is called once per frame

    [SerializeField]private float _moveSpeed = 3f;
    void Update()
    {
        float h=Input.GetAxis("Horizontal"); //키보드 왼/ 오른쪽 입력상태에 따라 -1f ~ 1f
        float v = Input.GetAxis("Vertical"); //키보드 위 /아래 -1f~1f
        Debug.Log($"h:{h} v:{v}");
        
        Vector2 direction = new Vector2(h,v); // 왼쪽방향
                                              //현재 방향과 속도에 따라 이동한다
        transform.Translate(direction * Time.deltaTime*_moveSpeed);
          
          
      
      
    }
}
