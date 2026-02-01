using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textHp;

    public TextMeshProUGUI TextHP { get => _textHp; set => _textHp = value; }

    public void BindPlayer(Player player)
    {
        player.OnHPChange += RefreshHPText;
    }

    private void RefreshHPText(int currrentHP)
    {
        if (_textHp == null)
        {
            return;
        }
        _textHp.text = $"HP : {currrentHP}";
    }
}
