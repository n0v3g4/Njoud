using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildCost : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image image;
    private Color costMetCol = new Color(0.1f, 0.1f, 0.1f);
    private Color costNotMetCol = new Color(1, 0.25f, 0.25f);
    [HideInInspector] public itemData item;
    [HideInInspector] public int cost;
    public void InitialiseBuildCost(itemData _item, int _cost)
    {
        cost = _cost;
        costText.SetText(_cost.ToString());
        costText.color = costNotMetCol;
        item = _item;
        image.sprite = item.image;
    }

    public void SetCostText(bool costMet)
    {
        costText.color = costMet ? costMetCol : costNotMetCol;
        Debug.Log("the cost mat was" + costMet);
    }
}
