using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkingPlayerExample : MonoBehaviour
{


    private Rigidbody2D rigidbody;
    public int playerIndex = 0;
    public float speed = 8f;

    void Start () 
    {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
    {
        rigidbody.velocity = speed * MGJInputManager.GetPlayerMovement(playerIndex);
        if(MGJInputManager.GetPlayerButtonInput(playerIndex, MGJInputManager.Actions.Button1))
        {
            if (puffThread == null)
                puffThread = StartCoroutine(Puff());
        }
        if(MGJInputManager.GetPlayerButtonDownInput(playerIndex, MGJInputManager.Actions.Button2)){
            Debug.Log("Button 2 pressed");
        }
	}

    private Coroutine puffThread;
    IEnumerator Puff()
    {
        float timer = 0f;
        float totalTime = 0.4f;
        while(timer < totalTime)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.one * (1f + 0.3f * Mathf.Sin(Mathf.PI * 2f * (timer / totalTime)));
            yield return null;
        }
        puffThread = null;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<ExampleGoal>())
        {
            MGJManager.GivePlayerAPoint(playerIndex);
        }
    }
}
