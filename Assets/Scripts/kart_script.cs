using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kart_script : MonoBehaviour {
	private int WalkSpeed = 30;
	public int TurnSpeed = 50;
	public int JumpForce = 10;
	Animator myAnim;
	bool isWalking=false;
	public int hp;
	public int wumpacount;
	public bool isFalling;
	public int akuaku;
	public bool isProtected;
	private double Timer = 0;
	public Text wumpaText;
	public Text crashText;
	public Text akuakuText;
	public bool isPaused;
	GameObject[] pauseObjects;
	public Button resume;
	public Button quit;
	public Button restart;
	public bool onCrate;
	static bool onlyOneAtk;
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
			transform.Translate (Vector3.forward * WalkSpeed * Time.deltaTime);
			Rigidbody rb = GetComponent<Rigidbody> ();
//			&& (transform.position.y<1 || onCrate)
			if (Input.GetButtonDown ("Jump") ) {
				GameObject sound = GameObject.Find ("jump");
				AudioSource audio = sound.GetComponent<AudioSource>();
				audio.Play ();
				rb.AddForce (new Vector3 (0, JumpForce, 0), ForceMode.Impulse);
			}
			float h = Input.GetAxis ("Horizontal");
			transform.Rotate (new Vector3 (0, 1, 0) * h * Time.deltaTime * TurnSpeed*-1);
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
		
}
