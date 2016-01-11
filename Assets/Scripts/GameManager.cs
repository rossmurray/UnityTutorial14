using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance = null;
	public BoardManager BoardManager;
	public int PlayerFoodPoints = 100;
	[HideInInspector]
	public bool IsPlayersTurn = true;

	private int level = 3;

	void Awake()
	{
		if(Instance == null)
		{
			Instance = null;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		BoardManager = GetComponent<BoardManager>();
		InitGame();
	}

	void InitGame()
	{
		BoardManager.SetupScene(level);
	}

	public void GameOver()
	{
		enabled = false;
	}
}
