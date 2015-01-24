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
	private System.Random r = new System.Random();


	public ActionType type;

	private float direction = 0f;
	private float playerToFlyVerticalDirection = 0f;

	private Transform playerToFly;
	private bool hasPlayerToFly = false;
	private void Awake()
	{
		character = GetComponent<PlatformerCharacter2D> ();
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetKey(leftKey))
		{
			if (hasPlayerToFly)
			{
				playerToFlyVerticalDirection = -1f;
			}
			else
			{
				direction = -1f;
			}
		}

		if (Input.GetKey(rightKey))
		{
			if (hasPlayerToFly)
			{
				playerToFlyVerticalDirection = 1f;
			}
			else
			{
				direction = 1f;
			}
		}

		if (Input.GetKey(leftKey) == Input.GetKey(rightKey))
		{
			if (hasPlayerToFly)
			{
				playerToFlyVerticalDirection = 0f;
			}
			else
			{
				direction = 0f;
			}
		}

		if(Input.GetKeyDown(actionKey))
		{
			switch (type) {
			case ActionType.Jump:
				jump = true;
				break;
			case ActionType.BrokingWall:
				var objectToBroke = GameObject.FindGameObjectsWithTag("Breakable")
					.FirstOrDefault(g => Vector3.Distance(g.transform.position, this.transform.position) <= 1.8f);
				if (objectToBroke != null && objectToBroke.renderer.enabled)
				{
					Destroy(objectToBroke);
				}
				break;
			case ActionType.Switcher:
				var switchToActivate = GameObject.FindGameObjectsWithTag("Switch")
					.FirstOrDefault(g => Vector3.Distance(g.transform.position, this.transform.position) <= 1.8f);
				if (switchToActivate != null && switchToActivate.name == "SwitchBridge")
				{
					switchToActivate.rigidbody2D.isKinematic = false;
					(GameObject.Find("BridgeStop").GetComponent("BoxCollider2D") as BoxCollider2D).isTrigger = false;
					GameObject.Find("Bridge").rigidbody2D.isKinematic = false;
				}
				break;
			case ActionType.Sorcerer:
				var player = GameObject.FindGameObjectsWithTag("Player")
					.Where(p => p.transform != this.transform)
					.OrderBy(p => Vector3.Distance(p.transform.position, this.transform.position)).FirstOrDefault();
				if (player != null)
				{
					hasPlayerToFly = true;
					playerToFly = player.transform;
				}
				break;
				default:
				break;
			}
		}

		if(type == ActionType.Sorcerer && Input.GetKeyUp(actionKey))
		{
			hasPlayerToFly = false;
		}
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
		if(hasPlayerToFly && playerToFly != null)
			(playerToFly.GetComponent ("PlatformerCharacter2D") as PlatformerCharacter2D).MoveVerticaly (playerToFlyVerticalDirection);
		jump = false;
	}
}
