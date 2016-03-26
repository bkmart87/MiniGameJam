using UnityEngine;
using System.Collections;

public class ChixAi : MonoBehaviour 
{
	private GameObject maxXMinY, minXMaxY; 
	private float maxX, minY, minX, maxY, margin;
	private Vector2 destination; 
	private float wandSpeed = 0, wandWait; 


	// Use this for initialization
	void Awake () 
	{
		maxXMinY = GameObject.Find("MaxXMinY");
		minXMaxY = GameObject.Find("MinXMaxY");
		maxX = maxXMinY.transform.position.x; 
		minY = maxXMinY.transform.position.y;
		minX = minXMaxY.transform.position.x; 
		maxY = minXMaxY.transform.position.y; 
		margin = 0.5f; 
		destination = WanderFunc();
		wandSpeed = 8;
		wandWait = 6f;


	}


	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float tempY, tempX; 
		tempX = this.transform.position.x;
		tempY = this.transform.position.y;
		wandWait -= Time.fixedDeltaTime; 
		if (wandWait < 0)
		{
			if (Mathf.Abs(tempX - destination.x) > margin && Mathf.Abs(tempY - destination.y) > margin)
			{
				float step = wandSpeed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, destination, step);
			}
			else 
				{
					destination = WanderFunc();
					wandWait = WandWaiter();	
				}
		}
		
	}


	Vector2 WanderFunc()
	{
		Vector2 James = new Vector2 (0, 0);
		float tempX, tempY;
		tempX = James.x + Random.Range (minX, maxX);
		tempY = James.y + Random.Range (minY, maxY);
		James = new Vector3 (tempX, tempY);
		return James;
	}
	float WandWaiter()
	{
		float f = Random.Range (0f, 3f);
		return f;
	}
}
