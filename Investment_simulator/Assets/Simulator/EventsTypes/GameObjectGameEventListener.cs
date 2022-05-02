using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameObjectGameEventListener : MonoBehaviour
{
    public GameObjectGameEvent Event;
    public GameObjectEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(GameObject param)
    { Response.Invoke(param); }
}

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> {}