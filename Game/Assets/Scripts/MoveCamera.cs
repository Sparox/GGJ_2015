using UnityEngine;
using System.Collections;
using System.Linq;

public class MoveCamera : MonoBehaviour {

	private float minPosX = -3.98f;
	private float maxPosX = 8.03f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var players = GameObject.FindGameObjectsWithTag ("Player");
		Vector3 newPosition = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		if (players.Count() > 0)
		{
			newPosition.x = players.Average (p => p.transform.position.x);
			if (newPosition.x > maxPosX)
				newPosition.x = maxPosX;

			if(newPosition.x < minPosX)
				newPosition.x = minPosX;
		}
		else
		{
			newPosition.x = minPosX;
		}
		this.transform.position = newPosition;
	}
}
