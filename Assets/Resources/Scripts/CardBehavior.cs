using UnityEngine;
using System.Collections;

public class CardBehavior : MonoBehaviour {

	public AbstractCard properties;
	public PlayerBehavior player;
	public int position = 0;	//Used to determine where the normal position should go

	float x = 0f;
	float y = 0f;
	float rotation = 0f;
	float default_y = -3.75f;
	float hover_y = -3.25f;
	float selected_x = 0;
	float selected_y = -1.5f;

	public bool selected = false;
	

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, this.transform.position.z), 0.1f);

	}

	public void SetData(PlayerBehavior bc)
	{
		player = bc;
		SpriteRenderer img = (SpriteRenderer)transform.Find("Card Picture").GetComponent("SpriteRenderer");
		img.sprite = properties.card_image;
		TextMesh txt = (TextMesh)transform.Find ("Card Text").GetComponent("TextMesh");
		txt.text = properties.card_description;
		txt = (TextMesh)transform.Find ("Card Name").GetComponent("TextMesh");
		txt.text = properties.card_name;
	}


	public void SetPosition()
	{
		float num_cards = player.hand_size;
		float max_dist = 8.25f;
		float spacing = Mathf.Min(1.7f, max_dist / num_cards);
		this.x = position * spacing - (num_cards - 1) * spacing / 2.0f;
		this.y = default_y;
	}

	public void SetLayer(float layer)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, layer);
	}

	void OnMouseDown() {
		player.SetSelected(gameObject);
	}

	void OnMouseOver() {
		if (!selected)
		{
			SetLayer (-3.0f);
			y = hover_y;
		}
	}

	void OnMouseExit() {
		if (!selected)
		{
			SetLayer ((float)position);
			y = default_y;
		}
	}

	public void Select() {
		selected = true;
		x = selected_x;
		y = selected_y;
		SetLayer (-2.5f);
		properties.OnSelect();
		properties.SetParent(this);

	}

	public void Deselect() {
		selected = false;
		SetPosition();
		properties.OnDeselect();
	}


	public void PlayCard()
	{
		player.RemoveCard(gameObject);

	}
}
