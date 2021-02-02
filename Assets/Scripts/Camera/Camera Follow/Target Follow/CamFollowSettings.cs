using UnityEngine;

[System.Serializable]
public class CamFollowSettings
{
    #region Serialized Fields
    [SerializeField] [Range(0.01f, 1f)] float soomthFollowValue = 0.15f;
    [SerializeField] float rotateSpeed = 3;
    [SerializeField] float distanceFromTarget = 8;
    [SerializeField] float topViewValue = 20;
    #endregion

    #region Properties
    public float SoomthFollowValue { get { return soomthFollowValue; } set { soomthFollowValue = value; } }
    public float RotateSpeed { get { return rotateSpeed; } set { rotateSpeed = value; } }
    public float DistanceFromTarget { get { return distanceFromTarget; } set { distanceFromTarget = value; } }
    public float TopViewValue { get { return topViewValue; } set { topViewValue = value; } }
    #endregion

    #region Initial Values
    public float InitialSoomthFollowValue { get; private set; }
    public float InitialDistance { get; private set; }
    public float InitialTopViewValue { get; private set; }
    public float InitialRotateSpeed { get; private set; }
    #endregion

    #region Contructor

    public CamFollowSettings()
    {
        Initialise();
    }
    #endregion

    void Initialise()
    {
        InitialSoomthFollowValue = soomthFollowValue;
        InitialDistance = distanceFromTarget;
        InitialTopViewValue = topViewValue;
        InitialRotateSpeed = rotateSpeed;
    }
}
