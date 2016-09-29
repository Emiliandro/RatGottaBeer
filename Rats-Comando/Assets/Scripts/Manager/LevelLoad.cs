using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour {

	public GameObject gameManager;
	private int cont = 1;

	void Awake(){
		if(GameManager.instance == null && cont == 1){
			Instantiate(gameManager);
		}
	}

}
