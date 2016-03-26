using UnityEngine;
using System.Collections;

public class MGJBackgroundFlash : MonoBehaviour
{

    public int BPM;
    public Color[] flashColors;

    private float flashWaitTime;
    private float timer;
    private Camera cam;
    private int flashIndex;


    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        flashWaitTime = BPM / 360.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > flashWaitTime)
        {
            timer = 0;
            cam.backgroundColor = flashColors[flashIndex];
            flashIndex = flashIndex + 1;
            flashIndex = flashIndex % 4;
        }
    }
}

