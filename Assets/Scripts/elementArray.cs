using System;

public class elementArray
{
    //use this to make sure all element arrays have exactly n elements
    public float[] elements = new float[4]; //[fire, water, air, earth]
    public elementArray(float[] elements)
    {
        this.elements = elements;
    }
    public elementArray copy()
    {
        float[] copiedElements = new float[elements.Length];
        Array.Copy(elements, copiedElements, elements.Length);

        return new elementArray(copiedElements);
    }

    public elementArray negate()
    {
        float[] negatedElements = new float[elements.Length];
        for(int i = 0; i < elements.Length; i++) negatedElements[i] = -elements[i];

        return new elementArray(negatedElements);
    }
}
/* entityStats:
 * hp
 * hpMax
 * team
 * ms
 * 
 */

/* entityElements:
 * damage
 * armor
 * shield
 * 
 */

/* damageStats:
 * knockback
 * collisionDirectionX
 * collisionDirectionY
 * 
 * damageScaling //these are used for buffing, values 1/0
 * shield
 * ms
 * armor
 * 
 */

/* spellStats:
 * wandCapacity
 * fireSpellMaxRecursion
 * fireSpellCurrentRecursion
 * waterSpellMaxRecursion
 * waterSpellCurrentRecursion
 * spellSpeed
 * spellDuration
 * spellCooldown
 * prBuRatio
 * buffDuration
 */

/* list of elements and corresponding number
 * 1: fire
 * 2: water
 * 3: air
 * 4: earth
 */