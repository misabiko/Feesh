using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignement")]
public class AlignementBehavior : FlockBehavior {
	public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
		//if no neighbors, maintain current movement
		if (context.Count == 0)
			return agent.transform.forward;
		
		//average all directions
		Vector3 alignementMove = Vector3.zero;
		foreach (Transform item in context)
			alignementMove += item.transform.forward;
		alignementMove /= context.Count;

		return alignementMove;
	}
}