using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class Saver
{
    public static void SaveLevel(Grid grid)
    {
        var data = JsonConvert.SerializeObject(grid);
        
        var saveFile = Application.streamingAssetsPath + "/" + "grid"+ ".json";
        
        if(File.Exists(saveFile))
        {
            File.Delete(saveFile);
        }
        
        File.WriteAllText(saveFile, data);
    }

    public static Grid LoadLevel(int id)
    {
        var saveFile = Application.streamingAssetsPath + "/" + id + ".json";
        
        if(File.Exists(saveFile))
        {
            var data = File.ReadAllText(saveFile);
            var grid = JsonConvert.DeserializeObject<Grid>(data);
            return grid;
        }
        else
        {
            return null;
        }
    }
}