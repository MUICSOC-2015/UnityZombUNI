using UnityEngine;
using System.Collections;
[System.Serializable]
public class PStats : MonoBehaviour {

	public Stats Player;
	public int BaseHP ;
	public int CurHP ;
	public int BaseMP ;
	public int BaseATK ;
	public int CurATK ;
	public int BaseDEF ;
	public int curDEF  ;
	public float Evasion ;
	
	public void StatUpdate() {

		BaseHP = Player.STR * 10;
		BaseMP = Player.INT * 5;
		BaseDEF = Player.STR * 2;
		BaseATK = Player.STR * 2;
		Evasion = Player.AGI * 0.1f;


	}

}
