using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : CohesionBehavior {
	Vector3 currentVelocity;
	//time to go from current to next velocity
	public float agentSmoothTime = 0.5f;
	
	public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
		Vector3 cohesionMove = base.calculateMove(agent, context, flock);
		cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
		return cohesionMove;
	}
}