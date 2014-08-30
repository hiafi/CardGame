using UnityEngine;
using System.Collections;

public class ProjectileBehavior : MonoBehaviour {
	
	public Vector3 start;
	public Vector3 destination;
	public float speed = 0.1f;
	public float pos = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (destination != null)
		{
			transform.position = Vector3.Lerp(start, destination, pos);
			pos += speed;
		}
		if (pos >= 1.0f)
		{
			Debug.Log ("Animation End");
			EventManager.Instance.OnCardAnimationEndEventCall();
		}

	}

	public void SetDestination(Vector3 destination)
	{
		start = this.transform.position;
		this.destination = destination;
	}
}
