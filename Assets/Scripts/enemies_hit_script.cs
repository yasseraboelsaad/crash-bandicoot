using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemies_hit_script : MonoBehaviour {
	GameObject player;
	Animator anim;
	crash_script controlscript;
	kart_script kartscript;
	Animator myAnim;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Crash");
		anim = player.GetComponent<Animator>();
		myAnim = GetComponent <Animator> ();
		controlscript = player.GetComponent<crash_script>();
		kartscript = player.GetComponent<kart_script>();
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		if (player.transform.position.z < (this.transform.position + new Vector3 (0, 0, 10)).z &&
		   player.transform.position.z > (this.transform.position - new Vector3 (0, 0, 10)).z &&
		   player.transform.position.x < (this.transform.position + new Vector3 (10, 0, 0)).x &&
		   player.transform.position.x > (this.transform.position - new Vector3 (10, 0, 0)).x &&
			(currentBaseState.IsName ("Spin") || currentBaseState.IsName ("kart_spin"))) {
			GameObject sound = GameObject.Find ("spin");
			AudioSource audio = sound.GetComponent<AudioSource> ();
			audio.Play ();
			Destroy (this.gameObject);
//			controlscript.hp++;
		}
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.CompareTag ("Player")) {
			if (Application.loadedLevelName.Equals ("Level 3")) {
				kartscript.hit ();
			} else {
				controlscript.hit ();
			}
		}
		
	}
}
