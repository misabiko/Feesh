using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Flock : MonoBehaviour {
	public FlockAgent agentPrefab;
	List<FlockAgent> agents = new List<FlockAgent>();
	public FlockBehavior behavior;

	[Range(10, 500)] public int startingCount = 250;
	const float agentDensity = 0.08f;

	[Range(1f, 100f)] public float driveFactor = 10f;
	[Range(1f, 100f)] public float maxSpeed = 5f;
	[Range(1f, 10f)] public float neighborRadius = 1.5f;
	[Range(0f, 1f)] public float avoidanceRadiusMultiplier = 0.5f;

	float squareMaxSpeed;
	float squareNeighborRadius;
	float squareAvoidanceRadius;
	public float SquareAvoidanceRadius {
		get {return squareAvoidanceRadius;}
	}

	void Start() {
		//setting the squares in advance
		squareMaxSpeed = maxSpeed * maxSpeed;
		squareNeighborRadius = neighborRadius * neighborRadius;
		squareAvoidanceRadius = avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

		for (int i = 0; i < startingCount; i++) {
			Vector2 randomPoint = Random.insideUnitCircle * startingCount * agentDensity;
			FlockAgent newAgent = Instantiate(
				agentPrefab,
				new Vector3(randomPoint.x, 0, randomPoint.y),
				quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
				transform
			);

			newAgent.name = "Agent " + i;
			newAgent.initialize(this);
			agents.Add(newAgent);
		}
	}

	void Update() {
		foreach (FlockAgent agent in agents) {
			List<Transform> context = getNearbyObjects(agent);

			Vector3 move = behavior.calculateMove(agent, context, this);
			move *= driveFactor;
			if (move.sqrMagnitude > squareMaxSpeed)
				move = move.normalized * maxSpeed;
			agent.move(move);
		}
	}

	List<Transform> getNearbyObjects(FlockAgent agent) {
		List<Transform> context = new List<Transform>();

		Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
		foreach (Collider c in contextColliders)
			if (c != agent.AgentCollider)
				context.Add(c.transform);
		
		return context;
	}
}