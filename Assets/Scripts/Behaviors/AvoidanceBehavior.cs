using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior {
	public Boolean steered;
	Vector3 currentVelocity;
	//time to go from current to next velocity
	public float agentSmoothTime = 0.5f;

	void Awake() {
		currentVelocity = new Vector3();
	}
	
	public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
		//if no neighbors, return no adjustment
		if (context.Count == 0)
			return Vector3.zero;

		//average all points
		Vector3 avoidanceMove = Vector3.zero;
		int numAvoids = 0;
		List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context);
		if (filteredContext.Count == 0)
			return Vector3.zero;

		foreach (Transform item in filteredContext) {
			Ray ray = new Ray(agent.transform.position, item.position - agent.transform.position);
			RaycastHit hit;
			
			if (item.GetComponent<Collider>().Raycast(ray, out hit, Vector3.SqrMagnitude(item.position - agent.transform.position)))
				if (hit.distance * hit.distance < flock.SquareAvoidanceRadius) {
					numAvoids++;
					avoidanceMove += agent.transform.position - item.position;
				}
		}

		if (numAvoids > 0)
			avoidanceMove /= numAvoids;
		if (steered)
			avoidanceMove = Vector3.SmoothDamp(agent.transform.forward, avoidanceMove, ref currentVelocity, agentSmoothTime);
		
		return avoidanceMove;
	}
}