using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyAI : AIBase
{
    [Header("SleepTime")]
    [SerializeField] private float sleepTimer;
    [SerializeField] private float awakeTimer;
    private bool isSleeping;

    [Header("State")]
    public State state;

    public enum State
    {
       Awake,
       Sleep,
    }

    private void SetState(State newState)
    {
        state = newState;

        switch (state)
        {
            case State.Awake:
                StartCoroutine(OnAwake());
                break;
            case State.Sleep:
                StartCoroutine(OnSleep());
                break;
        }
    }

    protected override void Start()
    {
        base.Start();
        isSleeping = true;
        SetState(State.Awake);
    }

    protected override void Update()
    {
        base.Update();     
        
    }

    IEnumerator OnSleep()
    {
        while(state == State.Sleep)
        {
            SoundManager.instance.PlayLooped3DSound("hulu",transform.position, true, sleepTimer);
            anim.SetBool("isSleeping", true);
            if (fieldOfView != null)
            {
                fieldOfView.gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(sleepTimer);

            isSleeping = false;
            anim.SetBool("isSleeping", false);
            SetState(State.Awake);
        }
    }

    IEnumerator OnAwake()
    {
        while (state == State.Awake)
        {
            anim.SetBool("isIdle", true);
            if (fieldOfView != null)
            {
                fieldOfView.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(awakeTimer);

            isSleeping = true;
            anim.SetBool("isIdle", false);
            SetState(State.Sleep);
        }
    }
}
