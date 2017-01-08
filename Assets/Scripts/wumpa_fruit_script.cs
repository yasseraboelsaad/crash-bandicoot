using UnityEngine;
using System.Collections;

public class wumpa_fruit_script : MonoBehaviour {
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
			GameObject sound = GameObject.Find ("wumpaFruitCollect");
			AudioSource audio = sound.GetComponent<AudioSource>();
			audio.Play ();
			if(Application.loadedLevelName.Equals ("Level 3")){
				controlscript2.wumpacount++;
				if(controlscript2.wumpacount%100==0){
					controlscript2.hp++;
				}
			}else{
				controlscript.wumpacount++;
				if(controlscript.wumpacount%100==0){
					controlscript.hp++;
				}
			}

			Destroy (this.gameObject);
		}
	}
}
