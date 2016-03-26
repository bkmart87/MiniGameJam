using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class MGJManager : MonoBehaviour
{
    #region singleton copy pasta, ignore this

    private static MGJManager _instance;

    private static object _lock = new object();

    private static MGJManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(MGJManager) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (MGJManager)FindObjectOfType(typeof(MGJManager));

                    if (FindObjectsOfType(typeof(MGJManager)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = (GameObject)GameObject.Instantiate(Resources.Load("GameManager"), Vector3.zero, Quaternion.identity);
                        _instance = singleton.GetComponent<MGJManager>();
                        singleton.name = "(singleton) " + typeof(MGJManager).ToString();

                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(MGJManager) +
                            " is needed in the scene, so '" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                            _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
    #endregion

    public delegate void NoArgsDelegate();

    private static NoArgsDelegate gameEndedCallback;

    internal static int gamesLeft;

    private static int minSceneIndex;
    private static int maxSceneIndex;
    private static readonly int lastNonMinigameSceneIndex = 2;

    private static int nextMinigameIndex = 1;
    private static List<int> unplayedGameIndecies = new List<int>();

    private const int numPlayers = 4;
    private static int[] playerScores = new int[numPlayers];
    private const float defaultEndOfRoundTime = 60f;

    private static bool transitionSceneLock = false;
    private static bool propetyOverrideLock = false;
    private static bool gameIndeciesInitialized = false;


    //ChainJamManager Global Properties:
    private static float roundTimer = 99f;
    private static float endOfRoundTime = defaultEndOfRoundTime;
    private static int[] playerQueuedForPoints = new int[numPlayers];

    #region Scene Setup
    internal static void ResetScene()
    {
        //ChainJamManager Global Properties:
        if (!propetyOverrideLock)
        {
            endOfRoundTime = defaultEndOfRoundTime;
            roundTimer = endOfRoundTime;
        }
        if (SceneManager.GetActiveScene().name != "Transition")
            playerQueuedForPoints = new int[] { 0, 0, 0, 0 };

        propetyOverrideLock = false;
        transitionSceneLock = false;

        //Other Unity Game-wide settings:
        Time.timeScale = 1f;
        Physics.bounceThreshold = 2f;
        Physics.gravity = new Vector3(0f, -9.81f, 0f);
        Physics.sleepThreshold = 0.005f;
        Physics.defaultContactOffset = 0.01f;
        Physics.queriesHitTriggers = true;
        Physics2D.gravity = new Vector2(0.0f, -9.81f);
    }

    internal static void OverrideGlobalProperties(float _endOfRoundTimeIn)
    {
        if (!propetyOverrideLock)
        {
            endOfRoundTime = _endOfRoundTimeIn;
            roundTimer = endOfRoundTime;
            propetyOverrideLock = true;
        }
    }
    #endregion
    public static void EndMinigame()
    {
        if (!transitionSceneLock)
        {
            for(int k=0; k < numPlayers; k++)
            {
                MGJInputManager.RumbleControllerForPlayer(k, 0);
            }
            transitionSceneLock = true;
            if (gameEndedCallback != null)
            {
                gameEndedCallback();
                gameEndedCallback = null;
            }
            Fader.fadeOut(LoadNextMinigame);
        }
    }

    public static void SetEndMinigameCallback(NoArgsDelegate inFunc)
    {
        if (!transitionSceneLock) {
            gameEndedCallback = inFunc;
        }
    }
    internal static void ApplyPointQueues()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            if (playerQueuedForPoints.Length > i && playerQueuedForPoints[i] == 1)
                playerScores[i]++;

        }
        playerQueuedForPoints = new int[] { 0, 0, 0, 0 };
    }


    private static void LoadNextMinigame()
    {

        nextMinigameIndex = GetRandomUnplayedSceneIndex();
        if (nextMinigameIndex < 0)
        {
            LoadFinalScene();
        }
        else {
            SceneManager.LoadScene("Transition");
        }
    }

    private static void LoadFinalScene()
    {
        ApplyPointQueues();
        SceneManager.LoadScene("FinalScene");
    }

    internal static int GetRandomUnplayedSceneIndex()
    {
        if (unplayedGameIndecies.Count == 0)
        {
            return -1;
        }
        gamesLeft--;
        int randIndex = Random.Range(0, unplayedGameIndecies.Count);
        int result = unplayedGameIndecies[randIndex];
        unplayedGameIndecies.RemoveAt(randIndex);
        return result;
    }

    internal static void EndTransition()
    {
        SceneManager.LoadScene(nextMinigameIndex);
    }

    #region Game State Info Hooks:
    public static void GivePlayerAPoint(int playerIndex)
    {
        for (int i = 0; i < numPlayers; i++)
        {
			if (i == playerIndex) {
				Debug.Log("Give p a point: "+i);
                playerQueuedForPoints[i] = 1;
			}
        }
    }

    public static void SetAllPlayerPoints(int[] playerPoints)
    {
        for (int i = 0; i < numPlayers; i++)
        {
            if (playerPoints.Length > i && playerPoints[i] == 1)
                playerQueuedForPoints[i] = 1;
            if (playerPoints.Length > i && playerPoints[i] == 0)
                playerQueuedForPoints[i] = 0;
        }
    }
    public static int GetPlayerScore(int playerIndexIn)
    {
        if (playerIndexIn < numPlayers && playerIndexIn >= 0)
        {
            return playerScores[playerIndexIn];
        }
        else
        {
            Debug.LogError("Invalid Player Index Requested!");
            return 0;
        }
    }

    public static List<int> GetWinningPlayers()
    {
        List<int> result = new List<int>();
        result.Add(0);
        int winningPlayerScore = playerScores[0];
        for (int k = 1; k < numPlayers; k++)
        {
            if (winningPlayerScore == playerScores[k])
            {
                result.Add(k);
            }
            if (playerScores[k] > winningPlayerScore)
            {
                result.Clear();
                result.Add(k);
                winningPlayerScore = playerScores[k];
            }
        }
        return result;
    }

    public static float GetTimeRemaining() { return roundTimer; }
    #endregion

    internal static void initializeSceneIndecies()
    {
        if (!gameIndeciesInitialized)
        {
            gameIndeciesInitialized = true;
            minSceneIndex = lastNonMinigameSceneIndex + 1;
            maxSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
            for (int k = minSceneIndex; k <= maxSceneIndex; k++)
            {
                unplayedGameIndecies.Add(k);
            }
            gamesLeft = maxSceneIndex - minSceneIndex + 2;
            //Fader.fadeIn(null);
            ResetScene();
        }
    }

    void Awake()
    {
        initializeSceneIndecies();
    }



    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= minSceneIndex && SceneManager.GetActiveScene().buildIndex <= maxSceneIndex)
        {
//            roundTimer -= Time.deltaTime;
            if (roundTimer <= 0f)
            {
//                EndMinigame();
            }
        }
    }

}
