using UnityEngine;
using System.Collections;

public class BattleControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Status status;
		for (int i=1; i<=4; i++)
		{
			GameObject player_prefab = Resources.Load<GameObject>("Prefabs/Player");
			GameObject player_obj = (GameObject)GameObject.Instantiate(player_prefab);
			PlayerBehavior player = (PlayerBehavior)player_obj.GetComponent("PlayerBehavior");
			status = (Status)player_obj.GetComponent("Status");
			status.controller = this;
			player.player_number = i;
			if (i == 1)
			{
				player.Init(this);
			}
			SetPlayerPosition (player_obj, i);
		}

		GameObject boss_prefab = Resources.Load<GameObject>("Prefabs/Boss");
		GameObject boss_obj = (GameObject)GameObject.Instantiate(boss_prefab);
		status = (Status)boss_obj.GetComponent("Status");
		status.controller = this;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void SetSelected(Status s)
	{

	}

	private void SetPlayerPosition(GameObject player, int number)
	{
		float x = 0;
		float y = 0;
		switch (number)
		{
		case 1:
			x = -6.75f;
			y = -3.0f;
			break;

		case 2:
			x = -6.75f;
			y = -4.35f;
			break;

		case 3:
			x = 6.75f;
			y = -3.0f;
			break;

		case 4:
			x = 6.75f;
			y = -4.35f;
			break;
		}

		player.transform.position = new Vector3(x, y, player.transform.position.z);
	}
	

}
