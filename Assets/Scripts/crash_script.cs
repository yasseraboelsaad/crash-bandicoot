using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	public Text wumpaText;
	public Text crashText;
	public Text akuakuText;
	bool isPaused;
	GameObject[] pauseObjects;
	public Button resume;
	public Button quit;
	public Button restart;
	public bool onCrate;
	public GameObject boss;
	static bool onlyOneAtk;
	Animator bossAnim;
	public static string lastScene;

	// Use this for initialization
	void Start () {
		myAnim = GetComponent <Animator> ();
		wumpacount = 0;
		hp = 3;
		isFalling = false;
		akuaku = 0;
		isProtected=false;
		isPaused = false;
		pauseObjects = GameObject.FindGameObjectsWithTag("PauseMenu");
		Button btn = resume.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		Button btn2 = quit.GetComponent<Button>();
		btn2.onClick.AddListener(TaskOnClick2);
		Button btn3 = restart.GetComponent<Button>();
		btn3.onClick.AddListener(TaskOnClick3);
		onCrate = false;
	}

	public void init() {
		wumpacount = 0;
		hp = 3;
		isFalling = false;
		akuaku = 0;
		isProtected=false;
		isPaused = false;
		onCrate = false;
	}

	// Update is called once per frame
	void Update () {
		lastScene = SceneManager.GetActiveScene ().name;
		Timer += Time.deltaTime;
		//Dead
		if(hp <=0){
			GameObject sound = GameObject.Find ("CrashDie");
			AudioSource audio = sound.GetComponent<AudioSource>();
			audio.Play ();
			Application.LoadLevel("Death Screen");
		}

		//Pausing
		if (Input.GetKeyDown ("p") || Input.GetKey (KeyCode.Escape)) {
			if(isPaused){
				isPaused = false;
			}else{
				isPaused = true;
			}
		}
		if (!isPaused) {
			//Hide Pause Menu
			foreach(GameObject g in pauseObjects){
				g.SetActive(false);
			}

			//Controls
			float v = Input.GetAxis ("Vertical");
			if (v != 0 && !isWalking) {
				myAnim.SetBool ("isWalking", true);
				isWalking = true;
			}
			if (v == 0) {
				myAnim.SetBool ("isWalking", false);
				isWalking = false;
			}
			transform.Translate (Vector3.forward * v * WalkSpeed * Time.deltaTime);
			Rigidbody rb = GetComponent<Rigidbody> ();
			if (Input.GetButtonDown ("Jump") && (transform.position.y<1 || onCrate)) {
				GameObject sound = GameObject.Find ("jump");
				AudioSource audio = sound.GetComponent<AudioSource>();
				audio.Play ();
				myAnim.SetTrigger ("jump");
				rb.AddForce (new Vector3 (0, JumpForce, 0), ForceMode.Impulse);
			}
			float h = Input.GetAxis ("Horizontal");
			transform.Rotate (new Vector3 (0, 1, 0) * h * Time.deltaTime * TurnSpeed);
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				GameObject sound = GameObject.Find ("spin");
				AudioSource audio = sound.GetComponent<AudioSource>();
				audio.Play ();
				myAnim.SetTrigger ("spin");
			}

			//if isFalling into hole
			if (isFalling) {
				transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (0, 0, 0), Time.deltaTime * 2f);
				hp = 0;
			}

			//akuaku is 3
			if (akuaku == 3) {
				GameObject sound = GameObject.Find ("thirdAkuaku");
				AudioSource audio = sound.GetComponent<AudioSource>();
				audio.Play ();
				isProtected = true;
				akuaku = 2;
				Timer = 0;
			}
			if (isProtected && Timer >= 30) {
				isProtected = false;
			}

			//GUI components
			wumpaText.text = wumpacount.ToString ();
			crashText.text = hp.ToString ();
			akuakuText.text = akuaku.ToString ();
		}else{
			foreach(GameObject g in pauseObjects){
				g.SetActive(true);
			}
		}
	}

	void OnTriggerEnter(Collider c) {
		if (boss != null && c.gameObject.tag == "Boss") {
			onlyOneAtk = true;
		}
	}

	void OnTriggerStay(Collider c) {
		if (boss != null && c.gameObject.tag == "Boss") {
			bossAnim = boss.GetComponentInChildren<Animator> ();
			int normalAtkHash = Animator.StringToHash ("Crunch@normalAtk");
			int heavyAtkHash = Animator.StringToHash ("Crunch@heavyAtk");
			if (onlyOneAtk && (bossAnim.GetCurrentAnimatorStateInfo (0).shortNameHash == normalAtkHash
				|| bossAnim.GetCurrentAnimatorStateInfo (0).shortNameHash == heavyAtkHash)) {
				GameObject sound = GameObject.Find ("CrashHit");
				AudioSource audio = sound.GetComponent<AudioSource>();
				audio.Play ();
				int idleHash = Animator.StringToHash ("Crunch@idle");
				StartCoroutine(hitAfterAnimationFinishes (idleHash));
				onlyOneAtk = false;
			}
			if (!(bossAnim.GetCurrentAnimatorStateInfo (0).shortNameHash == normalAtkHash
				|| bossAnim.GetCurrentAnimatorStateInfo (0).shortNameHash == heavyAtkHash)) {
				onlyOneAtk = true;
			}
		}
	}

	public void hit(){
		GameObject sound = GameObject.Find ("CrashHit");
		AudioSource audio = sound.GetComponent<AudioSource>();
		audio.Play ();
		if(akuaku>0 && !isProtected){
			akuaku--;	
		}else if(akuaku==0 && !isProtected){
			hp--;
		}
	}

	public void hitWithoutSound(){
		if(akuaku>0 && !isProtected){
			akuaku--;	
		}else if(akuaku==0 && !isProtected){
			hp--;
		}
	}

	void TaskOnClick(){
		isPaused = false;
	}

	void TaskOnClick2(){
		Application.Quit();
	}

	void TaskOnClick3(){
		Application.LoadLevel(Application.loadedLevel);
	}

	IEnumerator hitAfterAnimationFinishes(int idleHash) {
		yield return new WaitUntil (() => bossAnim.GetCurrentAnimatorStateInfo (0).shortNameHash == idleHash);
		hitWithoutSound ();
	}
}
