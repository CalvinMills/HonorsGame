using UnityEngine;
using System.Collections;
using Assets.Scripts;
public class GameController : MonoBehaviour {

	[HideInInspector]
	public static GameController Instance { get; private set;}

	[HideInInspector]
	public GameState gState;

	public GameObject player ;

	void Awake()
	{
		Instance = this;
		gState = GameState.Playing;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
