using UnityEngine;
using System.Collections;

internal class Fader : MonoBehaviour
{
    static bool fadedInThisScene;
    static bool fadedOutThisScene;

    bool fadingIn;
    bool called;

    public delegate void ThingDelegate();

    ThingDelegate done;

    public static void Reset()
    {
        fadedInThisScene = false;
        fadedOutThisScene = false;
    }

    public static void fadeIn(ThingDelegate doneFunc)
    {
        if (!fadedInThisScene)
        {
            fadedInThisScene = true;
            GameObject obj = (GameObject)Instantiate(Resources.Load("MGJ/GUI/Fader"), Vector3.zero, Quaternion.identity);
            obj.GetComponentInChildren<Animator>().SetTrigger("FadeIn");
            obj.GetComponentInChildren<Fader>().fadingIn = true;
            obj.GetComponentInChildren<Fader>().done = doneFunc;
        }
    }

    public static void fadeOut(ThingDelegate doneFunc)
    {
        if (!fadedOutThisScene)
        {
            fadedOutThisScene = true;
            GameObject obj = (GameObject)Instantiate(Resources.Load("MGJ/GUI/Fader"), Vector3.zero, Quaternion.identity);
            obj.GetComponentInChildren<Fader>().fadingIn = false;
            obj.GetComponentInChildren<Animator>().SetTrigger("FadeOut");
            obj.GetComponentInChildren<Fader>().done = doneFunc;
        }
    }

    void Start()
    {
        if (fadingIn)
        {
            StartCoroutine(fadeMusic(.7f, 1f));
        }
        else
        {
            StartCoroutine(fadeMusic(0, .3f));
        }
    }

    void Update()
    {

    }

    public void fadeOutDone()
    {
        if (!fadingIn)
        {
            if (!called)
            {
                called = true;
                if (done != null)
                {
                    done();
                }
                Destroy(gameObject, .1f);
            }
        }
    }

    public void fadeInDone()
    {
        if (fadingIn)
        {
            if (!called)
            {
                called = true;
                if (done != null)
                {
                    done();
                }
                Destroy(gameObject, .1f);

            }
        }
    }

    IEnumerator fadeMusic(float end, float time)
    {
        float timer = 0;
        AudioSource music = Camera.main.GetComponent<AudioSource>();
        if (music != null)
        {
            float start;
            if (end == 0)
            {
                start = music.volume;
            }
            else
            {
                start = 0;
            }
            while (timer < time)
            {
                timer += Time.deltaTime;
                music.volume = Mathf.Lerp(start, end, timer / time);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
    


