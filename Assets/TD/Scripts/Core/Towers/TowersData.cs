using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TowersData", menuName = "ScriptableObjects/TowersData", order = 1)]
public class TowersData : ScriptableObject
{
    public List<TowerSettings> TowersSettings;
    
    private static TowersData _data;
    public static TowersData Data
    {
        get
        {
            if (_data == null)
            {
                _data = Resources.Load<TowersData>("TowersData");
            }

            return _data;
        }
    }
    
    public static TowerSettings GetData(int id)
    {
        return Data.TowersSettings.FirstOrDefault(x => x.Id == id);
    }
}

[Serializable]
public class TowerSettings
{
    public int Id;
    public string TowerName;
    public int Cost;
    public TowerView TowerView;
    public TowerPreview Preview;
}