using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count{
		//Construtor para definir os maximos e minimos.
		public int minimun, maximun;
		public Count(int min, int max){
			minimun = min;
			maximun = max;
		}
	}

	public int rows; //Linhas.
	public int collumns; //Colunas.
	public Count wallCount = new Count(3,6); //Muros internos.
	public GameObject exitObject; //Objeto da saida.
	public GameObject playerRef; //Referencia do Player.
	public GameObject[] floorTiles; //Array dos tiles do piso.
	public GameObject[] wallTiles; // Array dos tiles dos muros.
	public GameObject[] outWallTiles; //Array dos tiles dos muros externos.
	public GameObject[] enemyArray; //Array dos inimigos.
	public GameObject[] itensArray; //Array de itens.
	public Count itensCount = new Count(2,5);

	private Transform boardHolder;
	private List<Vector3> gridPosition = new List<Vector3>(); //Lista de posições para os tiles.

	public Vector2[] positionFree;

	public bool arrayContains(Vector2 posRef, Vector2[] array){
		for(int i = 0; i < array.Length; i++){
			if(array[i] == posRef){
				return true;
			}
		}
		return false;
	}

	private void initialiseList(){
		//Limpa a lista de posições e a prepara para gerar um novo tabuleiro.
		gridPosition.Clear();
		for(int x = 0; x < collumns - 1; x++){
			for(int y = 0; y < rows - 1; y++){
				Vector2 posGeration = new Vector2(x,y);
				if(arrayContains(posGeration,positionFree)){
					Debug.Log("Contain o elemento");
				}else{
					gridPosition.Add(new Vector3(x,y,0));
				}
			}
		}
	}

	private void BoardSetup(){
		//Inicia o tabuleiro e atribui o seu transform.
		boardHolder = new GameObject("Board").transform;

		for(int x = -1; x <= collumns; x++){
			for(int y = -1; y <= rows; y++){
				
				//Pega o tile de um fundo aleatorio.
				GameObject toInstantiate = floorTiles[Random.Range(0,floorTiles.Length)];

				//Verifica se eh um muro externo.
				if((x == -1) || (y == -1) || (x == collumns) || (y == rows));
					toInstantiate = floorTiles[Random.Range(0,outWallTiles.Length)];

				GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);

			}
		}

	}

	private Vector3 RandomPosition(){
		//Pega uma posicao aleatoria.
		int randomIndex = Random.Range(0, gridPosition.Count);
		Vector3 randomPosition = gridPosition[randomIndex];
		gridPosition.RemoveAt(randomIndex);
		return randomPosition;
	}

	private void LayoutObjectAtRandom(GameObject[] tileArray, int minimun, int maximun){
		int objectCount = Random.Range(minimun, maximun);
		for(int i = 0; i < objectCount; i++){
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoise = tileArray[Random.Range(0, tileArray.Length)];
			Instantiate(tileChoise, randomPosition, Quaternion.identity);
		}
	}

	public void SetupScene(int level){
		if(level > 0){
			//Coloca os quadrados no tabuleiro.
			BoardSetup();
			//Inicia o Grid.
			initialiseList();
			//Instancia aleatoriamente os muros internos.
			LayoutObjectAtRandom(wallTiles, wallCount.minimun, wallCount.maximun);
			//Instancia aleatoriamente os itens.
			LayoutObjectAtRandom(itensArray, itensCount.minimun, itensCount.maximun);
			//Instancia o numero de inimigos conforme o level.
			int enemyCount = (int)Mathf.Log(level,2f);
			//Instancia a saida.
			Instantiate(exitObject, new Vector3(collumns-1, rows-1,0),Quaternion.identity);
			//Instancia o player.
			Instantiate(playerRef, new Vector3(1,0,0), Quaternion.identity);
		}
	}

}
