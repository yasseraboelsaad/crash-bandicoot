using UnityEngine;
using System.Collections;


public class crash_script : MonoBehaviour {
	public int WalkSpeed = 20;
	public int TurnSpeed = 50;
	public int JumpForce = 10;
	Animator myAnim;
	bool isWalking=false;
	public int hp;
	public int wumpacount;

	// Use this for initialization
	void Start () {
		myAnim = GetComponent <Animator> ();
		wumpacount = 0;
		hp = 3;
	}

	// Update is called once per frame
	void Update () {
		
		//Controls
		float v = Input.GetAxis ("Vertical");
		if(v!=0 && !isWalking){
			myAnim.SetBool ("isWalking",true);
			isWalking = true;
		}
		if(v==0){
			myAnim.SetBool ("isWalking",false);
			isWalking = false;
		}
		transform.Translate (Vector3.forward * v * WalkSpeed * Time.deltaTime);
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (Input.GetButtonDown ("Jump")) {
			myAnim.SetTrigger ("jump");
			rb.AddForce (new Vector3 (0, JumpForce, 0), ForceMode.Impulse);
		}
		float h = Input.GetAxis ("Horizontal");
		transform.Rotate (new Vector3 (0, 1, 0) * h*Time.deltaTime*TurnSpeed);
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			myAnim.SetTrigger ("spin");
		}

		//Wumpa Count
		if(wumpacount==100){
			hp++;
			wumpacount = 0;
		}
	}
}
