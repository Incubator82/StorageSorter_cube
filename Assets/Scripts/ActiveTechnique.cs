using UnityEngine;

public class ActiveTechnique : MonoBehaviour
{
	[SerializeField] private GameObject storageSorter1;
	[SerializeField] private GameObject storageSorter2;
	[SerializeField] private GameObject storageSorter3;

	private Vector3 _startPositionStorageSorter1;
	private Quaternion _startRotationStorageSorter1;

	private Vector3 _startPositionStorageSorter2;
	private Quaternion _startRotationStorageSorter2;

	private Vector3 _startPositionStorageSorter3;
	private Quaternion _startRotationStorageSorter3;
	private void Start() {
		_startPositionStorageSorter1 = storageSorter1.transform.position;
		_startRotationStorageSorter1 = storageSorter1.transform.rotation;

		_startPositionStorageSorter2 = storageSorter1.transform.position;
		_startRotationStorageSorter2 = storageSorter1.transform.rotation;

		_startPositionStorageSorter3 = storageSorter1.transform.position;
		_startRotationStorageSorter3 = storageSorter1.transform.rotation;
	}
	public void ActiveStorageSorter1() {
		if (!storageSorter1.activeSelf) {

			if (PlayerController._garbageInStorage != 0) {
//#if UNITY_EDITOR
//				UnityEditor.EditorUtility.DisplayDialog("StorageSorter", "Нельзя сменить технику пока в ковше есть мусор!", "Ок");
//#endif

			} else {
				storageSorter1.SetActive(true);
				storageSorter2.SetActive(false);
				storageSorter3.SetActive(false);
				storageSorter1.transform.position = _startPositionStorageSorter1;
				storageSorter1.transform.rotation = _startRotationStorageSorter1;
			}
		}
	}

	public void ActiveStorageSorter2() {
		if (!storageSorter2.activeSelf) {

			if (PlayerController._garbageInStorage != 0) {
//#if UNITY_EDITOR
//				UnityEditor.EditorUtility.DisplayDialog("StorageSorter", "Нельзя сменить технику пока в ковше есть мусор!", "Ок");
//#endif
			} else {
				storageSorter1.SetActive(false);
				storageSorter2.SetActive(true);
				storageSorter3.SetActive(false);
				storageSorter2.transform.position = _startPositionStorageSorter2;
				storageSorter2.transform.rotation = _startRotationStorageSorter2;
			}
		}
	}

	public void ActiveStorageSorter3() {
		if (!storageSorter3.activeSelf) {

			if (PlayerController._garbageInStorage != 0) {
//#if UNITY_EDITOR
//				UnityEditor.EditorUtility.DisplayDialog("StorageSorter", "Нельзя сменить технику пока в ковше есть мусор!", "Ок");
//#endif
			} else {
				storageSorter1.SetActive(false);
				storageSorter2.SetActive(false);
				storageSorter3.SetActive(true);
				storageSorter3.transform.position = _startPositionStorageSorter3;
				storageSorter3.transform.rotation = _startRotationStorageSorter3;
			}
		}
	}
}
