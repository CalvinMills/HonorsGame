using UnityEngine;
using System.Collections;
using Assets.Scripts;
public class PlayerScript : MonoBehaviour {

	private GameState gameState;
	private PlayerState playerState;
	private int jumpNumber;
	private Animator anim;
	private string colTag;
	// Use this for initialization
	void Start () 
	{
		gameState = GameController.Instance.gState;
		jumpNumber = 0;
		playerState = PlayerState.Running;
		anim = this.gameObject.GetComponent<Animator> ();
		colTag = "Terrain";
	}

	void Awake()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		colTag = "Air";
		switch (gameState) {
		case GameState.Playing:
			{
			if(playerState == PlayerState.Running)
		   	{
				Debug.Log(colTag);
				if (Input.GetKeyDown (KeyCode.Space))
				{
					playerState = PlayerState.inAir;
					Jump();
					anim.SetInteger("State", 1);

				}
			}
			if(playerState == PlayerState.inAir)
			{
				//Debug.Log(this.GetComponent<Rigidbody2D>().velocity.y);
				if(this.GetComponent<Rigidbody2D>().velocity.y == 0f )
				{
					jumpNumber = 0;
					playerState = PlayerState.Running;
					anim.SetInteger("State", 0);
				}
			}
			break;
			}
		default:
			break;
	
		}
	}

	void Jump()
	{
		if (jumpNumber == 0) {
			jumpNumber ++;
			this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 3000);
		}


	}

	void OnCollisionEnter2D (Collision2D col) 
	{
		colTag =  col.gameObject.tag;
	}


}
