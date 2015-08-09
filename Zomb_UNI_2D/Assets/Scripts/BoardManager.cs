using UnityEngine;
using System;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count
	{
		public int maximum;
		public int minimum;
		public Count (int max, int min)
		{
			maximum = max;
			minimum = min;
		}
	}

	public int columns = 10;
	public int rows = 10;
	public float size = 0.5f;
	public GameObject exit;
	public GameObject floorTiles;
	public GameObject tableTiles;
	public GameObject chairTiles;
	public GameObject playerTiles;
	public GameObject leftWallTiles;
	public GameObject rightWallTiles;
	public GameObject macTiles;
	public GameObject boardTiles;

	private Transform boardHolder;
	private List <Vector3> gridPosition = new List<Vector3> ();

	void InitialiseList()
	{
		gridPosition.Clear ();

		for (int x = 1; x < columns + 1; x++) 
		{
			for(int y = 1; y < rows + 1; y++)
			{
				gridPosition.Add(new Vector3(x,y,0f));
			}
		}
	}


	void BoardSetup()
	{
		boardHolder = new GameObject ("Board").transform;

		for (int x = 1; x < columns + 1; x++) 
		{
			for(int y = 1; y < rows + 1; y++)
			{

				if(x == 1)
				{
					GameObject leftWall = leftWallTiles;
					GameObject instance = Instantiate(leftWall, new Vector3 (x *size , y*size , 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if(x == columns)
				{
					GameObject rightWall = rightWallTiles;
					GameObject instance = Instantiate(rightWall, new Vector3 (x*size , y*size , 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if(x != columns && x != 1 && y == rows)
				{
					GameObject mac = macTiles;
					GameObject instance = Instantiate(mac, new Vector3 (x *size, y *size, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				
				if( x != 1 && x != 10 && y == 1)
				{
					GameObject board = boardTiles;
					GameObject instance = Instantiate(board, new Vector3 (x *size, y *size, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if (x != 1 && x != columns && y != 1 && y!= rows)
				{
					GameObject toInstantiate = floorTiles;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x *size, y *size, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
			
			}
		}
		Layout (boardHolder);
	}

	void Layout(Transform boardholder)
	{
		GameObject toInstantiate = new GameObject();
		for (int x = 1; x < columns + 1; x++) 
		{
			for (int y = 1; y < rows + 1; y++) 
			{
				if( (x == 3 && y == 4) || (x == 3 && y == 3) || (x == 4 && y == 3) || (x == 4 && y == 4))
				{
					toInstantiate = tableTiles;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x *size, y*size , 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);

				}
				if( (x == 7 && y == 4) || (x == 7 && y == 3) || (x == 8 && y == 3) || (x == 8 && y == 4))
				{
					toInstantiate = tableTiles;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x*size , y*size , 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if( (x == 3 && y == 7) || (x == 3 && y == 6) || (x == 4 && y == 7) || (x == 4 && y == 6))
				{
					toInstantiate = tableTiles;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x *size, y*size , 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if( (x == 7 && y == 6) || (x == 7 && y == 7) || (x == 8 && y == 7) || (x == 8 && y == 6))
				{
					toInstantiate = tableTiles;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x *size, y *size, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if( x == 2 && (y == 3 || y == 4 || y == 6 || y == 7))
				{
					toInstantiate = chairTiles;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x *size, y *size, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if( x == 5 && (y == 3 || y == 4 || y == 6 || y == 7))
				{
					toInstantiate = chairTiles;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x *size, y *size, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if( x == 6 && (y == 3 || y == 4 || y == 6 || y == 7))
				{
					toInstantiate = chairTiles;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x *size, y *size, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if( x == 9 && (y == 3 || y == 4 || y == 6 || y == 7))
				{
					toInstantiate = chairTiles;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x *size, y*size , 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if( x == 1 && (y == 8 || y == 9))
				{
					toInstantiate = exit;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x *size, y*size , 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if ( x == 2 && y == 2)
				{
					toInstantiate = playerTiles;
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x *size, y*size , 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
			}
		}
	}

	public void SetupScene(int level)
	{

		InitialiseList ();
		BoardSetup ();

	}

}
