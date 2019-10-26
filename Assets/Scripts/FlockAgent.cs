using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour {
	Flock agentFlock;
	public Flock AgentFlock {
		get {return agentFlock;}
	}
	
	Collider agentCollider;
	public Collider AgentCollider {
		get {return agentCollider;}
	}

	void Start() {
		agentCollider = GetComponent<Collider>();
	}

	public void initialize(Flock flock) {
		agentFlock = flock;
	}

	public void move(Vector3 velocity) {
		transform.forward = velocity.normalized;
		transform.position += velocity * Time.deltaTime;
	}
}