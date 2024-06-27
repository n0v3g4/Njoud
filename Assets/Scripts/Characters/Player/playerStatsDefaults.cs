using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStatsDefaults : MonoBehaviour
{
    private Dictionary<string, float> entityStats;
    private Dictionary<string, float> spellStats;
    private Dictionary<string, elementArray> entityElements;
    // Start is called before the first frame update
    void Start()
    {
        entityStats = GetComponent<entity>().entityStats;
        entityStats["ms"] = 100;
        entityStats["team"] = 0;

        entityElements = GetComponent<entity>().entityElements;
        entityElements["damage"] = new elementArray(new float[4] { 10, 10, 10, 10 });
    }
}
