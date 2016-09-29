using UnityEngine;
using System.Collections;

public class Player : MovingObject {

	public int horizontal;
	public int vertical;
	public float restartLevelDelay = 1.5f;

	public int lifePoints; //Quantidade de pontos.

	public int playerID;

	private string horizontalKey;
	private string verticalKey;

	private Animator animatorPlayer; //Animator do Player

	protected override void Start (){
		horizontalKey = "Horizontal";
		verticalKey = "Vertical";
		animatorPlayer = GetComponent<Animator>();
		lifePoints = GameManager.instance.playerLifePoints;
		base.Start ();
	}

	public void OnDisable(){
		GameManager.instance.playerLifePoints = lifePoints;
	}

	protected override void AttemptMove<T> (int xDir, int yDir){
		base.AttemptMove<T> (xDir, yDir);
		RaycastHit2D hit;
	}

	private void OnTriggerEnter2D (Collider2D other){
		if(other.CompareTag("Saida")){
			Invoke("Restart", restartLevelDelay);
		}else if(other.CompareTag("Beer")){
			
		}
	}

	public void LoseLife(int loss){
		lifePoints -= loss;
		CheckGameOver();
	}

	private void CheckGameOver(){
		if(lifePoints <= 0){
			GameManager.instance.GameOver();
		}
	}

	protected override void OnCantMove<T> (T Component)
	{
		Wall wallCollision = Component as Wall;
		animatorPlayer.SetTrigger("WallColision");
		Debug.Log("Não Pode andar");
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.instance.isGameRoad){
			return;
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			horizontal = -1;
		}else if(Input.GetKeyDown(KeyCode.RightArrow)){
			horizontal = 1;
		}else if(Input.GetKeyDown(KeyCode.UpArrow)){
			vertical = 1;
		}else if(Input.GetKeyDown(KeyCode.DownArrow)){
			vertical = -1;
		}

		if(horizontal != 0){
			vertical = 0;
		}

		if(horizontal != 0 || vertical != 0){
			AttemptMove<Wall>(horizontal, vertical);
			GameManager.instance.MoveEnemies();
			horizontal = 0;
			vertical = 0;
		}


	
	}
}
