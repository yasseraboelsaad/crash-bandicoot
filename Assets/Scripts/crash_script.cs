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
	public bool isProtected;
	public double Timer = 0;
	public Text wumpaText;
	public Text crashText;
	public Text akuakuText;
	public bool isPaused;
	GameObject[] pauseObjects;
	public Button resume;
	public Button quit;
	public Button restart;
	public bool onCrate;
	public GameObject boss;
	static bool onlyOneAtk;
	Animator bossAnim;
	public static string lastScene;
	boss_script bossScript;


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
		bossScript = boss.GetComponent<boss_script> ();
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
				boss_script.isPaused = false;
			}else{
				isPaused = true;
				boss_script.isPaused = true;
			}
		}
		if (!isPaused) {
			Timer += Time.deltaTime;
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

			if (lastScene != "Level 4") {
				transform.Translate (Vector3.forward * v * WalkSpeed * Time.deltaTime);
			} 
			else {
				if (bossScript.isBossActive) {
					if (v > 0) {
						if (transform.eulerAngles.y != 450.0f) {
							Vector3 temp = transform.rotation.eulerAngles;
							temp.y = 450.0f;
							transform.rotation = Quaternion.Euler(temp);
						}
						transform.Translate (Vector3.forward * v * WalkSpeed * Time.deltaTime);
					} else {
						if (v < 0) {
							if (transform.eulerAngles.y != 630.0f) {
								Vector3 temp = transform.rotation.eulerAngles;
								temp.y = 630.0f;
								transform.rotation = Quaternion.Euler(temp);
							}
							transform.Translate (Vector3.forward * -v * WalkSpeed * Time.deltaTime);
						}
					}
				} 
				else {
					transform.Translate (Vector3.forward * v * WalkSpeed * Time.deltaTime);
				}
			}

			Rigidbody rb = GetComponent<Rigidbody> ();
			if (Input.GetButtonDown ("Jump") && (transform.position.y<1 || onCrate)) {
				GameObject sound = GameObject.Find ("jump");
				AudioSource audio = sound.GetComponent<AudioSource>();
				audio.Play ();
				myAnim.SetTrigger ("jump");
				rb.AddForce (new Vector3 (0, JumpForce, 0), ForceMode.Impulse);
			}
			float h = Input.GetAxis ("Horizontal");

			if (lastScene != "Level 4") {
				transform.Rotate (new Vector3 (0, 1, 0) * h * Time.deltaTime * TurnSpeed);
			} 
			else {
				if (bossScript.isBossActive) {
					if (h < 0) {
						if (transform.eulerAngles.y != 360.0f) {
							Vector3 temp = transform.rotation.eulerAngles;
							temp.y = 360.0f;
							transform.rotation = Quaternion.Euler(temp);
						}
						transform.Translate (Vector3.forward * -h * WalkSpeed * Time.deltaTime);
					} else {
						if (h > 0) {
							if (transform.eulerAngles.y != 540.0f) {
								Vector3 temp = transform.rotation.eulerAngles;
								temp.y = 540.0f;
								transform.rotation = Quaternion.Euler(temp);
							}
							transform.Translate (Vector3.forward * h * WalkSpeed * Time.deltaTime);
						}
					}
				} 
				else {
					transform.Rotate (new Vector3 (0, 1, 0) * h * Time.deltaTime * TurnSpeed);
				}
			}
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

			if (isProtected && Timer >= 10) {
				GameObject sound = GameObject.Find ("thirdAkuaku");
				AudioSource audio = sound.GetComponent<AudioSource>();
				audio.Stop ();
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

	void OnTriggerExit(Collider c) {
		if (c.gameObject.transform.tag == "entry") {
			c.isTrigger = false;
			bossScript.isBossActive = true;
			bossScript.bossCam.enabled = true;
			bossScript.mainCam.enabled = false;
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
