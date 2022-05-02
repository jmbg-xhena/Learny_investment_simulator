using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulatorController : MonoBehaviour
{
    [Header("Scriptable Events")]
    [SerializeField] private StateRequestGameEvent onStateChangeRequest;

    [Header("Simulator References")]
    [SerializeField] private GameObject expoEntrance;

    [Header("Over References")]
    [SerializeField] private Sprite expoEntrance_over;

    void Start()
    {
        onStateChangeRequest.Raise(new StateRequest(State.ExpoEntranace, expoEntrance.transform.Find("Over_container").gameObject, expoEntrance_over));
    }
}
