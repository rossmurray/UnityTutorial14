using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
	public Sprite DamageSprite;
	public int Hp = 4;

	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		this.spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	public void DamageWall(int loss)
	{
		this.spriteRenderer.sprite = this.DamageSprite;
		this.Hp -= loss;
		if(Hp <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
