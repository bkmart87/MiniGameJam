using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

internal class MGJGameOverManager : MonoBehaviour {

    public Text mainText;
    public float leadupTime;
    public AudioClip yay;

    void Start() {
        StartCoroutine(waitAndReveal());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator waitAndReveal()
    {
        yield return new WaitForSeconds(leadupTime);
        List<int> winners = MGJManager.GetWinningPlayers();
        string winString = "Player " + (winners[0] + 1);
        for (int k = 1; k < winners.Count; k++)
        {
            winString += " and Player " + (winners[k] + 1);
        }
        mainText.text = winString;
        GetComponent<AudioSource>().Stop();
        AudioSource.PlayClipAtPoint(yay, Camera.main.transform.position);
    }
}

