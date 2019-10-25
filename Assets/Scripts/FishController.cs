using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FishController : MonoBehaviour {
	public float maxSpeed;
	public float neighborRadius = 1f;

	public float avoidanceRadius;
	//public float viewAngle = 3f;

	Collider fishCollider;
	float squareMaxSpeed;
	float squareAvoidanceRadius;

	const float VBound = 15f;
	const float HBound = 14f;
	
	//private static Color arcColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	void Start() {
		fishCollider = GetComponent<Collider>();
		squareMaxSpeed = maxSpeed * maxSpeed;
		squareAvoidanceRadius = avoidanceRadius * avoidanceRadius;
	}

	void Update() {
		List<Transform> neighbors = getNeighbors();
		
		Vector3 velocity = calculateAvoidance(neighbors);

		if (velocity.sqrMagnitude > squareMaxSpeed)
			velocity = velocity.normalized * maxSpeed;
		
		move(velocity);
	}

	Vector3 calculateCohesion(List<Transform> neighbors) {
		Vector3 cohesionMove = Vector3.zero;

		//if no neighbors, return no adjustments
		if (neighbors.Count <= 0) return cohesionMove;
		
		//average every neighbor positions
		foreach (Transform neighbor in neighbors)
			cohesionMove += neighbor.position;
		cohesionMove /= neighbors.Count;
		
		//create offset from pos
		cohesionMove -= transform.position;

		return cohesionMove;
	}

	Vector3 calculateAlignement(List<Transform> neighbors) {
		//if no neighbors, keep direction (move at speed 1)
		if (neighbors.Count <= 0) return transform.forward;
		
		//average every neighbor directions
		Vector3 alignementMove = Vector3.zero;
		foreach (Transform neighbor in neighbors)
			alignementMove += neighbor.transform.forward;
		alignementMove /= neighbors.Count;

		return alignementMove;
	}

	Vector3 calculateAvoidance(List<Transform> neighbors) {
		Vector3 avoidanceMove = Vector3.zero;

		//if no neighbors, return no adjustments
		if (neighbors.Count <= 0) return avoidanceMove;
		
		//average every neighbor positions
		int numAvoids = 0;
		foreach (Transform neighbor in neighbors)
			if (Vector3.SqrMagnitude(neighbor.position - transform.position) < squareAvoidanceRadius) {
				numAvoids++;
				avoidanceMove += transform.position - neighbor.position;
			}

		if (numAvoids > 0)
			avoidanceMove /= numAvoids;

		return avoidanceMove;
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