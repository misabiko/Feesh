using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior {
	public Vector3 center;
	public float radius = 15f;
	
	public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
		Vector3 centerOffset = center - agent.transform.position;
		//Fraction of pos from center to edge
		float t = centerOffset.magnitude / radius;

		if (t < 0.9f)
			return Vector3.zero;

		return t * t * centerOffset;
	}
}
