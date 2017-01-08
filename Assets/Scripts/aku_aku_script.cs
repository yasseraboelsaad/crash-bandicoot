using UnityEngine;
using System.Collections;

public class aku_aku_script : MonoBehaviour {

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
		transform.Rotate (0,50 * Time.deltaTime,0);
	}

	void OnTriggerEnter(Collider c){
		if (player.GetComponent <Collider> () == c) {
			GameObject sound = GameObject.Find ("akuakuCollect");
			AudioSource audio = sound.GetComponent<AudioSource>();
			audio.Play ();
			if (Application.loadedLevelName.Equals ("Level 3")) {
				controlscript2.akuaku++;

			}else{
				controlscript.akuaku++;

			}
			Destroy (this.gameObject);
		}
	}
}
