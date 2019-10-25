﻿using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour {
	Collider agentCollider;
	public Collider AgentCollider {
		get {return agentCollider;}
	}

	void Start() {
		agentCollider = GetComponent<Collider>();
	}

	public void move(Vector3 velocity) {
		transform.forward = velocity.normalized;
		transform.position += velocity * Time.deltaTime;
	}
}