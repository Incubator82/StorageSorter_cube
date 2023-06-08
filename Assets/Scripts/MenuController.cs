using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(1);
        }
    }
}
