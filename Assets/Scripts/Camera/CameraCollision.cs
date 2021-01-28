using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    #region Fields
    [SerializeField] LayerMask collisionMask;
    Vector3[] cameraClipPoints;
    Camera cam;
    CamFollow camFollow;
    CamFollowSettings camFollowSettings;
    float distanceDestination;
    #endregion

    #region Initialisation
    void Start() => Initialise(Camera.main);

    public void Initialise(Camera cam)
    {
        camFollow = GetComponent<CamFollow>();
        camFollowSettings = camFollow.CamFollowSettings;
        this.cam = cam;
        cameraClipPoints = new Vector3[4]; //To cast 4 rays from the camera clip points
    }
    #endregion

    #region Update loops
    void Update() => UpdateCameraPositionClipPoints(transform.position, transform.rotation, ref cameraClipPoints);

    void FixedUpdate()
    {
        GetCamClipPointsHitDistance(camFollow.Target.position);
        CenterRaycast();
    }
    #endregion

    #region CameraClipPoints
    void UpdateCameraPositionClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
    {
        if (!cam)
            return;

        intoArray = new Vector3[4];

        float z = cam.nearClipPlane;
        float x = Mathf.Tan(cam.fieldOfView / 3.41f) * z;
        float y = x / cam.aspect;

        // Calculating camera clip points
        intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition;
        intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition;
        intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition;
        intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition;
    }

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
            MoveCamera(distance);
    }
    #endregion

    #region ResettingCameraAttributes
    /// <summary> Raycast for resetting camera parameters </summary>
    void CenterRaycast()
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
