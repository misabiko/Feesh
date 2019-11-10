using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class TargetSpawner : MonoBehaviour {
	Camera cam;
	Plane targetPlane;

	void Awake() {
		cam = GetComponent<Camera>();
		
		InputActionAsset inputMap = GetComponent<PlayerInput>().actions;
		inputMap["Interact"].started += onInteract;
		
		targetPlane = new Plane(Vector3.up, 0f);
	}

	void onInteract(InputAction.CallbackContext context) {
		Vector2 mousePos = Mouse.current.position.ReadValue();
		Vector3 targetPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.y));

		Ray ray = cam.ScreenPointToRay(mousePos);

		float enter;
		if (!targetPlane.Raycast(ray, out enter)) {
			Debug.Log("Click missed?");
			return;
		}

		spawnTarget(ray.GetPoint(enter));
	}

	void spawnTarget(Vector3 position) {
		Debug.Log("Spawn");
		GameObject newTarget = GameObject.CreatePrimitive(PrimitiveType.Cube);
		newTarget.transform.position = position;
	}
}