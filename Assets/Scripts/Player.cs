using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
	public int WallDamage = 1;
	public int PointsPerFood = 10;
	public int PointsPerSoda = 20;
	public float RestartLevelDelay = 1f;

	private Animator animator;
	private int food;

	protected override void Start()
	{
		animator = GetComponent<Animator>();
		food = GameManager.Instance.PlayerFoodPoints;
		base.Start();
	}

	private void OnDisable()
	{
		GameManager.Instance.PlayerFoodPoints = food;
	}

	protected override void AttemptMove<T>(int xDir, int yDir)
	{
		food -= 1;
		base.AttemptMove<T>(xDir, yDir);
		RaycastHit2D hit;
		if(Move(xDir, yDir, out hit))
		{
			//sfx
		}
		CheckIfGameOver();
		GameManager.Instance.IsPlayersTurn = false;
	}

	private void CheckIfGameOver()
	{
		if(food <= 0)
		{
			GameManager.Instance.GameOver();
		}
	}

	void Update()
	{
		if(!GameManager.Instance.IsPlayersTurn)
		{
			return;
		}
		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw("Horizontal");
		vertical = (int)Input.GetAxisRaw("Vertical");

		if(horizontal != 0)
		{
			vertical = 0;
		}
		if(horizontal != 0 || vertical != 0)
		{
			AttemptMove<Wall>(horizontal, vertical);
		}
	}

	protected override void OnCantMove<T>(T component)
	{
		Wall hitWall = component as Wall;
		hitWall.DamageWall(this.WallDamage);
		animator.SetTrigger("playerChop");
	}

	private void OnTriggerEnter2d(Collider2D other)
	{
		if(other.tag == "Exit")
		{
			Invoke("Restart", RestartLevelDelay);
			enabled = false;
		}
		else if(other.tag == "Food")
		{
			food += PointsPerFood;
			other.gameObject.SetActive(false);
		}
		else if(other.tag == "Soda")
		{
			food += PointsPerSoda;
			other.gameObject.SetActive(false);
		}
	}

	private void Restart()
	{
		//SceneManager.LoadScene("Main");
		Application.LoadLevel(Application.loadedLevel);
	}

	public void LoseFood(int loss)
	{
		animator.SetTrigger("playerHit");
		this.food -= loss;
		CheckIfGameOver();
	}
}
