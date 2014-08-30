using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehavior : MonoBehaviour {

	List<GameObject> hand;
	List<string> draw_pile;
	List<string> discard_pile;

	BattleControl controller;

	public int player_number = 1;

	public int ap;

	// Use this for initialization
	void Start () {

	}

	public void Init(BattleControl controller)
	{
		hand = new List<GameObject>();
		draw_pile = new List<string>();
		discard_pile = new List<string>();
		for (int i=0; i<30; i++)
		{
			draw_pile.Add("Fireball");
		}
		for (int i=0; i<12; i++)
		{
			DrawCard ();
		}
		OrganizeCards();

	}

	// Update is called once per frame
	void Update () {
		Status status = (Status)GetComponent("Status");
		SpriteRenderer img = (SpriteRenderer)transform.Find("HP Bar").GetComponent("SpriteRenderer");
		float amount = ((float)status.hp) / ((float)status.max_hp);
		img.transform.localScale = Vector3.Lerp(img.transform.localScale, new Vector3(amount, img.transform.localScale.y, img.transform.localScale.z), 0.1f);
	}

	void DrawCard() {
		string card = draw_pile[0];
		draw_pile.RemoveAt(0);
		GameObject card_prefab = Resources.Load<GameObject>("Prefabs/Card");
		GameObject prop_prefab = Resources.Load<GameObject>(string.Format("Prefabs/Cards/{0}", card));
		GameObject card_obj = (GameObject)GameObject.Instantiate(card_prefab);

		CardBehavior cb = (CardBehavior)card_obj.GetComponent("CardBehavior");

		//GameObject prop_obj = (GameObject)GameObject.Instantiate(prop_prefab);
		AbstractCard properties = (AbstractCard)prop_prefab.GetComponent("AbstractCard");
		cb.properties = properties;
		cb.SetData(this);
		hand.Add(card_obj);
	}

	void OrganizeCards()
	{
		for (int i=0; i<hand.Count; i++)
		{
			CardBehavior cb = GetBehav(hand[i]);
			cb.position = i;
			cb.SetLayer(i);
			cb.SetPosition();
		}
	}

	public void SetSelected(GameObject card)
	{
		CardBehavior selected = null;
		GameObject obj = null;
		foreach (GameObject o in hand)
		{
			CardBehavior b = GetBehav(o);
			if (b.selected)
			{
				selected = b;
			}
			if (o == card)
			{
				obj = o;
			}
		}


		CardBehavior cb = GetBehav(obj);
		if (cb.selected)
		{
			cb.Deselect();
		}
		else
		{
			if (selected != null)
			{
				selected.Deselect();
			}
			cb.Select();
		}
	}

	public void RemoveCard(GameObject card)
	{
		GameObject obj = hand.Find(o => o == card);
		hand.Remove (obj);
		Destroy(obj);
		OrganizeCards();
	}

	private CardBehavior GetBehav(GameObject obj)
	{
		return (CardBehavior)obj.GetComponent("CardBehavior");
	}
	


	//Properties
	public int hand_size {
		get { return hand.Count; }
	}
}
