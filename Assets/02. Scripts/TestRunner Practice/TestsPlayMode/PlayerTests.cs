using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerTests
{
    private GameObject _playerGO;
    private Player _player;
    
    [SetUp]
    public void SetUp()
    {
        // 1. Arrange
        _playerGO = new GameObject("TestPlayer");
        _player = _playerGO.AddComponent<Player>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_playerGO);
    }


    [Test]
    public void Player_TakeDamage_ReduceHP()
    {
        // 2. Act
        _player.TakeDamage(10);
        // 3. Assert
        Assert.AreEqual(90, _player.HP);
    }

    [Test]
    public void Player_Move_ChangesPosition()
    {
        _player.transform.position = Vector3.zero;

        // 2. Act
        _player.Move(new Vector3(5, 0, 0));

        // 3. Assert
        Assert.AreEqual(new Vector3(5, 0, 0), _player.transform.position);
    }

    [UnityTest]
    public IEnumerator Player_Update_MovesForward()
    {
        // 1. Arrange
        _player.transform.position = Vector3.zero;

        // 2. Act
        // 테스트에서의 시간 흐름은 WaitForSeconds로 제어한다.
        yield return new WaitForSeconds(0.5f);

        // 3. Assert
        Assert.Greater(_player.transform.position.z, 0f);
        Debug.Log($"0.5초 후 위치 : {_player.transform.position.z}");
    }
}
