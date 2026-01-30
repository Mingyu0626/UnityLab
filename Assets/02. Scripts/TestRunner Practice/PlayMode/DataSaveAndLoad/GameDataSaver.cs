using UnityEngine;

public class GameDataSaver : IDataSaver
{
    public void Save(string key, int value)
    {
        Debug.Log($"[디스크 쓰기] {key} : {value} 저장 중...");
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }
}
