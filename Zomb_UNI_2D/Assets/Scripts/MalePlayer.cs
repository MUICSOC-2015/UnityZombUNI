using UnityEngine;
using System.Collections;

public class MalePlayer : MonoBehaviour { //male player is always initialise since it is already in the scene, just need to create and position it

	Animator anim;
	BoardManager charSize = new BoardManager(); //call board manager for size

	public float moveTime = 0.1f;
	public LayerMask blockingLayer;
	public float spd;
	public bool isAnimated = false;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rigid;
	private float inverseMoveTime;


	// Use this for initialization
	void Start () {
		//reduce the size of the player 
		Vector3 theScale = transform.localScale;
 		transform.localScale = theScale;

		//add rigidBody and animation and boxCollider
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		boxCollider = GetComponent<BoxCollider2D> ();

		//use reciprocal for multiplication instead of dividing, easier on the machine
		inverseMoveTime = 1f / moveTime;
		
	}
	

	void Update () //call movement and stuff
	{
		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)(Input.GetAxisRaw ("Horizontal"));
		vertical = (int)(Input.GetAxisRaw ("Vertical"));

		if (horizontal != 0) {
			vertical = 0;
		}

		Move (horizontal , vertical );
	}

	protected bool Move(int xDir, int yDir)
	{
		Vector2 start = transform.position ;

		Vector2 end = start + new Vector2 (xDir * charSize.size, yDir *charSize.size);

		boxCollider.enabled = false;


		boxCollider.enabled = true;
		StartCoroutine (SmoothMovement (end));

		return true;


	}

	protected IEnumerator SmoothMovement (Vector3 end)
	{
		float sqrRemainingDistance = ((transform.position - end)  * charSize.size).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) 
		{
			Vector3 newPosition = Vector3.MoveTowards(rigid.position , end, inverseMoveTime * Time.deltaTime);

			rigid.MovePosition(newPosition);

			sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			yield return null;
		}
	}



}
