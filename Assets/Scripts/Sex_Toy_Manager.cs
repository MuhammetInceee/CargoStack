using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sex_Toy_Manager : MonoBehaviour
{
    public static Sex_Toy_Manager Instance;

    public bool isAdd = false;

    public Material boxMaterial;
    public Material coverMaterial;
    public Material steamMaterial;

   void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

    }
}
