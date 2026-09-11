using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSpawnDataTableSO", menuName = "Scriptable Objects/ItemSpawnDataTableSO")]
public class ItemSpawnDataTableSO : ScriptableObject
{
    public List<ItemSpawnData> Datas;
}