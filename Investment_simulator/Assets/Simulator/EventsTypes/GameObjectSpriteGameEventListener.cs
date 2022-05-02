using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameObjectSpriteGameEventListener : MonoBehaviour
{
    public GameObjectSpriteGameEvent Event;
    public GameObjectSpriteEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(GameObject param1, Sprite param2)
    { Response.Invoke(param1, param2); }
}

[System.Serializable]
public class GameObjectSpriteEvent : UnityEvent<GameObject, Sprite> {}