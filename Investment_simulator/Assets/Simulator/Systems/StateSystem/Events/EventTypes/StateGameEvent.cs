using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStateGameEvent", menuName = "Events/New State Game Event", order = 1)]
public class StateGameEvent : ScriptableObject
{
	private List<StateGameEventListener> listeners =
		new List<StateGameEventListener>();

	public void Raise(State param)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(param);
	}

	public void RegisterListener(StateGameEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(StateGameEventListener listener)
	{ listeners.Remove(listener); }
}