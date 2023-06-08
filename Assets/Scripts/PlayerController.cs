using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private InputHandler _input;
	[SerializeField] float moveSpeed;
	[SerializeField] float rotateSpeed;

	[SerializeField] Camera camera;
	[SerializeField] bool rotateTowardsMouse;

	private void Awake() {
		_input = GetComponent<InputHandler>();
	}

	private AudioSource _audioSource;
	[SerializeField] private AudioClip _takeObjectSound;

	[SerializeField] Text _countGarbageText;
	private static int _garbageObjCount = 0;// количество мусора на сцене
	public static int _garbageInStorage = 0;// количество собранного мусора
	private static int _garbageInIncinerator = 0;// количество мусора в мусоросжигателе

	enum State { Playing, Dead, NextLevel };
	private State _state = State.Playing;
	private static int lvl = 1;
	void Start() {
		_audioSource = GetComponent<AudioSource>();
		_garbageObjCount = GameObject.FindGameObjectsWithTag("Garbage1").Length + GameObject.FindGameObjectsWithTag("Garbage2").Length + GameObject.FindGameObjectsWithTag("Garbage3").Length;
		ViewCountGarbajeText(_garbageObjCount, _garbageInStorage, _garbageInIncinerator);
	}

	void Update() {
		var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

		var movmentVector = MoveTowardTarget(targetVector);
		if (!rotateTowardsMouse)
			RotateTowardMovmentVector(movmentVector);
		else
			RotateTowardsMouseVector();

		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	private void ViewCountGarbajeText(int garbageObjCount, int garbageInStorage, int garbageInIncinerator) {
		_countGarbageText.text = $"Всего хлама = {garbageObjCount}. Хлам в ковше = {garbageInStorage}. Хлам в мусоросжигателе = {garbageInIncinerator}.";
	}

	private Vector3 MoveTowardTarget(Vector3 targetVector) {
		var speed = moveSpeed * Time.deltaTime;
		targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
		var targerPosition = transform.position + targetVector * speed;
		transform.position = targerPosition;
		return targetVector;
	}

	private void RotateTowardMovmentVector(Vector3 movmentVector) {
		if (movmentVector.magnitude == 0)
			return;
		var rotation = Quaternion.LookRotation(movmentVector);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
	}

	private void RotateTowardsMouseVector() {
		Ray ray = camera.ScreenPointToRay(_input.MousePosition);

		if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f)) {
			var target = hitInfo.point;
			target.y = transform.position.y;
			transform.LookAt(target);
		}
	}

	void OnCollisionEnter(Collision collision) {
		var obj = gameObject.name;
		switch (collision.gameObject.tag) {
			case "Garbage1":
			if (obj == "StorageSorter1") {
				GetGarbage(collision.gameObject);
				_audioSource.PlayOneShot(_takeObjectSound);
			}
			break;
			case "Garbage2":
			if (obj == "StorageSorter2") {
				GetGarbage(collision.gameObject);
				_audioSource.PlayOneShot(_takeObjectSound);
			}
			break;
			case "Garbage3":
			if (obj == "StorageSorter3") {
				GetGarbage(collision.gameObject);
				_audioSource.PlayOneShot(_takeObjectSound);
			}
			break;
			case "Finish":
			if (GameObject.FindGameObjectsWithTag("Garbage1").Length + GameObject.FindGameObjectsWithTag("Garbage2").Length + GameObject.FindGameObjectsWithTag("Garbage3").Length == 0) {
				++lvl;
				_state = State.NextLevel;
				Invoke("LoadNextLevel", 0.1f);
				_garbageInIncinerator = 0;
			} else {
				_garbageObjCount -= _garbageInStorage;
				_garbageInIncinerator += _garbageInStorage;
				_garbageInStorage = 0;
				ViewCountGarbajeText(_garbageObjCount, _garbageInStorage, _garbageInIncinerator);
			}
			break;
			default:
			break;
		}
	}

	private void GetGarbage(GameObject garbajeObj) {
		ViewCountGarbajeText(_garbageObjCount, ++_garbageInStorage, _garbageInIncinerator);
		Destroy(garbajeObj);
	}
	private void LoadNextLevel() {
		SceneManager.LoadScene(lvl);
	}
}
