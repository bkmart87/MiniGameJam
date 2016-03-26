using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


internal class MGJTransitionManager : MonoBehaviour
{
    public Text[] scoreGUI;
    public Text gamesLeftText;
    public float addScoreWaitTime;
    public GameObject confetti;
    public UnityEngine.UI.Text continueText;

    bool canContinue;

    void Start()
    {
        if (MGJManager.gamesLeft == 1)
        {
            gamesLeftText.text = "Only 1 Game Left!!!!!";
        }
        else {
            gamesLeftText.text = MGJManager.gamesLeft + " Games Left";
        }
        Fader.fadeIn(AddScoreSequence);
        for (int i = 0; i < scoreGUI.Length; i++)
            scoreGUI[i].text = "" + MGJManager.GetPlayerScore(i);
    }

    void AddScoreSequence()
    {
        MGJManager.ApplyPointQueues();
        StartCoroutine(AddScoreSequanceCoroutine());
    }

    void Update()
    {
        if (canContinue)
        {
            continueText.enabled = true;
            if (MGJInputManager.AnyButtonPressed())
            {
                Fader.fadeOut(MGJManager.EndTransition);
            }
        }
        else
        {
            continueText.enabled = false;
        }
    }


    IEnumerator AddScoreSequanceCoroutine()
    {
        yield return new WaitForSeconds(1);
        for (int k = 0; k < scoreGUI.Length; k++)
        {
            if (scoreGUI[k].text != "" + MGJManager.GetPlayerScore(k))
            {
                scoreGUI[k].text = "" + MGJManager.GetPlayerScore(k);
                Vector3 spawnPos = scoreGUI[k].transform.position;
                spawnPos.z -= 2;
                GameObject.Instantiate(confetti, spawnPos, Quaternion.identity);
                scoreGUI[k].GetComponent<AudioSource>().Play();
                scoreGUI[k].GetComponent<Animator>().SetTrigger("Scored");
                yield return new WaitForSeconds(addScoreWaitTime);
                scoreGUI[k].GetComponent<AudioSource>().Stop();
            }
        }
        canContinue = true;
    }
}

