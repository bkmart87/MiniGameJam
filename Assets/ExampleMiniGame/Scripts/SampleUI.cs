using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SampleUI : MonoBehaviour {

    public Text[] playerScoreGUI;
    public Text timerGUI;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (timerGUI != null)
        {
            timerGUI.text = "" + Mathf.Ceil(MGJManager.GetTimeRemaining());
        }

        for (int i = 0; i < playerScoreGUI.Length; i++)
        {
            playerScoreGUI[i].text = "" + MGJManager.GetPlayerScore(i);
        }
    }
}
