using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
	public enum GameState
	{
		Menu,
		Start,
		HighScore,
		Playing,
		Won,
		Lost
	}

	public enum PlayerState
	{
		Running, 
		inAir, 
		Dead
	}
}
