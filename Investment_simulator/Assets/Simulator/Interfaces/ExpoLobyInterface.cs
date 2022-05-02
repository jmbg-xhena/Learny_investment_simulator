using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpoLobyInterface : MonoBehaviour
{
    [Header("Scriptable Events")]
    [SerializeField] private GameObjectGameEvent onDehighlightObjectRequest;

    [Header("View References")]
    [SerializeField] private GameObject officeBoss;
    [SerializeField] private GameObject officeBossView;

    public void PressBossOffice()
    {
        onDehighlightObjectRequest.Raise(officeBoss);
        officeBossView.SetActive(true);

        if (StateController.instance.state == State.ExpoFirstLoby)
            ExpoEntrancViewInterface.instance.ShowInitialAlert();
    }
}
