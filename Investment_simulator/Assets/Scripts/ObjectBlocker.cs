using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBlocker : MonoBehaviour {

    public Collider2D coll;
    public GameObject blackout;
    
	void Awake () {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
    }

}
