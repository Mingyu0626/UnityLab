using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerJumpTests
{
    public class MockInput : IInputProvider
    {
        // 이 변수만 true로 바꾸면 인풋을 준 것과 똑같다.
        private bool _virtualKey = false;
        public bool VirtualKey
        {
            get => _virtualKey; 
            set => _virtualKey = value;
        }

        public bool IsKeyPressed(KeyCode keyCode) => _virtualKey;
    }

    private GameObject _gameObject;
    private PlayerJump _playerJump;
    private Rigidbody _rigidbody;
    private MockInput _mockInput;

    [SetUp]
    public void SetUp()
    {
        _gameObject = new GameObject();
        _playerJump = _gameObject.AddComponent<PlayerJump>();
        _rigidbody = _gameObject.GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;

        // 1. Arrange
        _mockInput = new MockInput();
        _playerJump.Initialize(_mockInput);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_gameObject);
    }

    [UnityTest]
    public IEnumerator WhenSpacePressed()
    {
        // 2. Act
        _mockInput.VirtualKey = true;
        yield return null;

        // 3. Assert
        Assert.IsTrue(_playerJump.IsJumping, "키를 눌렀는데 점프하지 않음");
    }

    [UnityTest]
    public IEnumerator WhenSpaceNotPressed()
    {
        // 2. Act
        _mockInput.VirtualKey = false;
        yield return null;

        // 3. Assert
        Assert.IsFalse(_playerJump.IsJumping, "키를 안 눌렀는데 점프함");
    }

    [UnityTest]
    public IEnumerator AddForce_MovesPlayerUpward()
    {
        Assert.AreEqual(0, _gameObject.transform.position.y, 0.01f);

        // 2. Act
        _mockInput.VirtualKey = true;
        yield return null;
        _rigidbody.useGravity = true;

        // Physics는 FixedUpdate 주기로 돌기 때문에, WaitForFixedUpdate를 사용하여 대기
        float jumpSimulationTime = 0.1f;
        int framesToWait = Mathf.CeilToInt(jumpSimulationTime / Time.fixedDeltaTime);
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        for (int i = 0; i < framesToWait; ++i)
        {
            yield return waitForFixedUpdate;
        }

        // 3. Assert
        Assert.Greater(_gameObject.transform.position.y, 0.05f, "점프했지만 플레이어가 위로 뜨지 않음");
        Debug.Log($"0.1초 후 높이: {_gameObject.transform.position.y}");
    }
}
