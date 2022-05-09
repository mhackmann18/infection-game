using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1f;
    public float maxDistnce = 4f;
    public float smooth = 10f;
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;
    // Start is called before the first frame update
    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCameraPosition = transform.parent.TransformPoint(dollyDir * maxDistnce);
        RaycastHit hit;

        if(Physics.Linecast(transform.parent.position, desiredCameraPosition, out hit)){
            distance = Mathf.Clamp(hit.distance * .9f, minDistance, maxDistnce);
        } else {
            distance = maxDistnce;
        }

        transform.position = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
