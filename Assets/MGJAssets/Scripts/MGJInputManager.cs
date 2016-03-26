using UnityEngine;
using System.Collections;
using Rewired;

public class MGJInputManager : MonoBehaviour{

    #region singleton copy pasta, ignore this

    private static MGJInputManager _instance;

    private static object _lock = new object();

    private static MGJInputManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(MGJInputManager) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (MGJInputManager)FindObjectOfType(typeof(MGJInputManager));

                    if (FindObjectsOfType(typeof(MGJInputManager)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = (GameObject)GameObject.Instantiate(Resources.Load("MGJ/InputManager"), Vector3.zero, Quaternion.identity);
                        singleton.hideFlags = HideFlags.HideAndDontSave;
                        _instance = singleton.GetComponent<MGJInputManager>();
                        singleton.name = "(singleton) " + typeof(MGJInputManager).ToString();

                        DontDestroyOnLoad(singleton);


                    }
                    else {
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

    public enum Actions
    {
        Button1,
        Button2
    }

    /* ---------------------------------------------------
    A quick readme if your looking at this for some reason
    ------------------------------------------------------
    We're using Rewired to do the input automatically for us but you don't need to worry about all the knitty gritty. If that stuff makes you scared then just move on and use the functions below

    Otherwise we continue ----
    So Rewired uses strings to get your inputs. They are named Horizontal and Vertical for the left stick movements, and Button1 and Button2 for buttons
    Case sensistivity is important here. Player is the main thing you access these through, their abstracted in our functions but if you wanna do more stuff get a reference to the player 
    that you care doing input for and interface with rewired directly. 

    Really whats important:

    Input strings are

    - Horizontal
    - Vertical
    - Button1
    - Button2

    Hopefully what those do is pretty self explanatory
    */



    /// <summary>
    /// returns true or false for whether or not the button is pressed. Action1 is A, Action2 is B
    /// </summary>
    /// <param name="inPlayerNum"></param>
    /// <param name="inAction"></param>
    /// <returns></returns>
    public static bool GetPlayerButtonInput(int inPlayerNum, Actions inAction)
    {
        if (Instance) { } //Make sure instance is assigned
        return ReInput.players.GetPlayer(inPlayerNum).GetButton(inAction.ToString());
    }

    /// <summary>
    /// returns true or false for whether or not the button was pressed this frame. Action1 is A, Action2 is B
    /// </summary>
    /// <param name="inPlayerNum"></param>
    /// <param name="inAction"></param>
    /// <returns></returns>
    public static bool GetPlayerButtonDownInput(int inPlayerNum, Actions inAction)
    {
        if (Instance) { } //Make sure instance is assigned
        return ReInput.players.GetPlayer(inPlayerNum).GetButtonDown(inAction.ToString());
    }

    /// <summary>
    /// Returns the movement for the playerNum, left and right on X value and up and down on the Y value
    /// </summary>
    /// <param name="inPlayerNum"></param>
    /// <returns></returns>
    public static Vector2 GetPlayerMovement(int inPlayerNum)
    {
        if (Instance) { } //Make sure instance is assigned
        return ReInput.players.GetPlayer(inPlayerNum).GetAxis2D("Horizontal", "Vertical");
    }

    /// <summary>
    /// If you want to be able to do more advanced stuff with input like double press then you can store the Player variable and interface directly with what it can give you
    /// If your just using buttons and movement feel free to use GetPlayerButtonInput and GetPlayerMovement instead and not mess up strings
    /// </summary>
    /// <param name="inPlayerNum"></param>
    /// <returns></returns>
    public static Player GetPlayer(int inPlayerNum)
    {
        if (Instance) { } //Make sure instance is assigned
        return ReInput.players.GetPlayer(inPlayerNum);
    }

    /// <summary>
    /// rumbles the controller for the player, this is state based, to stop rumble you must call again with intensity set to 0
    /// Sorry thats just how controller rumble works :P
    /// </summary>
    /// <param name="inPlayerNum"></param>
    /// <param name="inIntensity"></param>
    public static void RumbleControllerForPlayer(int inPlayerNum, float inIntensity)
    {
        var c = ReInput.players.GetPlayer(inPlayerNum).controllers.Joysticks;
        foreach(Joystick j in c)
        {
            if (j.supportsVibration)
            {
                j.SetVibration(inIntensity, inIntensity);
            }
        }
    }

    /// <summary>
    /// Checks if any button on any play is being pressed
    /// </summary>
    /// <returns></returns>
    public static bool AnyButtonPressed()
    {
        if (Instance) { }
        var players = ReInput.players;
        for (int k = 0; k < players.playerCount; k++)
        {
            if (players.GetPlayer(k).GetAnyButton())
            {
                return true;
            }
        }
        return false;
    }
}
