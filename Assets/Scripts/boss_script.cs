using UnityEngine;
using System.Collections;

public class boss_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider c) {
		if (transform.tag == "entry") {
			GetComponent<Collider> ().isTrigger = false;
		}
	}
}
