using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinningText : MonoBehaviour 
{
	public Text winText;
	// Use this for initialization
	void Awake () 
	{
		winText.text = "Player " +ChixScore.winningPlayer + " Wins";
	}
	
	// Update is called once per frame

}
