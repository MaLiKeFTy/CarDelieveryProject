using System;
using UnityEngine;

public class CamClipPointsProcessor
{
    #region Fields
    readonly CamFollow camFollow;
    readonly Vector3[] cameraClipPoints;
    readonly LayerMask collisionMask;

    Camera cam;
    event Action<float> camClipPointsCollided;
    #endregion

    #region Constructor
    public CamClipPointsProcessor(CamFollow camFollow, Vector3[] cameraClipPoints, LayerMask collisionMask, Action<float> camClipPointsCollided)
    {
        this.camFollow = camFollow;
        this.cameraClipPoints = cameraClipPoints;
        this.collisionMask = collisionMask;
        this.camClipPointsCollided = camClipPointsCollided;

        //To get the camera from the CamFollow class without having to pass an extra argument to this constructor.
        cam = camFollow.GetComponent<Camera>();
    }
    #endregion

    #region Update Caller
    public void DeployCamClipPoints()
    {
        UpdateCameraPositionClipPoints();
        GetCamClipPointsHitDistance(camFollow.Target.position);
    }
    #endregion

    void UpdateCameraPositionClipPoints()
    {
        if (!cam)
            return;

        var camTrans = cam.transform;

        float z = cam.nearClipPlane;
        float x = Mathf.Tan(cam.fieldOfView / 3.41f) * z;
        float y = x / cam.aspect;

        // Calculating camera clip points
        cameraClipPoints[0] = (camTrans.rotation * new Vector3(-x, y, z)) + camTrans.position;
        cameraClipPoints[1] = (camTrans.rotation * new Vector3(x, y, z)) + camTrans.position;
        cameraClipPoints[2] = (camTrans.rotation * new Vector3(-x, -y, z)) + camTrans.position;
        cameraClipPoints[3] = (camTrans.rotation * new Vector3(x, -y, z)) + camTrans.position;
    }

    /// <summary>
    /// Get the nearest camera hit clip point.
    /// </summary>
    void GetCamClipPointsHitDistance(Vector3 from)
    {
        float distance = -1;

        foreach (var clipPoint in cameraClipPoints)
        {
            float rayMaxDistance = Vector3.Distance(clipPoint, from);
            Ray ray = new Ray(from, clipPoint - from);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayMaxDistance, collisionMask))
            {

                if (distance == -1) //To get closest clip point hit distance.
                {
                    distance = hit.distance;
                }
                else
                {
                    if (hit.distance < distance)
                    {
                        distance = hit.distance;
                    }
                }
            }

        }
        if (distance != -1)
            camClipPointsCollided?.Invoke(distance);
    }
}
