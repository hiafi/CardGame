using UnityEngine;
using System.Collections;

public class FireballBehavior : AbstractCard {

	public float base_damage = 10;
	public float gear_scaling = 1;

	private GameObject fireball;
	private GameObject target;

	public override void OnSelect()
	{
		EventManager.Instance.OnTargetSelect += PlayCard;
	}

	public override void PlayCard (GameObject g)
	{
		parent.PlayCard();
		target = g;
		GameObject fb_prefab = Resources.Load<GameObject>("Prefabs/SpellEffects/Fireball");
		fireball = (GameObject)GameObject.Instantiate(fb_prefab);
		fireball.transform.position = new Vector3(0, -6.0f, 0);
		ProjectileBehavior pb = (ProjectileBehavior)fireball.GetComponent("ProjectileBehavior");
		pb.SetDestination(g.transform.position);
		EventManager.Instance.OnCardAnimationEnd += AnimationEnd;

	}

	public void AnimationEnd()
	{
		Vector3 pos = fireball.transform.position;
		Destroy(fireball);

		GameObject fb_prefab = Resources.Load<GameObject>("Prefabs/SpellEffects/Explosion");

		fireball = (GameObject)GameObject.Instantiate(fb_prefab);
		fireball.transform.position = pos;
		Debug.Log(target);
		Status status = (Status)target.GetComponent("Status");
		status.DealDamage((int)base_damage);

	}
	
}
