using UnityEngine;
using System.Collections;

public class hole_script : MonoBehaviour {

	GameObject player;
	crash_script controlscript;
	kart_script controlscript2;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Crash");
		controlscript = player.GetComponent<crash_script>();
		controlscript2 = player.GetComponent<kart_script>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider c){
		if (Application.loadedLevelName.Equals ("Level 3")) {
			if (player.GetComponent <Collider> () == c && !controlscript2.isProtected) {
				controlscript2.isFalling = true;
				controlscript2.hp = 0;
			}
		} else {
			if (player.GetComponent <Collider> () == c && !controlscript.isProtected) {
				controlscript.isFalling = true;
				controlscript.hp = 0;
			}
		}
	}
}
