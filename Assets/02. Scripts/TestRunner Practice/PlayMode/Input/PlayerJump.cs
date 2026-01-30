using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool IsJumping { get; private set; } = false;

    private IInputProvider _input;

    public void Initialize(IInputProvider inputProvider = null)
    {
        // 인수로 받은 IInputProvider가 없으면, 진짜 유니티 Input을 사용한다.
        _input = inputProvider ?? new UnityInputProvider();
    }

    private void Start()
    {
        if (_input == null)
        {
            Initialize();
        }
    }

    private void Update()
    {
        if (_input.IsKeyPressed(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        IsJumping = true;
        Debug.Log("점프!");
    }
}
