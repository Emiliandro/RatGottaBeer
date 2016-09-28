using UnityEngine;
using System.Collections;

public class Player : MovingObject {

	public int lifePoints; //Quantidade de pontos.

	private Animator animatorPlayer; //Animator do Player

	protected override void Start ()
	{
		animatorPlayer = GetComponent<Animator>();
		base.Start ();
	}

	protected override void AttemptMove<T> (int xDir, int yDir){
		base.AttemptMove (xDir, yDir);
		RaycastHit2D hit;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
