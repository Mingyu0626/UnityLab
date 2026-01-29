using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerTests
{
    public GameObject GO { get; private set; }
    public Player Player { get; private set; }
    
    [SetUp]
    public void SetUp()
    {
        // 1. Arrange
        GO = new GameObject("TestPlayer");
        Player = GO.AddComponent<Player>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(GO);
    }


    [Test]
    public void Player_TakeDamage_ReduceHP()
    {
        // 2. Act
        Player.TakeDamage(10);
        // 3. Assert
        Assert.AreEqual(90, Player.HP);
    }

    [Test]
    public void Player_Move_ChangesPosition()
    {
        Player.transform.position = Vector3.zero;

        // 2. Act
        Player.Move(new Vector3(5, 0, 0));

        // 3. Assert
        Assert.AreEqual(new Vector3(5, 0, 0), Player.transform.position);
    }

    [UnityTest]
    public IEnumerator Player_Update_MovesForward()
    {
        // 1. Arrange
        Player.transform.position = Vector3.zero;

        // 2. Act
        // 테스트에서의 시간 흐름은 WaitForSeconds로 제어한다.
        yield return new WaitForSeconds(0.5f);

        // 3. Assert
        Assert.Greater(Player.transform.position.z, 0f);
        Debug.Log($"0.5초 후 위치 : {Player.transform.position.z}");
    }
}
