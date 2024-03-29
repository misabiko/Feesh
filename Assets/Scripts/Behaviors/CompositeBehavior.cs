﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior {
	public FlockBehavior[] behaviors;
	public float[] weights;
	
	public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
		if (weights.Length != behaviors.Length) {
			Debug.LogError("Data mismatch in " + name, this);
			return Vector3.zero;
		}

		Vector3 move = Vector3.zero;
		
		//iterate through behaviors
		for (int i = 0; i < behaviors.Length; i++) {
			Vector3 partialMove = behaviors[i].calculateMove(agent, context, flock) * weights[i];
			if (partialMove != Vector3.zero) {
				if (partialMove.sqrMagnitude > weights[i] * weights[i]) {
					partialMove.Normalize();
					partialMove *= weights[i];
				}

				move += partialMove;
			}
		}

		return move;
	}
}