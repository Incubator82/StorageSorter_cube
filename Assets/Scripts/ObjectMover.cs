using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public GameObject storageSorter1;
    public GameObject storageSorter2;
    public GameObject storageSorter3;

    void Update()
    {
        if (storageSorter1.activeSelf)
            transform.position = storageSorter1.transform.position + new Vector3(0, 0, 0);

        if (storageSorter2.activeSelf)
            transform.position = storageSorter2.transform.position + new Vector3(0, 0, 0);

        if (storageSorter3.activeSelf)
            transform.position = storageSorter3.transform.position + new Vector3(0, 0, 0);
    }
}
