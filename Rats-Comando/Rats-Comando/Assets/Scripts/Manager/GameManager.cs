using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private BoardManager boardScript;
	public int level = 0;

	public float turnDelay = 0.1f;
	public int playerLifePoints;

	public bool isGameRoad;

	public float levelStartDelay = 2f;
	private bool doingSetup = false;

	private GameObject MainCamera;

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
}
