using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildCost : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] Image sprite;
    public void InitialiseBuildCost(Sprite _sprite, int _cost)
    {
        cost.SetText(_cost.ToString());
        sprite.sprite = _sprite;
    }

    public void SetCostText(bool costMet)
    {

    }
}
