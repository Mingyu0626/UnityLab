using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private int _damage = 10;
    public int Damage { get => _damage; set => _damage = value; }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (ReferenceEquals(player, null))
        {
            return;
        }
        player.TakeDamage(_damage);
        Debug.Log($"함정 발동! 플레이어 HP: {player.HP}");
    }
}
