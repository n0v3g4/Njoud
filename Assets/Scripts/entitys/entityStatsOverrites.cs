using UnityEngine;

public class entityStatsOverrites : MonoBehaviour
{
    private Entity entity;

    //used to serialize the values
    public string[] entityStatsK;
    public float[] entityStatsV;
    public string[] entityElementsK;
    public elementArray[] entityElementsV;
    public string[] damageStatsK;
    public float[] damageStatsV;

    public void OverrideStats()
    {
        entity = GetComponent<Entity>();
        for (int i = 0; i < entityStatsK.Length; i++) entity.entityStats[entityStatsK[i]] = entityStatsV[i];
        for (int i = 0; i < entityElementsK.Length; i++) entity.entityElements[entityElementsK[i]] = entityElementsV[i];
        for (int i = 0; i < damageStatsK.Length; i++) entity.damageStats[damageStatsK[i]] = damageStatsV[i];
    }
}
