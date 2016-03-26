using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ChixWalk : MonoBehaviour
{


    private Rigidbody2D rb;
    public int playerIndex = 0;
    public float speed = 8f;
	private bool insidePlayer, insideChild; 
	private string collidedPlayer, collidedBaby, myPlayer, lastParent;
	public GameObject baby1, baby2, baby3, baby4, evilbaby1, evilbaby2, evilbaby3, evilbaby4, heart; 
	private GameObject baby, evilbaby; 
	private float playerX, playerY;
	private int score; 


    void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
		myPlayer = gameObject.name;
	}
	
	void Update () 
    {
		chixScore (playerIndex);
		rb.velocity = speed * MGJInputManager.GetPlayerMovement(playerIndex);
        if(MGJInputManager.GetPlayerButtonInput(playerIndex, MGJInputManager.Actions.Button1))
        {
			if 	(puffThread == null)
			{
				puffThread = StartCoroutine(Puff());
				makeBabies(insidePlayer, insideChild); 
				playerX = rb.transform.position.x;
				playerY = rb.transform.position.y;
				if (lastParent != collidedPlayer || collidedPlayer == "")
				{
					Instantiate(heart, new Vector3 (playerX, playerY, 0), Quaternion.identity);	
				}
				else if (lastParent == collidedPlayer)
				{
					GetComponent<AudioSource>().Play();
					print ("played");
				}
				if (insideChild)
				{
					Instantiate(heart, new Vector3 (playerX, playerY, 0), Quaternion.identity);	
					collidedPlayer = "";
				}

				lastParent = collidedPlayer;
			}
        }
        if(MGJInputManager.GetPlayerButtonDownInput(playerIndex, MGJInputManager.Actions.Button2))
		{
            Debug.Log("Button 2 pressed");
        }
	}

    private Coroutine puffThread;
    IEnumerator Puff()
    {
        float timer = 0f;
        float totalTime = 1f;
        while(timer < totalTime)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.one * (1f + 0.3f * Mathf.Sin(Mathf.PI * 2f * (timer / totalTime)));
            yield return null;
        }
        puffThread = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "a")
		{
			insidePlayer = true;
			collidedPlayer = other.gameObject.name;
			if (collidedPlayer == lastParent)
			{
				insidePlayer = false;
			}

		}
		if (other.tag == "b")
		{
			insideChild = true;
		}
    }
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "a")
		{
			insidePlayer = false;
		}
		if (other.tag == "b")
		{
			insideChild = false;
		}	
	}
	void makeBabies (bool PlayerIn, bool ChildIn)
	{

		if (ChildIn)
		{
			score -= 1;
			Invoke("EvilBaby", 1f);


		}
		else if (!ChildIn && PlayerIn)
		{
	
			score += 3; 
			Invoke("Baby", 1f);
		}
		 
	}
	void EvilBaby()
	{
		Instantiate(evilbaby, new Vector3 (playerX, playerY, 0), Quaternion.identity);
		//print("Evil");
	}
	void Baby()
	{
		float evenOdd;
		evenOdd =  Random.Range (1, 100);
		print ((int)evenOdd);

		if ((int)evenOdd%2 == 0)
		{
			PickABaby(collidedPlayer);	
			print("babyPicker Runs");
		}
		else 
		{
			MyBaby();
			print("My Baby");
		}

		Instantiate(baby, new Vector3 (playerX, playerY, 0), Quaternion.identity);

	}


	void chixScore (int i)
	{
		if (i == 0)
		{
			ChixScore.score1 = score; 
		}
		else if (i == 1)
		{
			ChixScore.score2 = score; 
		}
		else if (i == 2)
		{
			ChixScore.score3 = score; 
		}
		else if (i == 3)
		{
			ChixScore.score4 = score; 
		}
	}
	void PickABaby(string chixName)
	{
		if (chixName == "Player1")
		{
			baby = baby1;
		}
		else if (chixName == "Player2")
		{
			baby = baby2;
		}
		else if (chixName == "Player3")
		{
			baby = baby3;
		}
		else if (chixName == "Player4")
		{
			baby = baby4;
		}

	}
	void MyBaby()
	{
		if (playerIndex == 0)
		{
			baby = baby1;
			evilbaby = evilbaby1;
		}
		else if (playerIndex == 1)
		{
			baby = baby2;
			evilbaby = evilbaby2;
		}
		else if (playerIndex == 2)
		{
			baby = baby3;
			evilbaby = evilbaby3;
		}
		else if (playerIndex == 3)
		{
			baby = baby4;
			evilbaby = evilbaby4;
		}
	}
}
