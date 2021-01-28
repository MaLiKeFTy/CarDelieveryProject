using UnityEngine;

public class CamLeftView : CamView
{
    public override CamViewModes CamViewModes => CamViewModes.Left;
    public override void ViewMode(Transform objTrans, Transform target, CamFollowSettings camFollowSettings)
    {
        objTrans.rotation = Quaternion.Lerp(objTrans.rotation, Quaternion.Euler(camFollowSettings.TopViewValue, target.eulerAngles.y + 90, 0), camFollowSettings.RotateSpeed * Time.deltaTime);
    }
}
