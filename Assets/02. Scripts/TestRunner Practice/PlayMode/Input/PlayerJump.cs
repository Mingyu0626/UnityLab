using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private float _jumpPower = 5f;
    private bool _jumpRequested = false;
    private bool _isJumping = false;
    public bool IsJumping
    {
        get => _isJumping; set => _isJumping = value;
    }

    private IInputProvider _input;
    private Rigidbody _rigidbody;

    public void Initialize(IInputProvider inputProvider = null)
    {
        // 인수로 받은 IInputProvider가 없으면, 진짜 유니티 Input을 사용한다.
        _input = inputProvider ?? new UnityInputProvider();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
            _jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (_jumpRequested)
        {
            Jump();
        }
    }

    public void Jump()
    {
        _isJumping = true;
        _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        _jumpRequested = false;
    }
}
