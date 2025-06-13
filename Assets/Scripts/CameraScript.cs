using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
        StartCoroutine(AnotherRotation());
    }

    IEnumerator AnotherRotation()
    {
        yield return new WaitForEndOfFrame();
        transform.rotation = Quaternion.identity;
    }
}
