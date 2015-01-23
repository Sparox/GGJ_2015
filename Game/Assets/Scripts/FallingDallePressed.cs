using UnityEngine;
using System.Collections;

public class FallingDallePressed : MonoBehaviour {

	public Transform FallingGround;
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			FallingGround.rigidbody2D.isKinematic = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
