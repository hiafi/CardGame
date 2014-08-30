using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

	public int hp = 100;
	public int max_hp = 100;

	public bool summon = false;

	public BattleControl controller;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown() {
		EventManager.Instance.OnTargetSelectEventCall(gameObject);
	}

	public void DealDamage(int amount)
	{
		hp -= amount;
	}

}
