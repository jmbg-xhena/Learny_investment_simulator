using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateGameEventListener : MonoBehaviour
{
    public StateGameEvent Event;
    public StateEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(State param)
    { Response.Invoke(param); }
}

[System.Serializable]
public class StateEvent : UnityEvent<State> {}