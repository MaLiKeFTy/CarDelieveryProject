using UnityEngine;
public abstract class CamView
{
    /// <summary>
    /// Determin which direction will the camera look to the target
    /// </summary>
    /// <param name="objTrans">The camera transform</param>
    /// <param name="target">The target transform</param>
    /// <param name="camFollowSettings">the camera settings</param>
    public abstract void ViewMode(Transform objTrans, Transform target, CamFollowSettings camFollowSettings);
    public abstract CamViewModes CamViewModes { get; }
}
