using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class sliderHandling : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    private Color fillCol;
    [SerializeField] private Image bg;
    private Color bgCol;
    public bool fading = false;
    private float fadeAfter = 5;
    private float dissapearSpeed = 0.05f;

    public void Awake()
    {
        fillCol = fill.color;
        bgCol = bg.color;
        if (fading) changeAlphas(0, 0);
    }
 
    public void updateSlider(float value, float valueMax)
    {
        slider.value = Mathf.Max(value, 0)/valueMax;
        changeAlphas(1, 1);
        StartCoroutine(fadeDelay());
    }
    private void FixedUpdate()
    {
        if (fading)
        {
            changeAlphas(fillCol.a - dissapearSpeed, bgCol.a - dissapearSpeed);
            if (fillCol.a <= 0) fading = false;
        }
    }
    private void changeAlphas(float fillA, float bgA)
    {
        fillCol.a = fillA;
        bgCol.a = bgA;
        fill.color = fillCol;
        bg.color = bgCol;
    }
    private IEnumerator fadeDelay()
    {
        yield return new WaitForSeconds(fadeAfter);
        fading = true;
    }
}
