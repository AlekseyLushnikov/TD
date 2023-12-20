using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "ScriptableObjects/EnemiesData", order = 2)]
public class EnemiesData : ScriptableObject
{
    public List<EnemyData> Enemies;
    
    private static EnemiesData _data;
    public static EnemiesData Data
    {
        get
        {
            if (_data == null)
            {
                _data = Resources.Load<EnemiesData>("EnemiesData");
            }

            return _data;
        }
    }

    public static EnemyData GetData(int id)
    {
        return Data.Enemies.FirstOrDefault(x => x.Id == id);
    }
}

[Serializable]
public class EnemyData
{
    public int Id;
    public string EnemyName;
    public EnemySettings Settings;
}

[Serializable]
public struct EnemySettings
{
    public int StartingHealth;
    public EnemyView EnemyViewPrefab;
}