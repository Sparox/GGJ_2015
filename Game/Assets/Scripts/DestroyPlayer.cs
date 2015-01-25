using UnityEngine;
using System.Collections;

public class DestroyPlayer : MonoBehaviour {
	
	private Animator anim; // Reference to the player's animator component.
	private bool mustSuicide = false;
	private float dieTimer = 0f;
	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if(mustSuicide && Time.fixedTime - dieTimer > 3f)
		{
			anim.SetBool("Diying", false);
			Destroy (gameObject);
		}
	}

	public void DestroyMe()
	{
		mustSuicide = true;
		dieTimer = Time.fixedTime;
	}
}
