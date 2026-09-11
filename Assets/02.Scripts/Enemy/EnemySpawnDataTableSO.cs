using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnDataTableSO", menuName = "Scriptable Objects/EnemySpawnDataTableSO")]
public class EnemySpawnDataTableSO : ScriptableObject
{
    public List<EnemySpawnData> Datas;
}