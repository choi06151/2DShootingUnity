using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePoint;
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
            CreateCommand createCommand=new CreateCommand(this.gameObject,BulletPrefab,FirePoint.position);
            CommandManager.Instance.ExecuteCommand(createCommand);
        }
    }
}
