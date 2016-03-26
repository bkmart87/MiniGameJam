using UnityEngine;
using UnityEngine.UI;
using System.Collections;


internal class MGJLocalJamSettings : MonoBehaviour
{
    [Range(10f, 300f)]
    public float GameLength = 30f;

    void Start()
    {
        MGJManager.OverrideGlobalProperties(GameLength);
    }

    void Update()
    {
            
    }
}

