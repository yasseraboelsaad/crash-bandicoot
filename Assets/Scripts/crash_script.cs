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
	public bool isFalling;
	public int akuaku;
	bool isProtected;
	private double Timer = 0;

	// Use this for initialization
	void Start () {
		myAnim = GetComponent <Animator> ();
		wumpacount = 0;
		hp = 3;
		isFalling = false;
		akuaku = 0;
		isProtected=false;
	}

	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;
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

		//if isFalling into hole
		if(isFalling){
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), Time.deltaTime*2f);
			hp = 0;
		}

		//akuaku is 3
		if(akuaku==3){
			isProtected = true;
			akuaku = 2;
			Timer = 0;
		}
		if(isProtected && Timer>=30){
			isProtected = false;
		}
	}

	public void hit(){
		if(akuaku>0 && !isProtected){
			akuaku--;	
		}else if(akuaku==0 && !isProtected){
			hp--;
		}
	}
}
