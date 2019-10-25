using UnityEngine;

public class Spawner : MonoBehaviour {
	public FishController fishPrefab;
	[Range(1, 500)] public int count = 10;
	public float density = 0.08f;
	
	void Start() {
		for (int i = 0; i < count; i++) {
			Vector2 pos2 = Random.insideUnitCircle * count * density;
			Vector3 pos3 = new Vector3(pos2.x, 0, pos2.y);
			Instantiate(
				fishPrefab,
				pos3,
				Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
				transform
			);
		}
	}
}