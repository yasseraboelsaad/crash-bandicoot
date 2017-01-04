using UnityEngine;
using System.Collections;

public class wumpa_fruit_script : MonoBehaviour {
	GameObject player;
	crash_script controlscript;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Crash");
		controlscript = player.GetComponent<crash_script>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider c){
		if (player.GetComponent <Collider> () == c) {
			controlscript.wumpacount++;
			Destroy (this.gameObject);
		}
	}
}
