using UnityEngine;
using System.Collections;

public class MalePlayer : MonoBehaviour {
	
	Rigidbody2D rigid;
	public float spd;
	Animator anim;
	
	// Use this for initialization
	void Start () {
		
		transform.position = new Vector2 (1f, 1f);
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		this.rigid.velocity = new Vector3 (Input.GetAxis ("Horizontal") , Input.GetAxis ("Vertical") , 0) * spd;
		
		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {

			//anim.SetBool("IDLE", false);
			anim.enabled = true;


		} else {

			anim.enabled = false;
//			anim.SetBool("IDLE", true);
//			anim.SetBool ("WalkFront", false);
//			anim.SetBool ("WalkBack", false);
//			anim.SetBool ("WalkLeft", false);
//			anim.SetBool ("WalkRight", false);
//
		}
		
		if (Input.GetAxisRaw ("Horizontal") > 0) {
			anim.SetBool ("WalkFront", false);
			anim.SetBool ("WalkBack", false);
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("WalkRight", true);
		} else if (Input.GetAxisRaw ("Horizontal") < 0) {
			
			anim.SetBool ("WalkFront", false);
			anim.SetBool ("WalkBack", false);
			anim.SetBool ("WalkLeft", true);
			anim.SetBool ("WalkRight", false);
			
		} else if (Input.GetAxisRaw ("Vertical") > 0) {
			
			anim.SetBool ("WalkFront", false);
			anim.SetBool ("WalkBack", true);
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("WalkRight", false);
			
		} else if (Input.GetAxisRaw ("Vertical") < 0) {
			
			anim.SetBool ("WalkFront", true);
			anim.SetBool ("WalkBack", false);
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("WalkRight", false);
		} 

		
	}
}
