using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject MainBulletPrefab;
    public static int MainBulletCount=2;
    public Transform[] MainFirePoint=new Transform[MainBulletCount];
    
    public GameObject SubBulletPrefab;
    public static int SubBulletCount=2;
    public Transform[] SubFirePoint=new Transform[MainBulletCount];

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckFire();
    }

    private void CheckFire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < MainBulletCount; i++)
            {
                CreateCommand createCommand=new CreateCommand(this.gameObject,MainBulletPrefab,MainFirePoint[i].position);
                CommandManager.Instance.ExecuteCommand(createCommand);
            }
            
            for (int i = 0; i < SubBulletCount; i++)
            {
                CreateCommand createCommand=new CreateCommand(this.gameObject,SubBulletPrefab,SubFirePoint[i].position);
                CommandManager.Instance.ExecuteCommand(createCommand);

            }
            
        }
    }
    
    
}
