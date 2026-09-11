using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour, IPlayerFun
{
    private PlayerSkill _curEquipedSkill;
    private GameObject _curEquipedSkillPrefab;
    private float _skillCoolTime;
    private float _curCoolTime;
    private Player _player;

    [Header("현재 플레이어 스킬 후보 프리팹")]
    [SerializeField] private List<GameObject> _playerSkillPrefab = new List<GameObject>();

    public List<GameObject> PlayerSkillPrefab => _playerSkillPrefab;

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


        if (_player.Stat.IsPlayerAutoSkillOn && _curCoolTime < 0)
        {
            CheckUseSkill();
        }

        _curCoolTime -= Time.deltaTime;
    }

    public void Init(Player player)
    {
        _player = player;
        GetRandomSkill();
    }

    private void GetRandomSkill()
    {
        int randomIdx = Random.Range(0, PlayerSkillPrefab.Count);
        _curEquipedSkillPrefab = PlayerSkillPrefab[randomIdx];
        _curEquipedSkill = _curEquipedSkillPrefab.GetComponent<PlayerSkill>();
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
            _curEquipedSkillPrefab,
            transform.position,
            Quaternion.identity
        );

        GetRandomSkill();
    }
}