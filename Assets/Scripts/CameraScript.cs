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

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
