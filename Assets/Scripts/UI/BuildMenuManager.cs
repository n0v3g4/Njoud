using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuManager : MonoBehaviour
{

    public Transform buildMenuHolder;

    // Update is called once per frame
    void Update()
    {
        if (Input.inputString != null)
        {
            if (Input.GetKeyDown(KeyCode.Q)) toggleBuildMenu();
        }
    }

    private void toggleBuildMenu()
    {
        if (buildMenuHolder.gameObject.activeSelf) buildMenuHolder.gameObject.SetActive(false);
        else buildMenuHolder.gameObject.SetActive(true);
    }
}
