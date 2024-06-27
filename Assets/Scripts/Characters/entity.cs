using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class entity : MonoBehaviour
{
    public Rigidbody2D rb;
    public Dictionary<string, float> entityStats = new Dictionary<string, float>();
    public Dictionary<string, elementArray> entityElements = new Dictionary<string, elementArray>();
    public Dictionary<string, float> damageStats = new Dictionary<string, float>();
    public Dictionary<string, float> spellStats = new Dictionary<string, float>();
    public GameObject barsHolder;
    private float hpDefault = 100; //the only value every entity has
    private sliderHandling[] bars;
    private animation animationScript;
    [SerializeField] private Transform damagePopupPrefab;
    
    //initialises the entity
    public void Setup()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.Log("failed rb search");
        bars = GetComponentsInChildren<sliderHandling>(); //[health, fire, water, air, earth]
        animationScript = GetComponent<animation>();
        if(!this.entityStats.TryGetValue("hpMax", out float value)) this.entityStats["hpMax"] = hpDefault;
        this.entityStats["hp"] = this.entityStats["hpMax"];
    }
    void Start()
    {
        Setup();
    }

    //every entity can take damage (overrite this in spesific cases)
    public void Damage(elementArray damageTemp, Dictionary<string, float> damageStats = null)
    {
        float trueDamage = 0;
        elementArray damage = new elementArray(damageTemp.elements.ToArray()); //deep copy

        //reduce damage by armor
        if (this.entityElements.TryGetValue("armor", out elementArray EAvalue))
        {
            for(int i = 0; i < damage.elements.Length; i++)
            {
                //must be > 0 to prevent negative damage
                if(EAvalue.elements[i] > 0) damage.elements[i] *= 1 / EAvalue.elements[i];
            }
        }

        //calculate true damage
        foreach (float damageElement in damage.elements) trueDamage += damageElement;
        entityStats["hp"] -= trueDamage;

        //create a damage popup
        Transform damagePopup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
        damagePopup.GetComponent<damagePopup>().Setup(trueDamage);

        Debug.Log("took " + trueDamage + " damage");

        if (entityStats["hp"] <= 0) Die();
        //do special things here, not applied if dead
        bars[0].updateSlider(entityStats["hp"], entityStats["hpMax"]);
        //knockback, needs value, x and y directions
        if (damageStats.TryGetValue("knockback", out float value) && damageStats.TryGetValue("collisionDirectionX", out float dirX) && damageStats.TryGetValue("collisionDirectionY", out float dirY)) 
        {
            Vector2 dir = new Vector2(dirX, dirY);
            rb.AddForce(dir.normalized * value, ForceMode2D.Impulse);
        }
    }

    //called if hp gets below 0
    private void Die()
    {
        //if there is an enemy script present call its onDeath
        enemy Enemy = GetComponent<enemy>();
        if (Enemy != null) Enemy.onDeath();
        //delete
        if (animationScript == null) Destroy(gameObject);
        else StartCoroutine(animationScript.Die());
    }
}
