using UnityEngine;
using System.Collections;

public class AnyKey : MonoBehaviour 
{
	float loadLevelTimer = 2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		loadLevelTimer -= Time.deltaTime;
		if (loadLevelTimer < 0)
		{
				if (MGJInputManager.AnyButtonPressed())
				{
					Application.LoadLevel(1);
				}
		}
	}
}
