using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Rewired;

public class MGJStartGame : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (MGJInputManager.AnyButtonPressed())
        {
            Fader.fadeOut(LoadFirstScene);
        }
    }

    void LoadFirstScene()
    {
        SceneManager.LoadScene(MGJManager.GetRandomUnplayedSceneIndex());
    }
}
