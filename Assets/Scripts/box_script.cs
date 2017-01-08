using UnityEngine;
using System.Collections;

public class box_script : MonoBehaviour {

	GameObject player;
	Animator anim;
	crash_script controlscript;
	kart_script controlscript2;
	public GameObject aku_aku;
	GameObject AkuAkuMask;
	public GameObject life;
	GameObject LifeBox;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Crash");
		anim = player.GetComponent<Animator>();
		controlscript = player.GetComponent<crash_script>();
		controlscript2 = player.GetComponent<kart_script>();
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		if(Application.loadedLevelName.Equals ("Level 3")){
			if(player.transform.position.z < (this.transform.position+ new Vector3(0,0,10)).z && 
				player.transform.position.z > (this.transform.position- new Vector3(0,0,10)).z &&
				player.transform.position.x < (this.transform.position+ new Vector3(10,0,0)).x &&
				player.transform.position.x > (this.transform.position- new Vector3(10,0,0)).x &&
				currentBaseState.IsName("kart_spin")){
				GameObject sound = GameObject.Find ("crateBreak");
				AudioSource audio = sound.GetComponent<AudioSource>();
				audio.Play ();
				int rand = Random.Range(1, 4);
				if(rand == 1){
					controlscript2.wumpacount=controlscript2.wumpacount+10;
				}else if(rand==2){
					AkuAkuMask = Instantiate (aku_aku, this.transform.position, Quaternion.identity) as GameObject;
				}else if(rand == 3){
					LifeBox = Instantiate (life, this.transform.position, Quaternion.identity) as GameObject;
				}
				Destroy (this.gameObject);
			}
		}else{
			if(player.transform.position.z < (this.transform.position+ new Vector3(0,0,5)).z && 
				player.transform.position.z > (this.transform.position- new Vector3(0,0,5)).z &&
				player.transform.position.x < (this.transform.position+ new Vector3(5,0,0)).x &&
				player.transform.position.x > (this.transform.position- new Vector3(5,0,0)).x &&
				currentBaseState.IsName("Spin")){
				GameObject sound = GameObject.Find ("crateBreak");
				AudioSource audio = sound.GetComponent<AudioSource>();
				audio.Play ();
				int rand = Random.Range(1, 4);
				if(rand == 1){
					controlscript.wumpacount=controlscript.wumpacount+10;
				}else if(rand==2){
					AkuAkuMask = Instantiate (aku_aku, this.transform.position, Quaternion.identity) as GameObject;
				}else if(rand == 3){
					LifeBox = Instantiate (life, this.transform.position, Quaternion.identity) as GameObject;
				}
				Destroy (this.gameObject);
			}
		}


	}
}
