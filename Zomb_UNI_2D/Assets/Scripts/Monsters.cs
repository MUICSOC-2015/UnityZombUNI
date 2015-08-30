using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Monsters : MonoBehaviour {

	public Stats monster;
	public int BaseHP ;
	public int CurHP ;
	public int BaseMP ;
	public int BaseATK ;
	public int CurATK ;
	public int BaseDEF ;
	public int curDEF  ;
	public float Evasion ;
	
	public void StatUpdate() {

		BaseHP = monster.STR * 10;
		BaseMP = monster.INT * 5;
		BaseDEF = monster.STR * 2;
		BaseATK = monster.STR * 2;
		Evasion = monster.AGI * 0.1f;

	}


}
