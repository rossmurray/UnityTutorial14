using UnityEngine;
using System.Collections;
using System;

public class Enemy : MovingObject
{
	public int PlayerDamage;

	private Animator animator;
	private Transform target;
	private bool skipMove;
	
	protected override void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	protected override void OnCantMove<T>(T component)
	{
		throw new NotImplementedException();
	}
}
