using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerJumpTests
{
    public class MockInput : IInputProvider
    {
        // 이 변수만 true로 바꾸면 인풋을 준 것과 똑같다.
        public bool VirtualSpaceKey { get; set; } = false;

        public bool IsKeyPressed(KeyCode keyCode)
        {
            return VirtualSpaceKey;
        }
    }

    public GameObject GO;
    public PlayerJump PlayerJump;

    [SetUp]
    public void SetUp()
    {
        GO = new GameObject();
        PlayerJump = GO.AddComponent<PlayerJump>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(GO);
    }

    [UnityTest]
    public IEnumerator WhenSpacePressed()
    {
        // 1. Arrange, 가짜 키보드 장착
        MockInput fakeInput = new MockInput();
        PlayerJump.Initialize(fakeInput);

        // 2. Act
        fakeInput.VirtualSpaceKey = true;
        yield return null;

        // 3. Assert
        Assert.IsTrue(PlayerJump.IsJumping, "키를 눌렀는데 점프하지 않음");
    }

    [UnityTest]
    public IEnumerator WhenSpaceNotPressed()
    {
        // 1. Arrange, 가짜 키보드 장착
        MockInput fakeInput = new MockInput();
        PlayerJump.Initialize(fakeInput);

        // 2. Act
        fakeInput.VirtualSpaceKey = false;
        yield return null;

        // 3. Assert
        Assert.IsFalse(PlayerJump.IsJumping, "키를 안 눌렀는데 점프함");
    }
}
