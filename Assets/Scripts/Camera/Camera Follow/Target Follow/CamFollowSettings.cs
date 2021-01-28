using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Settings", fileName = "Camera Data")]
public class CamFollowSettings : ScriptableObject
{
    #region Serialized Fields
    [SerializeField] [Range(0.01f, 1f)] float soomthFollowValue = 0.15f;
    [SerializeField] float rotateSpeed = 3;
    [SerializeField] float distanceFromTarget = 8;
    [SerializeField] float topViewValue = 20;
    #endregion

    #region Parameters
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
    /// <summary>
    /// Subcribing to the GameState Events
    /// </summary>
    public CamFollowSettings()
    {
        GameState.OnStart += Initialise;
        GameState.OnEnd += Reset;
    }
    #endregion

    /// <summary>
    /// Initialising values when start values
    /// </summary>
    void Initialise()
    {
        InitialSoomthFollowValue = soomthFollowValue;
        InitialDistance = distanceFromTarget;
        InitialTopViewValue = topViewValue;
        InitialRotateSpeed = rotateSpeed;
    }

    /// <summary>
    /// Resetting to initial values when stop playing
    /// </summary>
    void Reset()
    {
        soomthFollowValue = InitialSoomthFollowValue;
        distanceFromTarget = InitialDistance;
        topViewValue = InitialTopViewValue;
        rotateSpeed = InitialRotateSpeed;
    }
}
