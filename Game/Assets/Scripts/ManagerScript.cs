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
	public string ActionKey {
				get;
				set;
		}
	public string DirectionKey {
				get;
				set;
		}
	public string SuicideKey {
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
			DirectionKey = "LeftRightController1",
			ActionKey = "ActionController1",
			SuicideKey = "StartController1",
			IsUsed = false,
			Number = 0
		});
		playerControlList.Add (new PlayerControl (){
			DirectionKey = "LeftRightController2",
			ActionKey = "ActionController2",
			SuicideKey = "StartController2",
			IsUsed = false,
			Number = 1
		});
		playerControlList.Add (new PlayerControl (){
			DirectionKey = "LeftRightController3",
			ActionKey = "ActionController3",
			SuicideKey = "StartController3",
			IsUsed = false,
			Number = 2
		});
		playerControlList.Add (new PlayerControl (){
			DirectionKey = "LeftRightController4",
			ActionKey = "ActionController4",
			SuicideKey = "StartController4",
			IsUsed = false,
			Number = 3
		});
	}
	
	// Update is called once per frame
	void Update () {

		int controllerNumber = -1;

		if (Input.GetButtonDown ("StartController1")) 
		{
			controllerNumber = 0;
			Debug.Log("de meeeeeeeeeerd");
		}
		else if (Input.GetButtonDown ("StartController2"))
			controllerNumber = 1;
		else if (Input.GetButtonDown ("StartController3"))
			controllerNumber = 2;
		else if (Input.GetButtonDown ("StartController4"))
			controllerNumber = 3;
		if(controllerNumber != -1)
			Debug.Log (controllerNumber);

		if (controllerNumber != -1 && playerControlList.Any(p => p.Number == controllerNumber && !p.IsUsed)) 
		{
			var newPlayerControl = playerControlList.First(p => p.Number == controllerNumber);
			var newPlayer = Instantiate(PlayerPrefab, Spawner.position, Quaternion.identity);
			var scriptPlayerController = (newPlayer as Transform).GetComponent("Character2DController") as Character2DController;
			scriptPlayerController.actionKey = newPlayerControl.ActionKey;
			scriptPlayerController.directionKey = newPlayerControl.DirectionKey;
			scriptPlayerController.suicideKey = newPlayerControl.SuicideKey;

			var restingType = actionTypeList.Where(a => !a.InUse).ToList();
			scriptPlayerController.type = restingType[r.Next (restingType.Count)].Type;
			playerControlList.First(p => p.Number == controllerNumber).destroyScript = (newPlayer as Transform).GetComponent("DestroyPlayer") as DestroyPlayer;
			actionTypeList.First(a => a.Type == scriptPlayerController.type).InUse = true;
			playerControlList.First(p => p.Number == controllerNumber).type = scriptPlayerController.type;
			playerControlList.First(p => p.Number == controllerNumber).IsUsed = true;

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

	public void PlayerDestroyed(string actionKey)
	{
		var type = playerControlList.First (p => p.ActionKey == actionKey).type;
		playerControlList.First (p => p.ActionKey == actionKey).IsUsed = false;
		actionTypeList.First (a => a.Type == type).InUse = false;
	}
}
