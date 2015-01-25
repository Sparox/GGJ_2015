using UnityEngine;
using System.Collections;
using UnitySampleAssets._2D;
using System.Linq;
using System;
using System.Timers;


[RequireComponent(typeof (PlatformerCharacter2D))]
public class Character2DController : MonoBehaviour {

	private bool jump;
	public string actionKey;
	public string directionKey;
	public string suicideKey;
	private PlatformerCharacter2D character;
	private System.Random r = new System.Random();
	private float dieTimer = 0f;
	private bool mustSuicide = false;


	public ActionType type;

	private float direction = 0f;
	private float playerToFlyVerticalDirection = 0f;
	private float timerSuicide = 0f;

	private Transform playerToFly;
	private bool hasPlayerToFly = false;
	private Animator anim; // Reference to the player's animator component.

	private void Awake()
	{
		character = GetComponent<PlatformerCharacter2D> ();
		anim = GetComponent<Animator>();
	}


	// Update is called once per frame
	void Update () {

		if (hasPlayerToFly)
		{
			playerToFlyVerticalDirection = Input.GetAxisRaw(directionKey);
		}
		else
		{
			direction = Input.GetAxisRaw(directionKey);
		}

		if (Input.GetButton(suicideKey))
		{
			if(timerSuicide == 0f)
			{
				timerSuicide = Time.fixedTime;
			}

			if (Time.fixedTime - timerSuicide >= 3f)
			{
				mustSuicide = true;
				anim.SetBool("Diying", true);
				dieTimer = Time.fixedTime;
			}
		}

		if(mustSuicide && Time.fixedTime - dieTimer > 3f)
		{
			(GameObject.Find("GameManager").GetComponent("ManagerScript") as ManagerScript).PlayerDestroyed(actionKey);
			anim.SetBool("Diying", false);
			Destroy(this.gameObject);

		}

		if (Input.GetButtonUp(suicideKey))
		{
			timerSuicide = 0f;
		}

		if(Input.GetButtonDown(actionKey))
		{
			switch (type) {
			case ActionType.Jump:
				anim.SetBool("Jump", true);
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
				anim.SetBool("Magic", true);
				var player = GameObject.FindGameObjectsWithTag("Player")
					.Where(p => p.transform != this.transform)
					.OrderBy(p => Vector3.Distance(p.transform.position, this.transform.position)).FirstOrDefault();
				if (player != null)
				{
					(player.GetComponent("Character2DController") as Character2DController).SetFlying(true);
					hasPlayerToFly = true;
					playerToFly = player.transform;
				}
				break;
				default:
				break;
			}
		}

		if(type == ActionType.Sorcerer && Input.GetButtonUp(actionKey))
		{
			hasPlayerToFly = false;
			anim.SetBool("Magic", false);
			
			(playerToFly.GetComponent("Character2DController") as Character2DController).SetFlying(false);
		}
	}

	public void SetFlying(bool flying)
	{
		anim.SetBool ("Flying", flying);
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


		if(!this.renderer.isVisible)
		{
			(GameObject.Find("GameManager").GetComponent("ManagerScript") as ManagerScript).PlayerDestroyed(actionKey);
			Destroy(this.gameObject);
		}
	}
}
