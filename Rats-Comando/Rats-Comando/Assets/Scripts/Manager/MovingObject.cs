using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {

    public float moveTime = 100.50f; //tempo para se mover emsegundos
	public LayerMask BlockingLayer; //layer para checar colisao

    private BoxCollider2D boxCollider2D; //Box collider anexado ao objeto
	private Rigidbody2D rbd2D; //rigdbody anexado ao objeto
	private float inverseMoveTime; 
	// Use this for initialization

	//Protected Virtual permite ser substiuidos pelas classes que herdarem
	protected virtual void Start () {
		boxCollider2D = GetComponent<BoxCollider2D>();
		rbd2D = GetComponent<Rigidbody2D>();
		inverseMoveTime = 1f / moveTime;
	}

	protected bool Move (int xDir,int yDir, out RaycastHit2D hit)
    {
		//Ira retorna verdadeiro caso o objeto possa se mover
		//Parametros direcao x e y, alem do hit para poder checar a colisao
		Vector2 Start = transform.position; //Pegando a posicao
		Vector2 end = Start + new Vector2(xDir,yDir);
		boxCollider2D.enabled = false;
		hit = Physics2D.Linecast(Start,end, BlockingLayer);

        boxCollider2D.enabled = true;

		if(hit.transform == null){
			StartCoroutine (SmoothMovement(end));
			return true;
		}
		//Acertou alguma coisa
		return false;

	}

	protected IEnumerator SmoothMovement ( Vector3 end){
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		while(sqrRemainingDistance > float.Epsilon){
			Vector3 newPositon = Vector3.MoveTowards(rbd2D.position,end,inverseMoveTime*Time.deltaTime);
			rbd2D.MovePosition(newPositon);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	protected virtual void AttemptMove <T> (int xDir, int yDir)
		where T:Component
	{
		RaycastHit2D hit;
        RaycastHit2D hit2;
        bool canMove = Move(xDir, yDir, out hit);
		if(hit.transform == null)
			return;
		T hitComponent = hit.transform.GetComponent <T>();
		if(!canMove && hitComponent != null){
			OnCantMove(hitComponent);
		}
	} 

	protected abstract void OnCantMove <T> (T Component)
		where T: Component;

}
