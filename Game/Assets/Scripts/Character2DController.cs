using UnityEngine;
using System.Collections;
using UnitySampleAssets._2D;


[RequireComponent(typeof (PlatformerCharacter2D))]
public class Character2DController : MonoBehaviour {

	private bool jump;
	public KeyCode actionKey;
	public KeyCode leftKey;
	public KeyCode rightKey;
	private PlatformerCharacter2D character;
	public Transform BlocPrefab;
	private bool canCreateBlock = true;

	private enum actionType
	{
		Jump
	};

	private actionType type;

	private float direction = 0f;

	private void Awake()
	{
		character = GetComponent<PlatformerCharacter2D> ();
		type = actionType.Jump;
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
