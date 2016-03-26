using UnityEngine;
using System.Collections;

public class ExampleGoal : MonoBehaviour 
{
    void OnCollisionEnter2D(Collision2D col)
    {
        MGJManager.EndMinigame();
    }
}
