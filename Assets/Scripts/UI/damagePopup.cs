using System.Collections;
using UnityEngine;
using TMPro;

public class damagePopup : MonoBehaviour
{
    private float spread = 0.6f;
    private float stayDuration = 0.5f;
    private float dissapearSpeed = 0.05f;
    private bool dissapearing = false;
    private float riseSpeed = 0.05f;
    private TextMeshPro textMesh;
    private Color textColor;
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(float trueDamage)
    {
        textMesh.SetText(Mathf.FloorToInt(trueDamage).ToString());
        transform.position += new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0f);
        textColor = textMesh.color;
        StartCoroutine(dissapearTimer(stayDuration));
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0f, riseSpeed, 0f);
        //reduce alpha of color
        if (dissapearing)
        {
            textColor.a -= dissapearSpeed;
            if (textColor.a < 0) Destroy(gameObject);
            textMesh.color = textColor;
        }
    }
    IEnumerator dissapearTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        dissapearing = true;
    }
}
