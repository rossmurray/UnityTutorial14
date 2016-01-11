using UnityEngine;
using System.Collections;

public class Player : MovingObject
{
	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	public float restartLevelDelay = 1f;

	private Animator Animator;
	private int food;

	protected override void Start()
	{
		Animator = GetComponent<Animator>();
		food = GameManager.instance.playerFoodPoints;
		base.Start();
	}

	private void OnDisable()
	{
		GameManager.instance.playerFoodPoints = food;
	}

	protected override void AttemptMove <T>(int xDir, int yDir)
	{
		food -= 1;
		base.AttemptMove<T>(xDir, yDir);
	}

	private void CheckIfGameOver()
	{
		if(food <= 0)
		{
			GameManager.instance.GameOver();
		}
	}
}
