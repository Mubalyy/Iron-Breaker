using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    void Start()
    {
        if (FindObjectOfType<Light>() == null)
        {
            GameObject dirLight = new GameObject("Directional Light");
            Light lightComp = dirLight.AddComponent<Light>();
            lightComp.type = LightType.Directional;
            lightComp.intensity = 1.5f;
            lightComp.color = Color.white;
            dirLight.transform.rotation = Quaternion.Euler(50, -30, 0);
        }
    }
}