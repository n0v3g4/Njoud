using UnityEngine;
using UnityEngine.UI;

public class sliderHandling : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void updateSlider(float value, float valueMax)
    {
        slider.value = Mathf.Max(value, 0)/valueMax;
    }
}
