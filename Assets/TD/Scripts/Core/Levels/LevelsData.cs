using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObjects/LevelsData", order = 3)]
public class LevelsData : ScriptableObject
{
    public List<Level> Levels;
    
    private static LevelsData _data;
    public static LevelsData Data
    {
        get
        {
            if (_data == null)
            {
                _data = Resources.Load<LevelsData>("LevelsData");
            }

            return _data;
        }
    }
}

[Serializable]
public class Level
{
    public string LevelName;
    public int StertedMoney;
    public List<Wave> Waves;
}

[Serializable]
public class Wave
{
    public int EnemyId;
    public int EnemyCount;
}