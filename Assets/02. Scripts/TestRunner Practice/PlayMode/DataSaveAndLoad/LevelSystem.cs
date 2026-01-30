using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public int Level { get; private set; } = 1;
    private IDataSaver _saver;

    public void Initialize(IDataSaver saver)
    {
        _saver = saver;
    }

    public void LevelUp()
    {
        Level++;
        _saver?.Save("PlayerLevel", Level);
    }
}
