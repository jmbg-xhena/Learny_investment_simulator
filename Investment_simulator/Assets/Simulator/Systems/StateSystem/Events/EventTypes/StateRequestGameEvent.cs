using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStateRequestGameEvent", menuName = "Events/New StateRequest Game Event", order = 1)]
public class StateRequestGameEvent : ScriptableObject
{
	private List<StateRequestGameEventListener> listeners =
		new List<StateRequestGameEventListener>();

	public void Raise(StateRequest param)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(param);
	}

	public void RegisterListener(StateRequestGameEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(StateRequestGameEventListener listener)
	{ listeners.Remove(listener); }
}