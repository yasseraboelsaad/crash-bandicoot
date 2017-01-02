using UnityEngine;
using System.Collections;

public class bounce_crate_script : MonoBehaviour {

	GameObject player;
	Animator anim;
	crash_script controlscript;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Crash");
		anim = player.GetComponent<Animator>();
		controlscript = player.GetComponent<crash_script>();
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		if (player.transform.position.z < (this.transform.position + new Vector3 (0, 0, 5)).z &&
		   player.transform.position.z > (this.transform.position - new Vector3 (0, 0, 5)).z &&
		   player.transform.position.x < (this.transform.position + new Vector3 (5, 0, 0)).x &&
		   player.transform.position.x > (this.transform.position - new Vector3 (5, 0, 0)).x &&
			player.transform.position.y > (this.transform.position).y) {
			controlscript.onCrate = true;
			controlscript.JumpForce = 20;
		}else{
			controlscript.onCrate = false;
			controlscript.JumpForce = 10;
		}
	}
}
