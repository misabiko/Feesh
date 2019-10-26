using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignement")]
public class AlignementBehavior : FilteredFlockBehavior {
	public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
		//if no neighbors, maintain current movement
		if (context.Count == 0)
			return agent.transform.forward;
		
		//average all directions
		Vector3 alignementMove = Vector3.zero;
		List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context);
		foreach (Transform item in filteredContext)
			alignementMove += item.transform.forward;
		alignementMove /= filteredContext.Count;

		return alignementMove;
	}
}