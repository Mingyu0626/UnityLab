using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelSystemTests : MonoBehaviour
{
    public class DataSaverMock : IDataSaver
    {
        public string LastSavedKey {  get; private set; }
        public int LastSavedValue { get; private set; }
        public bool IsSaveCalled { get; private set; } = false;

        public void Save(string key, int value)
        {
            IsSaveCalled = true; 
            LastSavedKey = key; 
            LastSavedValue = value;
            Debug.Log($"MockDataSaver의 Save 메서드 호출됨(실제로 데이터가 저장되지는 않음)");
        }

        [Test]
        public void LevelUp_SaveDatas_UsingMock()
        {
            // 1. Arrange
            GameObject go = new GameObject();
            LevelSystem levelSystem = go.AddComponent<LevelSystem>();

            // 진짜 GameDataSaver 대신 DataSaverMock을 주입한다.
            DataSaverMock mockSaver = new DataSaverMock();
            levelSystem.Initialize(mockSaver);

            // 2. Act
            levelSystem.LevelUp();

            // 3. Assert
            // 3-1. 레벨업 확인
            Assert.AreEqual(2, levelSystem.Level);

            // 3-2. 데이터 저장 여부 확인
            Assert.IsTrue(mockSaver.IsSaveCalled, $"Save 메서드가 호출되지 않음");
            Assert.AreEqual("PlayerLevel", mockSaver.LastSavedKey);
            Assert.AreEqual(2, mockSaver.LastSavedValue);

            // Cleanup
            DestroyImmediate(go);
        }
    }
}
