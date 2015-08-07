using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	Rigidbody2D rigid;
	public float spd;
	Animator anim;

	// Use this for initialization
	void Start () {

		transform.position = new Vector2 (3f, 3.3f);
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {


		this.rigid.velocity = new Vector3 (Input.GetAxis ("Horizontal") , Input.GetAxis ("Vertical") , 0) * spd;

		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {

			anim.enabled = true;

		} else {

			anim.enabled = false;
		}

		if (Input.GetAxisRaw ("Horizontal") > 0) {

			transform.rotation =Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,-90), this.spd);
		} else if (Input.GetAxisRaw ("Horizontal") < 0) {

			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,90), this.spd);

		} else if (Input.GetAxisRaw ("Vertical") > 0) {

			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,0), this.spd);

		} else if (Input.GetAxisRaw ("Vertical") < 0) {

			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,180), this.spd);
		}
	
	}
}
