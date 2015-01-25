using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {

	public Transform camera;
	public float ratio;
	private Vector3 initialCameraPosition;
	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		initialCameraPosition = camera.position;

	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position = new Vector3 (camera.position.x * ratio, this.transform.position.y, this.transform.position.z);
	
	}
}
