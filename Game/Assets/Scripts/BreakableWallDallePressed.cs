using UnityEngine;
using System.Collections;

public class BreakableWallDallePressed : MonoBehaviour {

	public Transform BreakableWall;
	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		BreakableWall.renderer.enabled = false;
		(BreakableWall.GetComponent ("BoxCollider2D") as BoxCollider2D).isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			if(BreakableWall != null)
			{
				BreakableWall.renderer.enabled = true;
				(BreakableWall.GetComponent ("BoxCollider2D") as BoxCollider2D).isTrigger = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
