using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public enum ActionType
{
	Jump,
	BrokingWall,
	Switcher,
	//Killer,
	Sorcerer
};

public class ActionTypeInUse
{
	public ActionType Type {
				get;
				set;
	}
	public bool InUse {
				get;
				set;
		}
}

public class PlayerControl
{
	public KeyCode ActionKey {
				get;
				set;
	}
	public KeyCode LeftKey {
				get;
				set;
	}
	public KeyCode RightKey {
				get;
				set;
	}

	public bool IsUsed {
				get;
				set;
	}
	public int Number {
				get;
				set;
	}
	public DestroyPlayer destroyScript {
				get;
				set;
		}
	public ActionType type {
				get;
				set;
		}
}

public class ManagerScript : MonoBehaviour {

	private List<PlayerControl> playerControlList = new List<PlayerControl> ();

	public Transform Spawner;
	public Transform PlayerPrefab;
	private int nextToKill = 0;
	private List<ActionTypeInUse> actionTypeList = new List<ActionTypeInUse> ();
	private System.Random r = new System.Random();
	// Use this for initialization
	void Start () {
		foreach(var type in Enum.GetValues (typeof(ActionType)))
		{
			actionTypeList.Add(new ActionTypeInUse()
           	{
				Type = (ActionType)type,
				InUse = false
			});
		}
		playerControlList.Add (new PlayerControl (){
			ActionKey = KeyCode.Z,
			LeftKey = KeyCode.A,
			RightKey = KeyCode.E,
			IsUsed = false,
			Number = 0
		});
		playerControlList.Add (new PlayerControl (){
			ActionKey = KeyCode.I,
			LeftKey = KeyCode.U,
			RightKey = KeyCode.O,
			IsUsed = false,
			Number = 1
		});
		playerControlList.Add (new PlayerControl (){
			ActionKey = KeyCode.UpArrow,
			LeftKey = KeyCode.LeftArrow,
			RightKey = KeyCode.RightArrow,
			IsUsed = false,
			Number = 2
		});
		playerControlList.Add (new PlayerControl (){
			ActionKey = KeyCode.V,
			LeftKey = KeyCode.C,
			RightKey = KeyCode.B,
			IsUsed = false,
			Number = 3
		});
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && playerControlList.Any(p => !p.IsUsed)) 
		{
			var newPlayerControl = playerControlList.First(p => !p.IsUsed);
			var newPlayer = Instantiate(PlayerPrefab, Spawner.position, Quaternion.identity);
			var scriptPlayerController = (newPlayer as Transform).GetComponent("Character2DController") as Character2DController;
			scriptPlayerController.actionKey = newPlayerControl.ActionKey;
			scriptPlayerController.leftKey = newPlayerControl.LeftKey;
			scriptPlayerController.rightKey = newPlayerControl.RightKey;

			var restingType = actionTypeList.Where(a => !a.InUse).ToList();
			scriptPlayerController.type = restingType[r.Next (restingType.Count)].Type;
			playerControlList.First(p => !p.IsUsed).destroyScript = (newPlayer as Transform).GetComponent("DestroyPlayer") as DestroyPlayer;
			actionTypeList.First(a => a.Type == scriptPlayerController.type).InUse = true;
			playerControlList.First(p => !p.IsUsed).type = scriptPlayerController.type;
			playerControlList.First(p => !p.IsUsed).IsUsed = true;

			if (playerControlList.Count(p => p.IsUsed) == 4)
			{
				actionTypeList.First(a => a.Type == playerControlList.First(p => p.Number == nextToKill).type).InUse = false;
				playerControlList.First(p => p.Number == nextToKill).IsUsed = false;
				playerControlList.First(p => p.Number == nextToKill).destroyScript.DestroyMe();

				nextToKill++;
				if(nextToKill == 4)
					nextToKill = 0;
			}
		}
	}
}
