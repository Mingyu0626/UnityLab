using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSpeed = 100f;
    
    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -_rotationSpeed * Time.deltaTime);
            return;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
            return;
        }
    }
}