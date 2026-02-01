using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;


public class TrapTests
{
    private GameObject _playerGO;
    private Player _player;
    private GameObject _trapGO;
    private Trap _trap;

    [SetUp]
    public void SetUpPlayer()
    {
        _playerGO = new GameObject("Player");
        _playerGO.transform.position = new Vector3(-100, 0, 0);
        _player = _playerGO.AddComponent<Player>();

        Rigidbody playerRb = _playerGO.AddComponent<Rigidbody>();
        playerRb.useGravity = false;
        _playerGO.AddComponent<BoxCollider>();
    }

    [SetUp]
    public void SetUpTrap()
    {
        _trapGO = new GameObject("Trap");
        _trapGO.transform.position = new Vector3(100, 0, 0);
        _trap = _trapGO.AddComponent<Trap>();

        BoxCollider trapCollider = _trapGO.AddComponent<BoxCollider>();
        trapCollider.isTrigger = true;
        trapCollider.size = new Vector3(2, 2, 2);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_playerGO);
        Object.DestroyImmediate(_trapGO);
    }

    [UnityTest]
    public IEnumerator Player_EnterTrap_ReduceHP()
    {
        // 1. Arrange
        _playerGO.transform.position = new Vector3(0, 0, 0);
        _trapGO.transform.position = new Vector3(10, 0, 0);

        yield return new WaitForFixedUpdate();

        // 2. Act
        _playerGO.transform.position = _trapGO.transform.position;

        // 충돌 이벤트(OnTriggerEnter)는 물리 연산 후에 발생하기 때문에, 
        // 물리 프레임만큼 한번 더 대기한다.
        yield return new WaitForFixedUpdate();

        // 3. Assert
        Assert.AreEqual(90, _player.HP, "Trap을 밟았음에도 HP가 깎이지 않음");
    }
}
