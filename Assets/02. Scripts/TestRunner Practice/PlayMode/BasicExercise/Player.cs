using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnDeath;
    public event Action<int> OnHPChange;

    private int _hp = 100;
    public int HP { get => _hp; set => _hp = value; }


    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        OnHPChange?.Invoke(_hp);
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
        OnDeath?.Invoke();
    }
}
