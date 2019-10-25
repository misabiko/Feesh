using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FishController : MonoBehaviour {
	public float speed = 1f;
	public float neighborRadius = 1f;
	//public float viewAngle = 3f;

	Collider fishCollider;

	const float VBound = 15f;
	const float HBound = 14f;
	
	//private static Color arcColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	void Start() {
		fishCollider = GetComponent<Collider>();
	}

	void Update() {
		transform.Translate(Time.deltaTime * speed * transform.forward, Space.World);

		checkBounds();

		List<Transform> neighbors = getNeighbors();
		GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.white, Color.red, neighbors.Count / 6f);
	}

	List<Transform> getNeighbors() {
		List<Transform> neighbors = new List<Transform>();
		Collider[] colliders = Physics.OverlapSphere(transform.position, neighborRadius);
		
		foreach (Collider c in colliders)
			if (c != fishCollider)
				neighbors.Add(c.transform);

		return neighbors;
	}

	void checkBounds() {
		Vector3 pos = transform.position;
		
		if (pos.x > HBound)
			transform.position = new Vector3(-HBound, pos.y, pos.z);
		else if (transform.position.x < -HBound)
			transform.position = new Vector3(HBound, pos.y, pos.z);

		if (transform.position.z > VBound)
			transform.position = new Vector3(pos.x, pos.y, -VBound);
		else if (transform.position.z < -VBound)
			transform.position = new Vector3(pos.x, pos.y, VBound);
	}

	/*void OnDrawGizmos() {
		Gizmos.DrawRay(transform.position, transform.forward * 5f);
		Handles.color = arcColor;
		Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
		Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, viewAngle / 2, 1f);
		Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -viewAngle / 2, 1f);
	}*/
}