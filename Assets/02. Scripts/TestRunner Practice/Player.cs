using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP { get; private set; } = 100;


    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }

    public void Move(Vector3 offset)
    {
        transform.position += offset;
    }
}
