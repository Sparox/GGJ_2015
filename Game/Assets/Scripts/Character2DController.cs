using UnityEngine;
using System.Collections;
using UnitySampleAssets._2D;
using System.Linq;
using System;


[RequireComponent(typeof (PlatformerCharacter2D))]
public class Character2DController : MonoBehaviour {

	private bool jump;
	public KeyCode actionKey;
	public KeyCode leftKey;
	public KeyCode rightKey;
	private PlatformerCharacter2D character;
	public Transform BlocPrefab;
	private bool canCreateBlock = true;
	private System.Random r = new System.Random();
	private enum actionType
	{
		Jump,
		BrokingWall,
		//Actionner,
		//Killer,
		//Sorcerer
	};

	private actionType type;

	private float direction = 0f;

	private void Awake()
	{
		var values = Enum.GetValues (typeof(actionType));
		character = GetComponent<PlatformerCharacter2D> ();
		type = (actionType)values.GetValue (r.Next (values.Length));
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetKey(leftKey))
		{
			direction = -1f;
		}

		if (Input.GetKey(rightKey))
		{
			direction = 1f;
		}


		if (!Input.GetKey(leftKey) && !Input.GetKey(rightKey))
		{
			direction = 0f;
		}

		if (Input.GetKey(leftKey) && Input.GetKey(rightKey))
		{
			direction = 0f;
		}

		if(Input.GetKeyDown(actionKey))
		{
			switch (type) {
			case actionType.Jump:
				jump = true;
				break;
			case actionType.BrokingWall:
				var objectToBroke = GameObject.FindGameObjectsWithTag("Breakable")
					.FirstOrDefault(g => Vector3.Distance(g.transform.position, this.transform.position) <= 1);
				if (objectToBroke != null)
				{
					Destroy(objectToBroke);
				}
				break;
				default:
				break;
			}
		}
	}

	private void GenerateCube()
	{
		var posNewBloc = new Vector3 (this.transform.position.x, this.transform.localPosition.y - 2, this.transform.position.z);
		//var newBloc = Instantiate (BlocPrefab, posNewBloc, Quaternion.identity);
	}

	private void FixedUpdate()
	{
		if(!character.IsGrounded() && direction != 0f && this.transform.rigidbody2D.velocity.x == 0f)
		{
			direction  = 0f;
		}
		// Read the inputs.
		float h = direction;
		// Pass all parameters to the character control script.
		character.Move(h, false, jump);

		jump = false;
		/*if(!canCreateBlock && character.IsGrounded())
		{
			canCreateBlock = true;
		}*/
	}
}
