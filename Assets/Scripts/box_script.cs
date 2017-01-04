using UnityEngine;
using System.Collections;

public class box_script : MonoBehaviour {

	GameObject player;
	Animator anim;
	static int atakState = Animator.StringToHash("Spin");
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Crash");
		anim = player.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		if(player.transform.position.z < (this.transform.position+ new Vector3(0,0,5)).z && 
			player.transform.position.z > (this.transform.position- new Vector3(0,0,5)).z &&
			player.transform.position.x < (this.transform.position+ new Vector3(5,0,0)).x &&
			player.transform.position.x > (this.transform.position- new Vector3(5,0,0)).x &&
			currentBaseState.IsName("Spin")){
			Destroy (this.gameObject);
		}
	}
}
