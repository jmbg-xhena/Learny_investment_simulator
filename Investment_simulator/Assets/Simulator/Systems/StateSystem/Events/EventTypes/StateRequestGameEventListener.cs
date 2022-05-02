using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateRequestGameEventListener : MonoBehaviour
{
    public StateRequestGameEvent Event;
    public StateRequestEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(StateRequest param)
    { Response.Invoke(param); }
}

[System.Serializable]
public class StateRequestEvent : UnityEvent<StateRequest> {}