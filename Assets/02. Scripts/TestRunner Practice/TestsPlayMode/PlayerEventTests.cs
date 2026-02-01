using NUnit.Framework;
using UnityEngine;

public class PlayerEventTests 
{
    private GameObject _playerGO;
    private Player _player;
    private bool _isEventCalled;

    [SetUp]
    public void SetUp()
    {
        // 1. Arrange
        _playerGO = new GameObject("TestPlayer");
        _player = _playerGO.AddComponent<Player>();
        _isEventCalled = false;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_playerGO);
    }

    [Test]
    public void TakeDamage_InvokesOnDeathEvent_WhenHPIsZero()
    {
        // 1. Arrange
        _player.OnDeath += () =>
        {
            _isEventCalled = true;
        };

        // 2. Act
        _player.TakeDamage(100);

        // 3. Assert
        Assert.IsTrue(_isEventCalled, "플레이어가 죽었음에도 OnDeath 이벤트가 호출되지 않음");
    }
}
