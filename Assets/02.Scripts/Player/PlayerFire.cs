using System.Threading;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject MainBulletPrefab;
    public const int MainBulletCount=2;
    public Transform[] MainFirePoint=new Transform[MainBulletCount];
    
    public GameObject SubBulletPrefab;
    public const int SubBulletCount=2;
    public Transform[] SubFirePoint=new Transform[MainBulletCount];

    public float FireCoolTime=0.1f;
    
    private bool _isAutoFire=false;
    private float _currentFireCooldown=999f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckFire();
        CheckAutoFire();
    }

    private void CheckFire()
    {

        UpdateFireCoolTime();
        
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoFire = !_isAutoFire;
            if (_isAutoFire)
            {
                Debug.Log("Auto Fire활성화");
            }
            else
            {
                Debug.Log("Auto Fire비활성화");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space)&&CheckFireCoolTime())
        {
            FireAllBullet();
        }
        
    }
    
    private void FireAllBullet()
    {
        _currentFireCooldown = 0;

        FireMainBullet();
        FireSubBullet();
    }
    
    private void FireMainBullet()
    {
        for (int i = 0; i < MainBulletCount; i++)
        {
            CreateCommand createCommand=new CreateCommand(this.gameObject,MainBulletPrefab,MainFirePoint[i].position);
            CommandManager.Instance.ExecuteCommand(createCommand);
        }
    }
    

    private void FireSubBullet()
    {
        for (int i = 0; i < SubBulletCount; i++)
        {
            CreateCommand createCommand=new CreateCommand(this.gameObject,SubBulletPrefab,SubFirePoint[i].position);
            CommandManager.Instance.ExecuteCommand(createCommand);
        }
    }

    
    private void CheckAutoFire()
    {
        if (_isAutoFire)
        {
            if (_currentFireCooldown > FireCoolTime)
            {

                FireAllBullet();
            }
        }
    }

    private bool CheckFireCoolTime()
    {
        return _currentFireCooldown > FireCoolTime;
    }

    private void UpdateFireCoolTime()
    {
        _currentFireCooldown+=Time.deltaTime;
    }
    
}
