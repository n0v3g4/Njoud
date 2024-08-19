using System;
[Serializable]
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