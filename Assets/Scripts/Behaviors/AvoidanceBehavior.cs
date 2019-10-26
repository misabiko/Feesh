using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior {
	public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
		//if no neighbors, return no adjustment
		if (context.Count == 0)
			return Vector3.zero;

		//average all points
		Vector3 avoidanceMove = Vector3.zero;
		int numAvoids = 0;
		List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context);
		foreach (Transform item in filteredContext)
			if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius) {
				numAvoids++;
				avoidanceMove += agent.transform.position - item.position;
			}
		if (numAvoids > 0)
			avoidanceMove /= numAvoids;

		return avoidanceMove;
	}
}