using UnityEngine;
using System.Collections;

public class LastWallDallePressed : MonoBehaviour {

	public Transform lastWall;
	private Collider2D playerCollider;
	private Vector3 lastPlayerPosition = Vector3.zero;
	private Vector3 initialLastWallPosition;

	// Use this for initialization
	void Start () {
		
	}

	void Awake()
	{
		initialLastWallPosition = lastWall.position;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			playerCollider = collider;
			lastPlayerPosition = playerCollider.transform.position;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (playerCollider != null && lastPlayerPosition != Vector3.zero &&  Vector3.Distance( playerCollider.transform.position, lastPlayerPosition) >= 1f ) {
			//lastWall.position = initialLastWallPosition;

			if(lastWall.position.y < initialLastWallPosition.y)
			{
				lastWall.position = new Vector3(lastWall.position.x, lastWall.position.y +0.03f, lastWall.position.z);
			}
			else
			{
				playerCollider = null;
				lastPlayerPosition = Vector3.zero;
			}
		}
		else if(playerCollider != null && lastPlayerPosition != Vector3.zero && lastWall.position.y > initialLastWallPosition.y -2.3f)
		{
			lastWall.position = new Vector3(lastWall.position.x, lastWall.position.y -0.03f, lastWall.position.z);
		}
	}


}
