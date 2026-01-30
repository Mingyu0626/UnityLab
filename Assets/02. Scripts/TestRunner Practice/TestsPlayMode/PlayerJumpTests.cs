using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerJumpTests
{
    public class MockInput : IInputProvider
    {
        // 이 변수만 true로 바꾸면 인풋을 준 것과 똑같다.
        private bool _virtualSpaceKey = false;
        public bool VirtualSpaceKey
        {
            get => _virtualSpaceKey; 
            set => _virtualSpaceKey = value;
        }

        public bool IsKeyPressed(KeyCode keyCode)
        {
            return _virtualSpaceKey;
        }
    }

    private GameObject _gameObject;
    private PlayerJump _playerJump;

    [SetUp]
    public void SetUp()
    {
        _gameObject = new GameObject();
        _playerJump = _gameObject.AddComponent<PlayerJump>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_gameObject);
    }

    [UnityTest]
    public IEnumerator WhenSpacePressed()
    {
        // 1. Arrange, 가짜 키보드 장착
        MockInput fakeInput = new MockInput();
        _playerJump.Initialize(fakeInput);

        // 2. Act
        fakeInput.VirtualSpaceKey = true;
        yield return null;

        // 3. Assert
        Assert.IsTrue(_playerJump.IsJumping, "키를 눌렀는데 점프하지 않음");
    }

    [UnityTest]
    public IEnumerator WhenSpaceNotPressed()
    {
        // 1. Arrange, 가짜 키보드 장착
        MockInput fakeInput = new MockInput();
        _playerJump.Initialize(fakeInput);

        // 2. Act
        fakeInput.VirtualSpaceKey = false;
        yield return null;

        // 3. Assert
        Assert.IsFalse(_playerJump.IsJumping, "키를 안 눌렀는데 점프함");
    }
}
