using UnityEngine;

public class Player : MonoBehaviour
{
    private int _hp = 100;
    public int HP { get => _hp; set => _hp = value; }


    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Die();
        }
    }

    public void Move(Vector3 offset)
    {
        transform.position += offset;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Player »ç¸Á");
    }
}
