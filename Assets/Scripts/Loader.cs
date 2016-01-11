using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
	public class Loader : MonoBehaviour
	{
		public GameManager GameManager;
		//public SoundManager SoundManager;

		void Awake()
		{
			if(GameManager.Instance == null)
			{
				Instantiate(GameManager);
			}
		}
	}
}
