using UnityEngine;

public class PlayerSkillManager : MonoBehaviour, IPlayerFun
{
    private PlayerSkill _curEquipedSkill;
    private float _skillCoolTime;
    private float _curCoolTime;
    private Player _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            CheckUseSkill();
        }

        _curCoolTime -= Time.deltaTime;
    }

    public void Init(Player player)
    {
        _player = player;
        _curEquipedSkill = _player.PlayerSkillPrefab.GetComponent<PlayerSkill>();
        _skillCoolTime = _curEquipedSkill.SkillCoolTime;
    }


    public void CheckUseSkill()
    {
        if (IsEnableUseSkill())
        {
            Debug.Log("스킬 사용됌");
            SpawnSkill();
        }
    }

    private bool IsEnableUseSkill()
    {
        if (_curCoolTime < 0)
        {
            _curCoolTime = _skillCoolTime;

            return true;
        }

        Debug.Log(_curCoolTime);
        return false;
    }

    private void SpawnSkill()
    {
        GameObject skill = Instantiate(
            _player.PlayerSkillPrefab,
            _player.BulletSpawnPoint.position,
            Quaternion.identity
        );
    }
}