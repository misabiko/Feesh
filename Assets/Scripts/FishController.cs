using System;
using UnityEditor;
using UnityEngine;

public class FishController : MonoBehaviour {
	public float speed = 1f;
	public float viewAngle = 3f;
	public float rotation = 0f;
	public Vector3 from = Vector3.left;

	public float vBound = 15f;
	public float hBound = 14f;

	private Color arcColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
	
	void Start() {}

	void Update() {
		transform.Rotate(Vector3.up, rotation);
		transform.Translate(Time.deltaTime * speed * transform.forward, Space.World);

		Vector3 pos = transform.position;
		
		if (pos.x > hBound)
			transform.position = new Vector3(-hBound, pos.y, pos.z);
		else if (transform.position.x < -hBound)
			transform.position = new Vector3(hBound, pos.y, pos.z);

		if (transform.position.z > vBound)
			transform.position = new Vector3(pos.x, pos.y, -vBound);
		else if (transform.position.z < -vBound)
			transform.position = new Vector3(pos.x, pos.y, vBound);
	}

	void OnDrawGizmos() {
		Gizmos.DrawRay(transform.position, transform.forward * 5f);
		Handles.color = arcColor;
		Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
		Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, viewAngle / 2, 1f);
		Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -viewAngle / 2, 1f);
	}
}