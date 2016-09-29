using UnityEngine;
using System.Collections;
using System;

public enum Intelligence{
	low,
	high
}

public class Enemy : MovingObject {

	public int playerDamager;

	public Intelligence intelligenceEnemy;

	private Animator animator;
	private Transform target;

	// Use this for initialization
	protected override void Start (){
		GameManager.instance.addEnemy(this);

		animator = GetComponent<Animator>();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		base.Start ();
	}

	protected override void AttemptMove<T> (int xDir, int yDir){
		base.AttemptMove<T>(xDir, yDir);
	}

	public void MoveEnemy(){
		if(intelligenceEnemy.Equals(Intelligence.low)){
			Debug.Log("Inteligencia baixa");
			Invoke("BasicMove",0.1f);
		}else{
			Debug.Log("Inteligencia alta");
			Invoke("PathMove",0.1f);
		}
		
	}

	private void BasicMove(){
		int xDir = 0;
		int yDir = 0;

		if(Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon){
			//Mesma coluna, anda pela linha
			//Verifica se as posicoes em y entre os dois eh satisfatoria se sim recebe 1, se nao recebe -1
			yDir = target.position.y > transform.position.y ? 1 : -1;
		}else {
			//Nao esta na mesma coluna, entao ele muda a coluna
			//Verifica se as posicoes em x entre os dois e satisfatoria, se sim recebe 1, se nao recebe -1
			xDir = target.position.x > transform.position.x ? 1 : -1;
		}
		AttemptMove<Player>(xDir,yDir);

	}

	private void PathMove(){
		bool inListOk = false;
		int currentX = Mathf.RoundToInt(transform.position.x);
		int currentY = Mathf.RoundToInt(transform.position.y);
		GetComponent<BoxCollider2D>().enabled = false;
		target.GetComponent<BoxCollider2D>().enabled = false;

		AStar astar = new AStar(new LineCastAStarCost(BlockingLayer),currentX,currentY,
			Mathf.RoundToInt(target.position.x),Mathf.RoundToInt(target.position.y));
		astar.findPath();
		GetComponent<BoxCollider2D>().enabled = true;
		target.GetComponent<BoxCollider2D>().enabled = true;
		AStarNode2D nextStep = (AStarNode2D)astar.solution[1];
		int xDir = nextStep.x - currentX;
		int yDir = nextStep.y - currentY;
		AttemptMove<Player>(xDir,yDir);

	}


	protected override void OnCantMove<T> (T Component){
		Player hitPlayer = Component as Player;
		hitPlayer.LoseLife(playerDamager);
	}




}
