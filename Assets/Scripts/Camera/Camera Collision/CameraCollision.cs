using System;
using UnityEngine;

[RequireComponent(typeof(CamFollow))]
public class CameraCollision : MonoBehaviour
{
    #region Fields
    [SerializeField] LayerMask collisionMask;

    Vector3[] cameraClipPoints;
    CamFollow camFollow;
    CamFollowSettings camFollowSettings;
    CamClipPointsProcessor clipPointProcessor;
    float distanceDestination;
    #endregion

    #region Initialisation
    void Start() => Initialise();

    public void Initialise()
    {
        camFollow = GetComponent<CamFollow>();
        camFollowSettings = camFollow.CamFollowSettings;
        cameraClipPoints = new Vector3[4]; //To cast 4 rays from the camera clip points
        clipPointProcessor = new CamClipPointsProcessor(camFollow, cameraClipPoints, collisionMask, (float distance) => MoveCamera(distance));
    }
    #endregion

    #region Update loops
    void FixedUpdate()
    {
        clipPointProcessor.DeployCamClipPoints();
        CamMainRaycast();
    }
    #endregion

    #region ResettingCameraAttributes
    /// <summary> Raycast for resetting camera parameters </summary>
    void CamMainRaycast()
    {
        Vector3 adjustedCamera = transform.position - transform.forward;
        Ray ray = new Ray(camFollow.Target.position, adjustedCamera - camFollow.Target.position);
        float distance = Vector3.Distance(adjustedCamera, camFollow.Target.position) + 10;
        if (!Physics.Raycast(ray, distance, collisionMask))
        {
            camFollowSettings.DistanceFromTarget = Mathf.Lerp(camFollowSettings.DistanceFromTarget, camFollowSettings.InitialDistance, 10 * Time.deltaTime);
            camFollowSettings.TopViewValue = Mathf.Lerp(camFollowSettings.TopViewValue, camFollowSettings.InitialTopViewValue, 10 * Time.deltaTime);
        }
    }

    /// <summary> Move camera to clip point hit position </summary>
    void MoveCamera(float distance)
    {
        if (camFollowSettings.DistanceFromTarget >= 4) //To prevent the camera from moving too much into the target.
        {
            distanceDestination = distance;
            camFollowSettings.DistanceFromTarget = Mathf.Lerp(camFollowSettings.DistanceFromTarget, distanceDestination, 5 * Time.deltaTime);
            camFollowSettings.TopViewValue = Mathf.Lerp(camFollowSettings.TopViewValue, camFollowSettings.InitialTopViewValue * 2, 20 * Time.deltaTime);
        }
        else
        {
            camFollowSettings.TopViewValue = Mathf.Lerp(camFollowSettings.TopViewValue, camFollowSettings.InitialTopViewValue * 2, 20 * Time.deltaTime);
        }
    }
    #endregion
}
