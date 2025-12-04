using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 2f;

    public enum AxisName
    {
        Horizontal,
        Vertical
    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis(nameof(AxisName.Horizontal));
        float vertical = Input.GetAxis(nameof(AxisName.Vertical));
        Vector3 movement = new Vector3(horizontal, 0, vertical) * _moveSpeed * Time.deltaTime;
        transform.position += movement;
    }
}