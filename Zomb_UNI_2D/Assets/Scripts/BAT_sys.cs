using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class BAT_sys : MonoBehaviour {
	PStats stat;
	Monsters enem_stat;
	bool OurTurn = true;
	bool startATK = false;
	bool finished = false;
	bool showDamage = false;
	bool enem_atk = false;
	bool dead = false;
	public GameObject Player;
	public GameObject[] All_Enemy;
	GameObject Enemy;
	GameObject OurEnemy;
	Rigidbody2D rigid;
	Animator anim;
	Vector2 init_player;
	Vector2 init_enem;
	string skill_name;
	Text statText;
	Text Player_HP;
	Text Enem_HP;
	Text Enem_LVL;
	Text Player_LVL;
	Text PlayerName;
	Text EnemyName;
	Scrollbar Player_HPBar;
	Scrollbar Enem_HPBar;

	// Use this for initialization
	void Start () {


		statText =  GameObject.Find ("Event Text").GetComponent<Text> ();
		Player_HP =  GameObject.Find ("Player_HP").GetComponent<Text> ();
		Enem_HP =  GameObject.Find ("Enem_HP").GetComponent<Text> ();
		PlayerName =  GameObject.Find ("PlayerName").GetComponent<Text> ();
		EnemyName =  GameObject.Find ("EnemyName").GetComponent<Text> ();
		Player_LVL = GameObject.Find ("Player_LVL").GetComponent<Text> ();
		Enem_LVL = GameObject.Find ("Enem_LVL").GetComponent<Text> ();
		Player_HPBar = GameObject.Find ("PlayerHealth").GetComponent<Scrollbar> ();
		Enem_HPBar = GameObject.Find ("EnemHealth").GetComponent<Scrollbar> ();
		PlayerName.text = stat.Player.Name;
		EnemyName.text = enem_stat.monster.Name;
		stat.CurHP = stat.BaseHP;
		enem_stat.CurHP = enem_stat.BaseHP;

	
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator YouDead() {
		anim = Player.GetComponent<Animator> ();
		anim.SetBool ("Dead", true);
		yield return null;
		anim.SetBool ("Dead", false);
		Debug.Log ("You are dead");
		yield return new WaitForSeconds (1f);
		//Time.timeScale = 0;
		}

	IEnumerator EnemDead() {
		dead = true;
		enem_atk = false;
		finished = false;
		anim = OurEnemy.GetComponent<Animator> ();
		anim.SetBool ("Dead", true);
		yield return null;
		anim.SetBool ("Dead", false);
		Debug.Log ("You kill the enemy");
		yield return new WaitForSeconds (1f);
		Time.timeScale = 0;



	}




	void Awake () {
		stat = Player.GetComponent<PStats>();
		Enemy = All_Enemy[Random.Range (0, All_Enemy.Length)];
		OurEnemy = (GameObject) Instantiate(Enemy,new Vector2(-1.22f, 0), Quaternion.identity);
		enem_stat = OurEnemy.GetComponent<Monsters> ();
		stat.StatUpdate ();
		enem_stat.StatUpdate ();

	}

	void OnGUI () {
		Player_LVL.text = "Level: " + stat.Player.LVL;
		Enem_LVL.text = "Level: " + enem_stat.monster.LVL;
		Player_HP.text = "HP: " + stat.CurHP + "/" + stat.BaseHP;
		Player_HPBar.size = (float) stat.CurHP / (float) stat.BaseHP;
		Enem_HP.text = enem_stat.CurHP + "/" + enem_stat.BaseHP;
		Enem_HPBar.size = (float) enem_stat.CurHP / (float) enem_stat.BaseHP;


		if (finished) {

			statText.text = "Attacking the enemy with " + skill_name;
			//Status showing which skill we use to attack the enemy
		} else if (enem_atk) {
			statText.text = "The enemy is attacking you";

		} else if (dead) {

			statText.text = "You have killed the enemy";

		}

		else {

			statText.text = "";

		}

		//Display Damage when get ATK (still need improvement)
//		if (showDamage) {
//			GUI.Box (new Rect (170, 250, 25, 25), stat.BaseATK.ToString());
//
//		}
		//When we click the attack button (display 4 different skills)
		if (OurTurn && !startATK) {


			if (GUI.Button (new Rect (200, 380, 125, 50), "Attack")) {

				startATK = true;


			}
		}
		//__Init__ the 4 skills from the skill pool (need to configure the tree system etc.
		if (startATK) {
			int x1;
			int y1;
			for (int i = 0; i < stat.Player.skill.Length; i++) {
				if (i < 2) {
					x1 = 200;
					y1 = 380;
				}
				else {
					x1 = 380;
					y1 = 200;

				}

				if (GUI.Button (new Rect (x1 + i*0 , y1 + i * 50 , 100, 50), "" + stat.Player.skill[i].Name)) {
					
					print (stat.Player.skill[i].Name);
					if (stat.Player.skill[i].Name.ToString() == "Induction") {

						StartCoroutine(Induction(i));
						skill_name = stat.Player.skill[i].Name.ToString();


					}
					if (stat.Player.skill[i].melee) {
						finished = !finished;
						StartCoroutine (MeleeATK (i));
						skill_name = stat.Player.skill[i].Name.ToString();
					}
				}
			}
		}
	}

	//Player Normal attack skill 
	
	IEnumerator MeleeATK (int i) {
		OurTurn = false;
		startATK = false;
		finished = true;
		init_player = Player.transform.position;
		init_enem = OurEnemy.transform.position;
		rigid = Player.GetComponent<Rigidbody2D> ();
		anim = Player.GetComponent<Animator> ();
		anim.SetBool("WalkL", true);
		//initiate the attack by going forward
		while (Player.transform.position.x >= init_enem.x) {
			yield return new WaitForEndOfFrame();
			anim.SetBool("WalkL", false);
			rigid.velocity = new Vector3 (OurEnemy.transform.position.x, 0, 0) * Time.deltaTime * 50f;
		}
		float time = 2f;
		anim.SetBool("Slap", true);
		//Stop so that the character can attack
		while (time > 0) {
			showDamage = true;
			yield return new WaitForEndOfFrame();
			anim.SetBool("Slap", false);
			rigid.velocity = new Vector3 (OurEnemy.transform.position.x, 0, 0) * Time.deltaTime * 0;
			time -= Time.deltaTime;
		}
		enem_stat.CurHP -= stat.Player.skill[i].Damage;
		anim.SetBool("WalkR", true);
		showDamage = false;
		//Character go back to the same place
		while (Player.transform.position.x <= init_player.x) {
			yield return new WaitForEndOfFrame();
			anim.SetBool("WalkR", false);
			rigid.velocity = new Vector3 (-OurEnemy.transform.position.x, 0, 0) * Time.deltaTime * 50f;
			OurEnemy.transform.position = Vector3.Lerp(OurEnemy.transform.position, init_enem ,Time.deltaTime * 2f );

		}

		rigid.velocity = new Vector3 (OurEnemy.transform.position.x, 0, 0) * Time.deltaTime * 0;

		anim.SetBool("IDLE", true);
		finished = false;
		yield return new WaitForEndOfFrame ();
		anim.SetBool ("IDLE", false);
		if (enem_stat.CurHP < 0) {
			//StopAllCoroutines();
			statText.text = "You have killed the enemy";
			StartCoroutine(EnemDead());
			
			
		} else {
			StartCoroutine (eTurn ());
		}



	}

	IEnumerator Induction(int i) {
		OurTurn = false;
		startATK = false;
		finished = true;
		SpriteRenderer sprite;
		sprite = OurEnemy.GetComponent<SpriteRenderer> ();
		for (int x = 0; x < 10 ; x++) {

			sprite.enabled = !sprite.enabled;
			yield return new WaitForSeconds(0.25f);
			 
		}

		sprite.enabled = true;
		enem_stat.CurHP -= stat.Player.skill[i].Damage;
		yield return new WaitForSeconds (1f);

		if (enem_stat.CurHP <= 0) {
			StopAllCoroutines();
			StartCoroutine(EnemDead());


		} else {
			finished = false;
			StartCoroutine (eTurn ());
		}

	}

	//Enemy Normal attack skill
	IEnumerator EMeleeATK () {
		enem_atk = true;
		anim = OurEnemy.GetComponent<Animator> ();
		rigid = OurEnemy.GetComponent<Rigidbody2D> ();
		init_player = Player.transform.position;
		init_enem = OurEnemy.transform.position;
		anim.SetBool("WalkR", true);
		//Start walking to attack
		while (OurEnemy.transform.position.x <= init_player.x) {
			yield return new WaitForEndOfFrame();
			anim.SetBool("WalkR", false);
			rigid.velocity = new Vector3 (Player.transform.position.x, 0, 0) * Time.deltaTime * 50f;
		}
		float time = 2f;
		anim.SetBool("Slash", true);
		//Start attacking the enemy
		while (time > 0) {
			yield return new WaitForEndOfFrame();
			anim.SetBool("Slash", false);
			rigid.velocity = new Vector3 (OurEnemy.transform.position.x, 0, 0) * Time.deltaTime * 0;
			time -= Time.deltaTime;
		}
		stat.CurHP -= enem_stat.monster.skill [0].Damage;
		anim.SetBool("WalkL", true);
		while (OurEnemy.transform.position.x >= init_enem.x) {

			yield return new WaitForEndOfFrame();
			anim.SetBool("WalkL", false);
			rigid.velocity = new Vector3 (-Player.transform.position.x, 0, 0) * Time.deltaTime * 50f;
			Player.transform.position = Vector3.Lerp(Player.transform.position, init_player ,Time.deltaTime * 2f );
			
		}
		
		rigid.velocity = new Vector3 (OurEnemy.transform.position.x, 0, 0) * Time.deltaTime * 0;
		anim.SetBool("IDLE", true);
		enem_atk = false;
		OurTurn = true;
		yield return new WaitForEndOfFrame();
		anim.SetBool ("IDLE", false);

	}

	IEnumerator eTurn () {
			yield return new WaitForSeconds (2);
			//insert random skill function here will be added later
			StartCoroutine(EMeleeATK());
			//OurTurn = true;


	}


	
}
