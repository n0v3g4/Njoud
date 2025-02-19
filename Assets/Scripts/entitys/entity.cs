using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.EventSystems.EventTrigger;

public class Entity : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] private Transform damagePopupPrefab;
    public GameObject barsHolder;
    private sliderHandling[] bars;
    private animation animationScript;

    //data about an entity
    public Dictionary<string, float> entityStats = new Dictionary<string, float>();
    public Dictionary<string, float> damageStats = new Dictionary<string, float>();
    public Dictionary<string, elementArray> entityElements = new Dictionary<string, elementArray>();

    //initialises the entity
    public void Awake()
    {
        defaultValues();
        GetComponent<entityStatsOverrites>().OverrideStats();
        rb = GetComponent<Rigidbody2D>();
        bars = GetComponentsInChildren<sliderHandling>(); //[health, fire, water, air, earth] old, was intendet for elemental shields
        animationScript = GetComponent<animation>(); //is not needed in all entities
        entityStats["hp"] = entityStats["hpMax"];
    }

    //set default values to prevent excessive tryGetValue checks
    private void defaultValues()
    {
        //stats that are commented out are not used by all entitys by default
        entityStats["hp"]        = 0;
        entityStats["hpMax"]     = 1;
        entityStats["team"]      = -1;
        entityStats["animation"] = 1; //which animation script to use
        //entityStats["as"]        = 1; //attack speed
        //entityStats["ms"]        = 10; //movement speed
        //entityStats["range"] = 5; //target detection range

        entityElements["damage"] = new elementArray(new float[] { 1, 1, 1, 1 });
        entityElements["armor"]  = new elementArray(new float[] { 0, 0, 0, 0 });
        entityElements["shield"] = new elementArray(new float[] { 0, 0, 0, 0 });

        //damageStats["knockback"]           = 0;
        //damageStats["collisionDirectionX"] = 0;
        //damageStats["collisionDirectionY"] = 0;

        //damageStats["damageScaling"] = 1;
    }

    //every entity can take damage (overrite this in spesific cases)
    public void Damage(elementArray _damage, Dictionary<string, float> recievedDamageStats)
    {
        float trueDamage = 0;
        elementArray damage = new elementArray(_damage.elements.ToArray()); //deep copy

        //reduce damage by armor
        for (int i = 0; i < damage.elements.Length; i++)
        {
            //must be > 0 to prevent negative damage; also scaling the damage
            if(entityElements["armor"].elements[i] > 0) damage.elements[i] *= 1 / entityElements["armor"].elements[i];
            damage.elements[i] = Mathf.Max(0, damage.elements[i] - entityElements["shield"].elements[i]);
        }

        //calculate true damage
        foreach (float damageElement in damage.elements) trueDamage += damageElement;
        trueDamage *= recievedDamageStats["damageScaling"];
        entityStats["hp"] -= trueDamage;

        //create a damage popup
        Transform damagePopup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
        damagePopup.GetComponent<damagePopup>().Setup(trueDamage);

        if (entityStats["hp"] <= 0)
        {
            Die();
            return;
        }

        //add special effects here, not applied if dead
        bars[0].updateSlider(entityStats["hp"], entityStats["hpMax"]);
        //knockback, needs value, x and y directions
        if (recievedDamageStats["knockback"] != 0) 
        {
            Vector2 dir = new Vector2(recievedDamageStats["collisionDirectionX"], recievedDamageStats["collisionDirectionY"]);
            rb.AddForce(dir.normalized * recievedDamageStats["knockback"], ForceMode2D.Impulse);
        }
    }

    //called if hp gets below 0
    public void Die()
    {
        //apply spesifics
        deathSpesifics();

        //disable movement and colissions and bars
        Destroy(rb);
        foreach (Collider2D col in rb.GetComponents<Collider2D>()) Destroy(col);
        barsHolder.SetActive(false);

        //play animation and destroy
        if (animationScript == null) Destroy(gameObject);
        else StartCoroutine(animationScript.Die());
    }
    //override on spesific entities
    public virtual void deathSpesifics()
    {

    }
}
