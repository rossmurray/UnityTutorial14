using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

	[Serializable]
	public class Count
	{
		public int maximum;
		public int minimum;

		public Count(int min, int max)
		{
			this.minimum = min;
			this.maximum = max;
		}
	}

	public int columns = 8;
	public int rows = 8;
	public Count wallCount = new Count(5, 9);
	public Count foodCount = new Count(1, 5);
	public GameObject exit;
	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] foodTiles;
	public GameObject[] enemyTiles;
	public GameObject[] outerWallTiles;

	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	void InitializeList()
	{
		gridPositions.Clear();
		for(int c = 1; c < columns - 1; c++)
		{
			for(int r = 1; r < rows - 1; r++)
			{
				gridPositions.Add(new Vector3(c, r, 0f));
			}
		}
	}

	void BoardSetup()
	{
		boardHolder = new GameObject("Board").transform;
		for(int c = -1; c < columns + 1; c++)
		{
			for(int r = -1; r < rows + 1; r++)
			{
				GameObject toInstantiate;
				if(r == -1 || r == rows || c == -1 || c == columns)
				{
					toInstantiate = GetRandomTile(outerWallTiles);
				}
				else
				{
					toInstantiate = GetRandomTile(floorTiles);
				}
				var instance = Instantiate(toInstantiate, new Vector3(c, r, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
	}

	Vector3 RandomPosition()
	{
		var randomIndex = Random.Range(0, gridPositions.Count);
		var randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex);
		return randomPosition;
	}

	void LayoutObjectAtRandom(GameObject[] tileSource, int minimum, int maximum)
	{
		var objectCount = Random.Range(minimum, maximum + 1);
		for(int i = 0; i < objectCount; i++)
		{
			var randomPosition = RandomPosition();
			var tileChoice = GetRandomTile(tileSource);
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}
	}

	private GameObject GetRandomTile(GameObject[] tileSource)
	{
		return tileSource[Random.Range(0, tileSource.Length)];
	}

	public void SetupScene(int level)
	{
		BoardSetup();
		InitializeList();
		LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
		LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
		var enemyCount = (int)Mathf.Log(level, 2f);
		LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
		Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
	}
}
