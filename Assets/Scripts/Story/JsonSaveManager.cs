using UnityEngine;
using System.IO;
public static class JsonSaveManager<T>
{
    static string SavePath(string path) => $"{Application.dataPath}/{path}.json";      

    public static void Save(T data, string path)
    {
        using StreamWriter sw = new StreamWriter(SavePath(path), false);
        var jsonstr = JsonUtility.ToJson(data, true);
        sw.Write(jsonstr);
        sw.Flush();
    }

    public static T Load(string path)
    {
        if (!File.Exists(SavePath(path))) return default; //データが存在する場合は返す
        using StreamReader sr = new StreamReader(SavePath(path));
        var datastr = sr.ReadToEnd();
        return JsonUtility.FromJson<T>(datastr);

        //存在しない場合はdefaultを返却
    }
}
