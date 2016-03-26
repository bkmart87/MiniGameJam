using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChixScore : MonoBehaviour
{
	public static int score1, score2, score3, score4;        // The player's score.
	public static int winningPlayer = 0;
	private float chixTimer;


	public Text[] chixScoreGUI;
	public Text chixTimerGUI;


	void Awake ()
	{
		score1=  score2 = score3 = score4 = 0;
		chixTimer = 60;
	}


	void Update ()
	{
		
		chixTimer -= Time.deltaTime; 
		for (int i = 0; i < chixScoreGUI.Length; i++)
		{
			if (i == 0)
			{
				chixScoreGUI[i].text = "" +score1 ;
			}
			else if (i == 1)
			{
				chixScoreGUI[i].text = "" +score2 ;
			}
			else if (i == 2)
			{
				chixScoreGUI[i].text = "" +score3 ;
			}
			else if (i == 3)
			{
				chixScoreGUI[i].text = "" +score4 ;
			}
		}

		chixTimerGUI.text = "" +(int)chixTimer ;

		if(chixTimer <= 0.5f)
		{

			if (score1 > score2 && score1 > score3 && score1 > score4)
			{
				winningPlayer =1;
			}
			else if (score2 > score1 && score2 > score3 && score2 > score4)
			{
				winningPlayer =2;
			}

			else if (score3 > score1 && score3 > score2 && score3 > score4)
			{
				winningPlayer =3;
			}
			else 
			{
				winningPlayer =4;
	
			}
			Application.LoadLevel(2);
		}
	}
}

