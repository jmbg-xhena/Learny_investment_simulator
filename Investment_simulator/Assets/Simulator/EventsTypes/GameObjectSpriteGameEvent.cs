using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewGameObjectSpriteGameEvent", menuName = "Events/New GameObject Sprite Game Event", order = 1)]
public class GameObjectSpriteGameEvent : ScriptableObject
{
	private List<GameObjectSpriteGameEventListener> listeners =
		new List<GameObjectSpriteGameEventListener>();

	public void Raise(GameObject param1, Sprite param2)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(param1, param2);
	}

	public void RegisterListener(GameObjectSpriteGameEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(GameObjectSpriteGameEventListener listener)
	{ listeners.Remove(listener); }
}