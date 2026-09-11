using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Stat _stat;

    private bool _isDataConfirm;

    private UnityWebRequest _request;
    private string _csvText;

    private const string DATAURL =
        "https://docs.google.com/spreadsheets/d/1b4pWvVb83pEkaKJxMEhgRpJgua_qPNRx9pwC9JOXdLc/export?format=csv&gid=0";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartLoadCsv();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDataConfirm)
        {
            LoadCsvData(_csvText);
        }
        else
        {
            _csvText = GetCsvData();
        }
    }


    private void StartLoadCsv()
    {
        _request = UnityWebRequest.Get(DATAURL);
        _request.SendWebRequest();
    }

    private string GetCsvData()
    {
        if (_request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"CSV Load Failed : {_request.error}");
            return null;
        }

        _isDataConfirm = true;

        return _request.downloadHandler.text;
    }

    private void LoadCsvData(string csvText)
    {
        _isDataConfirm = false;
        _stat = new Stat();

        string[] lines = csvText.Split('\n');

        // 0번은 "내용,수치" 헤더니까 제외
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] values = lines[i].Split(',');

            if (values.Length < 2)
                continue;

            string key = values[0].Trim();
            string value = values[1].Trim();

            switch (key)
            {
                case "이동 속도":
                    _stat.MoveSpeed = float.Parse(value);
                    break;

                case "체력":
                    _stat.Hp = float.Parse(value);
                    break;

                case "공격력(배율)":
                    _stat.Damage = float.Parse(value);
                    break;

                case "총알 쿨타임":
                    _stat.BulletCoolTime = float.Parse(value);
                    break;

                case "총알 발사 위치 간격":
                    _stat.BulletInterval = float.Parse(value);
                    break;

                case "총알 발사 개수":
                    _stat.BulletCount = int.Parse(value);
                    break;

                case "총알 업그레이드 기준 개수":
                    _stat.BulletUpgradeCount = int.Parse(value);
                    break;

                case "총알 자동 발사 기능":
                    _stat.IsBulletAutoFireOn = bool.Parse(value);
                    break;

                case "자동 움직임 기능":
                    _stat.IsPlayerAutoMoveOn = bool.Parse(value);
                    break;

                case "자동 스킬 사용 기능":
                    _stat.IsPlayerAutoSkillOn = bool.Parse(value);
                    break;
                case "팔로워 생성 간격":
                {
                    _stat.FollowerInterval = float.Parse(value);
                    break;
                }
                case "플레이어 Y범위 최솟값":
                {
                    _stat.PlayerYRangeMin = float.Parse(value);
                    break;
                }
                case "플레이어 Y범위 최댓값":
                {
                    _stat.PlayerYRangeMax = float.Parse(value);

                    break;
                }
            }
        }


        ApplyToPlayer();
    }


    private void ApplyToPlayer()
    {
        _player.ApplyPlayerData(_stat);
    }
}