using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[System.Serializable]
public class PStats : MonoBehaviour {

	public Stats Player;
	public GameObject[] Item;
	public int BaseHP ;
	public int CurHP ;
	public int CurMP;
	public int BaseMP ;
	public int BaseATK ;
	public int CurATK ;
	public int BaseDEF ;
	public int curDEF  ;
	public int BaseEXP;
	public int CurEXP;
	public int statPoint;
	public float Evasion ;
	public void StatUpdate() {


		BaseHP = Player.STR * 10;
		//PlayerPrefs.SetInt ("BaseHP", BaseHP);
		BaseMP = Player.INT * 5;
		BaseDEF = Player.STR * 2;
		BaseATK = Player.STR * 2;
		Evasion = Player.AGI * 0.1f;
		BaseEXP = Player.EXP * Player.LVL;

		Player.STR = PlayerPrefs.GetInt ("STR");
		Player.INT = PlayerPrefs.GetInt ("INT");
		Player.AGI = PlayerPrefs.GetInt ("AGI");
		statPoint = PlayerPrefs.GetInt ("statPoint");
		//PlayerPrefs.SetInt ("statPoint", statPoint);
		//print ("point: " + statPoint);

	}

	void Update () {

		BaseHP = Player.STR * 10;
		BaseMP = Player.INT * 5;
		BaseDEF = Player.STR * 2;
		BaseATK = Player.STR * 2;
		Evasion = Player.AGI * 0.1f;
		BaseEXP = Player.EXP * Player.LVL;


	}

	public void UPSTR() {
		if (statPoint > 0) {
			Player.STR++;
			PlayerPrefs.SetInt ("STR", Player.STR);
			statPoint --;
			PlayerPrefs.SetInt ("statPoint", statPoint);

		}

	}
	public void UPAGI() {
		if (statPoint > 0) {
			Player.AGI++;
			PlayerPrefs.SetInt ("AGI", Player.AGI);
			statPoint --;
			PlayerPrefs.SetInt ("statPoint", statPoint);
		}
		
	}
	public void UPINT() {
		if (statPoint > 0) {
			Player.INT++;
			PlayerPrefs.SetInt ("INT", Player.INT);
			statPoint --;
			PlayerPrefs.SetInt ("statPoint", statPoint);
		}
	}

	public void UsePotion(string Name) {
		
		if (Name == "SuperYOYO") {
			foreach (GameObject i in this.Item) {
				Item k;
				k = i.GetComponent<Item>();
				if (k.Name == Name) {
					k.Amount--;
					CurHP+= k.giveHP;
					CurMP+= k.giveMP;

				}
				
			}
			
		}
		
	}



}
