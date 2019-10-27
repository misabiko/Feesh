using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior {
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
		Vector3 cohesionMove = Vector3.zero;
		List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context);
		if (filteredContext.Count == 0)
			return Vector3.zero;

		foreach (Transform item in context)
			cohesionMove += item.position;
		cohesionMove /= context.Count;

		//create offset from agent position
		cohesionMove -= agent.transform.position;
		cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
		
		return cohesionMove;
	}
}