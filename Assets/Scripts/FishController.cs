using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FishController : MonoBehaviour {
	public float maxSpeed;
	public float neighborRadius = 1f;
	//public float viewAngle = 3f;

	Collider fishCollider;
	float squareMaxSpeed;

	const float VBound = 15f;
	const float HBound = 14f;
	
	//private static Color arcColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	void Start() {
		fishCollider = GetComponent<Collider>();
		squareMaxSpeed = maxSpeed * maxSpeed;
	}

	void Update() {
		List<Transform> neighbors = getNeighbors();
		
		Vector3 velocity = calculateCohesion(neighbors);

		if (velocity.sqrMagnitude > squareMaxSpeed)
			velocity = velocity.normalized * maxSpeed;
		
		move(velocity);

		//checkBounds();
	}

	Vector3 calculateCohesion(List<Transform> neighbors) {
		Vector3 cohesionMove = Vector3.zero;

		if (neighbors.Count > 0) {
			foreach (Transform neighbor in neighbors)
				cohesionMove += neighbor.position;

			cohesionMove /= neighbors.Count;
			cohesionMove -= transform.position;
		}

		return cohesionMove;
	}

	void move(Vector3 velocity) {
		transform.position += Time.deltaTime * velocity;
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