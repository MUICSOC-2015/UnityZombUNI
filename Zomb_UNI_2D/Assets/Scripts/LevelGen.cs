using UnityEngine;
using System.Collections;

public class LevelGen : MonoBehaviour {


	public GameObject ground;
	public GameObject wall;
	public int row;
	public int col;

	// Use this for initialization
	void Start () {
		// create ground
		for (int y = 0; y < row; y++) {
			for (int x = 0; x < col; x++) {
				Instantiate(ground, new Vector2(x, y), Quaternion.identity);
			}
		}

		for (int i = 0; i < col; i++) {
			// create wall
			Instantiate(wall, new Vector2(i, -1), Quaternion.identity);
			Instantiate(wall, new Vector2(i, row), Quaternion.identity);

		}

		for (int i = 0; i < row; i++) {
			// create wall
			Instantiate(wall, new Vector2(col, i), Quaternion.identity);
			Instantiate(wall, new Vector2(-1,i), Quaternion.identity);
			
		}


	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
