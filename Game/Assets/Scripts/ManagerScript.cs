using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
}

public class ManagerScript : MonoBehaviour {

	private List<PlayerControl> playerControlList = new List<PlayerControl> ();

	public Transform Spawner;
	public Transform PlayerPrefab;
	// Use this for initialization
	void Start () {
		playerControlList.Add (new PlayerControl (){
			ActionKey = KeyCode.Z,
			LeftKey = KeyCode.A,
			RightKey = KeyCode.E,
			IsUsed = false
		});
		playerControlList.Add (new PlayerControl (){
			ActionKey = KeyCode.I,
			LeftKey = KeyCode.U,
			RightKey = KeyCode.O,
			IsUsed = false
		});
		playerControlList.Add (new PlayerControl (){
			ActionKey = KeyCode.UpArrow,
			LeftKey = KeyCode.LeftArrow,
			RightKey = KeyCode.RightArrow,
			IsUsed = false
		});
		playerControlList.Add (new PlayerControl (){
			ActionKey = KeyCode.V,
			LeftKey = KeyCode.C,
			RightKey = KeyCode.B,
			IsUsed = false
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
			playerControlList.First(p => !p.IsUsed).IsUsed = true;
		}
	}
}
