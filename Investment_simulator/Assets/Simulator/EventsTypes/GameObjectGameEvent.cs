using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewGameObjectGameEvent", menuName = "Events/New GameObject Game Event", order = 1)]
public class GameObjectGameEvent : ScriptableObject
{
	private List<GameObjectGameEventListener> listeners =
		new List<GameObjectGameEventListener>();

	public void Raise(GameObject param)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(param);
	}

	public void RegisterListener(GameObjectGameEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(GameObjectGameEventListener listener)
	{ listeners.Remove(listener); }
}