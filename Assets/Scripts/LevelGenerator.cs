using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	
	// Use this for initialization
	private GameObject collectedTiles;
	private const float tileWidth= 1.25f;
	private GameObject tilePos;
	private int heightLevel = 0;
	
	private GameObject gameLayer;
	private GameObject bgLayer;
	
	private GameObject tmpTile;
	
	private float startUpPosY;
	
	private float gameSpeed = 4.0f;
	private float outofboundsX;
	private int blankCounter = 0;
	private int middleCounter = 0;
	private string lastTile = "right";
	
	
	void Start () 
	{
		gameLayer = GameObject.Find("gameLayer");
		bgLayer = GameObject.Find("backgroundLayer");
		collectedTiles = GameObject.Find("tiles");
		for(int i = 0; i<20; i++){
			GameObject tmpG1 = Instantiate(Resources.Load("ground_left", typeof(GameObject))) as GameObject;
			tmpG1.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
			tmpG1.transform.position = Vector2.zero;
			GameObject tmpG2 = Instantiate(Resources.Load("ground_middle", typeof(GameObject))) as GameObject;
			tmpG2.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
			tmpG2.transform.position = Vector2.zero;
			GameObject tmpG3 = Instantiate(Resources.Load("ground_right", typeof(GameObject))) as GameObject;
			tmpG3.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
			tmpG3.transform.position = Vector2.zero;
			GameObject tmpG4 = Instantiate(Resources.Load("blank", typeof(GameObject))) as GameObject;
			tmpG4.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
			tmpG4.transform.position = Vector2.zero;
		}
		collectedTiles.transform.position = new Vector2 (-60.0f, -20.0f);
		
		tilePos = GameObject.Find("startTilePosition");
		startUpPosY = tilePos.transform.position.y;
		outofboundsX = tilePos.transform.position.x - 5.0f;

		
		
		fillScene ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		gameLayer.transform.position = new Vector2 (gameLayer.transform.position.x - gameSpeed * Time.deltaTime, 0);
		bgLayer.transform.position = new Vector2 ((gameLayer.transform.position.x - gameSpeed * Time.deltaTime)/4, 0);
		foreach (Transform child in gameLayer.transform) 
		{
			if(child.position.x < outofboundsX)
			switch(child.gameObject.name){
				
				case "ground_left(Clone)":
				child.gameObject.transform.position = collectedTiles.transform.FindChild("gLeft").transform.position;
				child.gameObject.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
				break;
				case "ground_middle(Clone)":
				child.gameObject.transform.position = collectedTiles.transform.FindChild("gMiddle").transform.position;
				child.gameObject.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
				break;
				case "ground_right(Clone)":
				child.gameObject.transform.position = collectedTiles.transform.FindChild("gRight").transform.position;
				child.gameObject.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
				break;
				case "blank(Clone)":
				child.gameObject.transform.position = collectedTiles.transform.FindChild("gBlank").transform.position;
				child.gameObject.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
				break;
				case "enemy(Clone)":
				child.gameObject.transform.position = collectedTiles.transform.FindChild("killers").transform.position;
				child.gameObject.transform.parent = collectedTiles.transform.FindChild("killers").transform;
				break;
				case "Reward":
				//GameObject.Find("Reward").GetComponent<crateScript>().inPlay = false;
				break;
				default:
				Destroy(child.gameObject);
				break;
				
			}
		}

		if (gameLayer.transform.childCount < 25)
			spawnTile ();
	}
	/*
	 * Fill scene initially
	*/
	private	void fillScene()
	{
		//Fill start
		for (int i = 0; i<15; i++)
		{
			setTile("middle");
		}
		setTile("right");
	}
	/*
	 * Set tile to spawn
	*/
	private void setTile(string type)
	{
		switch (type){
		case "left":
			tmpTile = collectedTiles.transform.FindChild("gLeft").transform.GetChild(0).gameObject;
			break;
		case "middle":
			tmpTile = collectedTiles.transform.FindChild("gMiddle").transform.GetChild(0).gameObject;
			break;
		case "right":
			tmpTile = collectedTiles.transform.FindChild("gRight").transform.GetChild(0).gameObject;
			break;
		case "blank":
			tmpTile = collectedTiles.transform.FindChild("gBlank").transform.GetChild(0).gameObject;
			break;
		}
		tmpTile.transform.parent = gameLayer.transform;
		tmpTile.transform.position = new Vector3(tilePos.transform.position.x+(tileWidth),startUpPosY+(heightLevel * tileWidth),0);
		tilePos = tmpTile;
		lastTile = type;
	}
	/*
	 * Method to Handle the spawning of tiles
	*/
	private void spawnTile(){
		
		if (blankCounter > 0) {
			
			setTile("blank");
			blankCounter--;
			return;
			
		}
		if (middleCounter > 0) {
			
			//randomizeEnemy();
			setTile("middle");
			middleCounter--;
			return;
			
		}
		//enemyAdded = false;
		
		
		if (lastTile == "blank") {
			
			changeHeight();
			setTile("left");
			middleCounter = (int)Random.Range(1,8);
			
		}else if(lastTile =="right"){
			//this.GetComponent<scoreHandler> ().Points ++;
			blankCounter = (int)Random.Range(1,3);
		}else if(lastTile == "middle"){
			setTile("right");
		}
	}

	/*
	 * Method to randomize the height of the tile being spawned, within a perturbation
	*/
	private void changeHeight()
	{
		int newHeightLevel = (int)Random.Range(0,4);
		if(newHeightLevel<heightLevel)
			heightLevel--;
		else if(newHeightLevel>heightLevel)
			heightLevel++;
	}
	
	
}
