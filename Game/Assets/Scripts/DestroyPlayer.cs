using UnityEngine;
using System.Collections;

public class DestroyPlayer : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DestroyMe()
	{
		Destroy (gameObject, 3);
	}
}
