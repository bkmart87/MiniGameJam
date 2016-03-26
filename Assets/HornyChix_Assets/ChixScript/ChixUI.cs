using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChixUI : MonoBehaviour 
{

    public Text[] chixScoreGUI;
    public Text chixTimerGUI;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (chixTimerGUI != null)
        {
            chixTimerGUI.text = "" + Mathf.Ceil(MGJManager.GetTimeRemaining());
        }

        for (int i = 0; i < chixScoreGUI.Length; i++)
        {
           chixScoreGUI[i].text = "" + MGJManager.GetPlayerScore(i);
        }
    }
}
