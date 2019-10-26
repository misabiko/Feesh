using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior {
	public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
		//if no neighbors, return no adjustment
		if (context.Count == 0)
			return Vector3.zero;
		
		//average all points
		Vector3 cohesionMove = Vector3.zero;
		List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context);
		foreach (Transform item in filteredContext)
			cohesionMove += item.position;
		cohesionMove /= filteredContext.Count;

		//create offset from agent position
		cohesionMove -= agent.transform.position;
		return cohesionMove;
	}
}