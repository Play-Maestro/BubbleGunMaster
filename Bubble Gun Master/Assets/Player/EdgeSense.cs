using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeSense : MonoBehaviour {

	private float groundTimer = 2;

	private bool groundSensed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		groundTimer -= Time.deltaTime;

		if (groundTimer <= 0) {
			groundSensed = false;
		} else {
			groundSensed = true;
		}

	}

	public bool HasGroundToWalk(){
		return groundSensed;
	}

	void OnTriggerStay2D(Collider2D collider){
		if (collider.tag != "Item") {
			groundTimer = Time.deltaTime * 3;
            
		}
	}
}
