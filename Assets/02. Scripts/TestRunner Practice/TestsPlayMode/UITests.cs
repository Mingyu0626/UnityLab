using NUnit.Framework;
using UnityEngine;
using TMPro;

public class UITests
{
    private GameObject _playerGO;
    private Player _player;

    private GameObject _uiGO;
    private GameUI _gameUI;

    private GameObject _tmpGO;
    private TextMeshProUGUI _tmpMock;

    [SetUp]
    public void SetUp()
    {
        // 1. Arrange
        _playerGO = new GameObject("Player");
        _player = _playerGO.AddComponent<Player>();

        _uiGO = new GameObject("GameUI");
        _gameUI = _uiGO.AddComponent<GameUI>();

        _tmpGO = new GameObject("Text");
        _tmpMock = _tmpGO.AddComponent<TextMeshProUGUI>();

        _gameUI.TextHP = _tmpMock;
        _gameUI.BindPlayer(_player);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_player);
        Object.DestroyImmediate(_uiGO);
        Object.DestroyImmediate(_tmpGO);
    }

    [Test]
    public void Player_TakeDamage_Updates_UIText()
    {
        // 2. Act
        _player.TakeDamage(10);

        // 3. Assert
        Assert.AreEqual("HP : 90", _tmpMock.text);
    }
}
