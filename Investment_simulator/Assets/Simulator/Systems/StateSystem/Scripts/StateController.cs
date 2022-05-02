using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    None,
    ExpoEntranace,
    ExpoFirstLoby
}

public class StateController : MonoBehaviour
{
    public static StateController instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("Scriptable Events")]
    [SerializeField] private GameObjectSpriteGameEvent onHighlightObjectRequest;

    [Header("Public Members")]
    public State state;

    public void ChangeState(StateRequest request)
    {
        this.state = request.state;
        onHighlightObjectRequest.Raise(request.target, request.over);
    }
}

public class StateRequest
{
    public State state;
    public GameObject target;
    public Sprite over;

    public StateRequest(State state, GameObject target, Sprite over)
    {
        this.state = state;
        this.target = target;
        this.over = over;
    }
}