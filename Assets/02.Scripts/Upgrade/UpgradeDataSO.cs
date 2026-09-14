using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "UpgradeDataSO", menuName = "Scriptable Objects/UpgradeDataSO")]
public class UpgradeDataSO : ScriptableObject
{
    [Header("효과 내용")]
    [SerializeField] public string _title;

    [Header("최초 가격")]
    [SerializeField] public int _initScoreText;

    [Header("가격 증가량(배율)")]
    [SerializeField] public float _scoreMultiplier;

    [Header("효과량 (배율)")]
    [SerializeField] public float _effectAmount;
}