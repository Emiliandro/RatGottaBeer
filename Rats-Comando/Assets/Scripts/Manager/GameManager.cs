using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private BoardManager boardScript;
	public int level = 0;

	public float turnDelay = 0.1f;
	public int playerLifePoints;

	public bool isGameRoad;

	public float levelStartDelay = 2f;
	private bool doingSetup = false;

	public Text levelOverGamer;
	private GameObject panelGameOver;

	private GameObject MainCamera;

	private List<Enemy> enemies = new List<Enemy>();

	void Awake(){
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);

		boardScript = GetComponent<BoardManager>();
		MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		isGameRoad = false;

		//Chama a inicializacao do primeiro level
		InitGame();

	}

	void InitGame(){
		MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

		doingSetup = true; //Impedir de o jogador de jogar enquanto a fase eh montada

		Debug.Log(level);
		boardScript.SetupScene(level);

		isGameRoad = true;


	}

	public void addEnemy(Enemy enemy){
		enemies.Add(enemy);
	}

	public void MoveEnemies(){
		for(int i = 0; i < enemies.Count; i++){
			enemies[i].MoveEnemy();
		}
	}


	public void GameOver(){
		panelGameOver.SetActive(true);
		levelOverGamer = GameObject.Find("LevelOverGamer").GetComponent<Text>();
		string mensagerOver = "" + level;
		levelOverGamer.text = mensagerOver;

		//Desativa o GameManager.
		enabled = false;

	}
}
