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
	bool lvlup = false;
	bool display = true;
	bool dtem = true;
	float culTime = 0;
	public GameObject Player;
	public GameObject[] All_Enemy;
	GameObject Enemy;
	GameObject OurEnemy;
	RectTransform ex_UI;
	Text ex_UI_txt;
	RectTransform ex_UI2;
	Text ex_UI_txt2;
	Rigidbody2D rigid;
	Animator anim;
	Animator anim2;
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
	Text Player_MP;
	Text Enem_MP;
	Text STR;
	Text INT;
	Text AGI;
	Text statPoint;
	GameObject Canvas;
	GameObject Back;
	GameObject STR_up;
	GameObject INT_up;
	GameObject AGI_up;
	GameObject X;
	GameObject Y;
	GameObject OurItem;
	Scrollbar Player_HPBar;
	Scrollbar Enem_HPBar;
	Scrollbar Player_MPBar;
	Scrollbar Enem_MPBar;
	Item item_info;
	List<GameObject> current_item;
	RectTransform trans;


	public void Display() {

		display = true;
		startATK = false;
		dtem = true;
		Destroy (OurItem);
		if (current_item.Count > 0) {

			foreach (GameObject x in current_item) {

				Destroy(x);

			}

			current_item.Clear();


		}

	}
	// Use this for initialization mainly to Initialize the GUI and set the stats
	void Start () {
		current_item = new List<GameObject> ();
		Canvas = GameObject.Find ("Canvas");
		ex_UI = GameObject.Find ("dmg_text").GetComponent<RectTransform> ();
		ex_UI_txt = GameObject.Find ("dmg_text").GetComponent<Text> ();
		ex_UI2 = GameObject.Find ("Bubble").GetComponent<RectTransform> ();
		ex_UI_txt2 = GameObject.Find ("speechBubble").GetComponent<Text> ();
		statPoint = GameObject.Find ("StatPoint").GetComponent<Text> ();
		Back = GameObject.Find ("Button");
		STR_up = GameObject.Find ("STR+");
		AGI_up = GameObject.Find ("INT+");
		INT_up = GameObject.Find ("AGI+");
		STR = GameObject.Find ("STR").GetComponent<Text> ();
		INT = GameObject.Find ("INT").GetComponent<Text> ();
		AGI = GameObject.Find ("AGI").GetComponent<Text> ();
		statText =  GameObject.Find ("Event Text").GetComponent<Text> ();
		Player_HP =  GameObject.Find ("Player_HP").GetComponent<Text> ();
		Enem_HP =  GameObject.Find ("Enem_HP").GetComponent<Text> ();
		PlayerName =  GameObject.Find ("PlayerName").GetComponent<Text> ();
		EnemyName =  GameObject.Find ("EnemyName").GetComponent<Text> ();
		Player_LVL = GameObject.Find ("Player_LVL").GetComponent<Text> ();
		Enem_LVL = GameObject.Find ("Enem_LVL").GetComponent<Text> ();
		Player_HPBar = GameObject.Find ("PlayerHealth").GetComponent<Scrollbar> ();
		Enem_HPBar = GameObject.Find ("EnemHealth").GetComponent<Scrollbar> ();
		Enem_MP = GameObject.Find ("Enem_MP ").GetComponent<Text> ();
		Enem_MPBar = GameObject.Find ("EnemMagic ").GetComponent<Scrollbar> ();
		Player_MP = GameObject.Find ("Player_MP").GetComponent<Text> ();
		Player_MPBar = GameObject.Find ("PlayerMagic").GetComponent<Scrollbar> ();
		PlayerName.text = stat.Player.Name;
		EnemyName.text = enem_stat.monster.Name;
		if (PlayerPrefs.GetInt ("HP") == 0) {
			stat.CurHP = stat.BaseHP;
			stat.CurMP = stat.BaseMP;
		} else {

			stat.CurHP = PlayerPrefs.GetInt("HP");
			stat.CurMP = PlayerPrefs.GetInt("MP");
			stat.CurEXP = PlayerPrefs.GetInt("EXP");
			stat.Player.LVL = PlayerPrefs.GetInt("LVL");
			stat.BaseEXP = PlayerPrefs.GetInt("BEXP");
		}
		enem_stat.CurHP = enem_stat.BaseHP;
		X = GameObject.Find ("Bubble");
		//XA = GameObject.Find ("speechBubble");
		Y = GameObject.Find ("dmg_text");
		Y.SetActive (false);
		X.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {

		//display The stats
		if (!display && !startATK &&dtem) {

			STR.text = "STR: " + stat.Player.STR;
			AGI.text = "AGI: " + stat.Player.AGI;
			INT.text = "INT: " + stat.Player.INT;
			statPoint.text = "Stat Point: " + stat.statPoint;

		}
		// display Damage when get attack
		if (showDamage) {
			Y.SetActive(true);
			anim2 = Canvas.GetComponent<Animator>();
			culTime = culTime + 0.0725f * Time.deltaTime;
			Vector2 pos = new Vector2(OurEnemy.transform.position.x + 0.55f, OurEnemy.transform.position.y + 0.15f);
			Vector2 viewportPoint = Camera.main.WorldToViewportPoint (pos);
			ex_UI.anchorMin = new Vector2(viewportPoint.x, viewportPoint.y + culTime);
			ex_UI.anchorMax = new Vector2(viewportPoint.x, viewportPoint.y + culTime);
			ex_UI_txt.text = stat.BaseATK.ToString ();
			StartCoroutine(OneShot("away"));
			if(!showDamage) {

				culTime = 0;
			}
			
		} else {
			Y.SetActive(false);
			ex_UI_txt.text = "";

		}

		Player_LVL.text = "Level: " + stat.Player.LVL + " | EXP: "+ stat.CurEXP + "/" + stat.BaseEXP;
		Enem_LVL.text = "Level: " + enem_stat.monster.LVL;
		Player_HP.text = "HP: " + stat.CurHP + "/" + stat.BaseHP;
		Player_HPBar.size = (float) stat.CurHP / (float) stat.BaseHP;
		Enem_HP.text = "HP: " + enem_stat.CurHP + "/" + enem_stat.BaseHP;
		Enem_HPBar.size = (float)enem_stat.CurHP / (float)enem_stat.BaseHP; 
		Player_MPBar.size = (float) stat.CurMP / (float) stat.BaseMP;
		Player_MP.text = "MP: " + stat.CurMP + "/" + stat.BaseMP;

	}
	//the function that happen when you are dead
	IEnumerator YouDead() {
		anim = Player.GetComponent<Animator> ();
		anim.SetBool ("Dead", true);
		yield return null;
		anim.SetBool ("Dead", false);
		Debug.Log ("You are dead");
		yield return new WaitForSeconds (1f);
		//Time.timeScale = 0;
		}
	//The function to end the current session when the enemy is dead
	IEnumerator EnemDead() {
		dead = true;
		enem_atk = false;
		finished = false;
		if (stat.CurEXP + enem_stat.monster.EXP >= stat.BaseEXP) {
			yield return new WaitForSeconds (1.5f);
			lvlup = true;
			dead = false;
			stat.Player.LVL ++;
			stat.CurEXP += enem_stat.monster.EXP;
			stat.CurEXP -= stat.BaseEXP;
			stat.statPoint += 3; 
			yield return null;
			PlayerPrefs.SetInt("statPoint", stat.statPoint);
			print (statPoint);
			yield return null;
			stat.StatUpdate();
		} else {
			stat.CurEXP += enem_stat.monster.EXP;
		}
		anim = OurEnemy.GetComponent<Animator> ();
		yield return null;
		anim.SetBool ("Dead", true);
		yield return null;
		anim.SetBool ("Dead", false);
		Debug.Log ("You kill the enemy");
		yield return new WaitForSeconds (2f);
		PlayerPrefs.SetInt ("HP", stat.CurHP);
		PlayerPrefs.SetInt ("MP", stat.CurMP);
		PlayerPrefs.SetInt ("EXP", stat.CurEXP);
		PlayerPrefs.SetInt ("LVL", stat.Player.LVL);
		PlayerPrefs.SetInt ("BEXP", stat.BaseEXP);
		yield return null;
		Application.LoadLevel (Application.loadedLevel);



	}



	//initilisation before starting the game
	void Awake () {
		stat = Player.GetComponent<PStats>();
		Enemy = All_Enemy[Random.Range (0, All_Enemy.Length)];
		OurEnemy = (GameObject) Instantiate(Enemy,new Vector2(-1.22f, 0), Quaternion.identity);
		enem_stat = OurEnemy.GetComponent<Monsters> ();
		if (!PlayerPrefs.HasKey("init")) {
			PlayerPrefs.SetInt ("STR", stat.Player.STR);
			PlayerPrefs.SetInt ("AGI", stat.Player.AGI);
			PlayerPrefs.SetInt ("INT", stat.Player.INT);
			PlayerPrefs.SetInt ("init", 1);
		}
		stat.StatUpdate ();
		enem_stat.StatUpdate ();

	}
	//most of the stuff that gets to do with the GUI
	void OnGUI () {

		if (GUI.Button (new Rect (0, 0, 75, 25), "Reset")) {

			PlayerPrefs.DeleteAll();

		}
		// showing status of the current battle progress
		if (finished) {
			statText.enabled = enabled;
			statText.text = "Attacking the enemy with " + skill_name;
			//Status showing which skill we use to attack the enemy
		} else if (enem_atk) {
			statText.text = "The enemy is attacking you";
			statText.enabled = enabled;

		} else if (dead) {
			statText.enabled = enabled;
			statText.text = "You have killed the enemy";

		} else if (lvlup) {
			statText.enabled = enabled;
			statText.text = "Level up!! Click the stat button to increase stat.";

		} 

		else {

			statText.text = "";
			statText.enabled = false;
		}

		//Show the current items
		Transform topItem;
		if (OurTurn && !startATK && display) {
			if (GUI.Button (new Rect (200, 430, 125, 50), "Item")) {
				dtem = false;
				display = false;
				Back.SetActive(true);
				for (int i = 0; i < stat.Item.Length; i++) {
					OurItem = (GameObject)Instantiate (stat.Item[i], new Vector2 (0, 0), Quaternion.identity);
					current_item.Add(OurItem);
					item_info = OurItem.GetComponent<Item>();
					if (item_info.Amount > 0) {
						Text txt;
						txt = OurItem.GetComponentInChildren<Text> ();
						topItem = OurItem.transform.GetChild(0);
						trans = topItem.GetComponent<RectTransform>();
						trans.offsetMin = new Vector2(trans.offsetMin.x+i*200,trans.offsetMin.y);
						trans.offsetMax = new Vector2(trans.offsetMax.x+i*200, trans.offsetMax.y);
						txt.text = item_info.Name + " X" + item_info.Amount;

					}


				}

			}
		}
		//When we click the attack button (display 4 different skills)
		if (OurTurn && !startATK && display) {
			if (GUI.Button (new Rect (200, 380, 125, 50), "Attack")) {

				startATK = true;
				display = false;
			}
		}
		//Show the stats of the character
		if (OurTurn && display) {
			if (GUI.Button (new Rect (400, 380, 125, 50), "Stats")) {
				stat.StatUpdate();
				display = false;
				Back.SetActive (true);
				STR_up.SetActive (true);
				AGI_up.SetActive (true);
				INT_up.SetActive (true);

				
				
			} else {
				STR.text = "";
				AGI.text = "";
				INT.text = "";
				Back.SetActive (false);
				STR_up.SetActive (false);
				AGI_up.SetActive (false);
				INT_up.SetActive (false);
				statPoint.text = "";
			}
		} 
		//__Init__ the 4 skills from the skill pool (need to configure the tree system etc.
		if (startATK && !display) {
			Back.SetActive (true);
			int x1;
			int y1;
			for (int i = 0; i < stat.Player.skill.Length; i++) {
				if (i < 2) {
					x1 = 200;
					y1 = 380;
				} else {
					x1 = 380;
					y1 = 200;

				}

				if (GUI.Button (new Rect (x1 + i * 0, y1 + i * 50, 100, 50), "" + stat.Player.skill [i].Name)) {
					Back.SetActive (false);
					print (stat.Player.skill [i].Name);

					if (stat.Player.skill [i].melee) {
						finished = !finished;
						StartCoroutine (MeleeATK (i));
						skill_name = stat.Player.skill [i].Name.ToString ();
					}

					if (stat.CurMP < stat.Player.skill [i].MP) {

						StartCoroutine (showStatus ());
						Debug.Log ("You don't have enough MP");


					} else {
						// check if the player can use the skill or not by checking their MP
						if (stat.Player.skill [i].Name.ToString () == "Induction") {
							
							StartCoroutine (Induction (i));
							skill_name = stat.Player.skill [i].Name.ToString ();
							
						}


						stat.CurMP -= stat.Player.skill [i].MP;

					}
				}
			}
		} 
	}

	IEnumerator showStatus () {

		X.SetActive (true);
		//XA.SetActive (true);
		Vector2 pos = new Vector2(Player.transform.position.x, Player.transform.position.y + 0.50f);
		Vector2 viewportPoint = Camera.main.WorldToViewportPoint (pos);
		ex_UI2.anchorMin = viewportPoint;
		ex_UI2.anchorMax = viewportPoint;
		ex_UI_txt2.text = "Need more YOYO!!";
		yield return new WaitForSeconds (1f);
		X.SetActive (false);
		//XA.SetActive (false);

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
			rigid.velocity = new Vector3 (OurEnemy.transform.position.x, 0, 0) * Time.deltaTime * 75f;
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
			rigid.velocity = new Vector3 (-OurEnemy.transform.position.x, 0, 0) * Time.deltaTime * 75f;
			OurEnemy.transform.position = Vector3.Lerp(OurEnemy.transform.position, init_enem ,Time.deltaTime * 2f );

		}

		rigid.velocity = new Vector3 (OurEnemy.transform.position.x, 0, 0) * Time.deltaTime * 0;

		anim.SetBool("IDLE", true);
		finished = false;
		yield return new WaitForEndOfFrame ();
		anim.SetBool ("IDLE", false);
		if (enem_stat.CurHP <= 0) {
			StopAllCoroutines();
			StartCoroutine(EnemDead());
			
		} else {
			StartCoroutine (eTurn ());
		}



	}
	//one of the enemy skill (this is used for testing purposes
	IEnumerator Induction(int i) {
		OurTurn = false;
		startATK = false;
		finished = true;
		SpriteRenderer sprite;
		sprite = OurEnemy.GetComponent<SpriteRenderer> ();
		for (int x = 0; x < 10 ; x++) {

			sprite.enabled = !sprite.enabled;
			showDamage = true;
			yield return new WaitForSeconds(0.25f);
			 
		}

		sprite.enabled = true;
		enem_stat.CurHP -= stat.Player.skill[i].Damage;
		showDamage = false;
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
			rigid.velocity = new Vector3 (Player.transform.position.x, 0, 0) * Time.deltaTime * 75f;
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
			rigid.velocity = new Vector3 (-Player.transform.position.x, 0, 0) * Time.deltaTime * 75f;
			Player.transform.position = Vector3.Lerp(Player.transform.position, init_player ,Time.deltaTime * 2f );
			
		}
		
		rigid.velocity = new Vector3 (OurEnemy.transform.position.x, 0, 0) * Time.deltaTime * 0;
		anim.SetBool("IDLE", true);
		enem_atk = false;
		OurTurn = true;
		yield return new WaitForEndOfFrame();
		anim.SetBool ("IDLE", false);

	}
	//Function to control the enemy in the future more skill can be added
	IEnumerator eTurn () {
			yield return new WaitForSeconds (1);
			//insert random skill function here will be added later
			StartCoroutine(EMeleeATK());
			//OurTurn = true;


	}

	IEnumerator OneShot (string name) {

		anim2.SetBool (name, true);
		yield return null;
		anim2.SetBool (name, false);

	}


	
}
