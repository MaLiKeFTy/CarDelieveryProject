using UnityEngine;

public class CamRightView : CamView
{
    public override CamViewModes CamViewModes => CamViewModes.Right;
    public override void ViewMode(Transform objTrans, Transform target, CamFollowSettings camFollowSettings)
    {
        objTrans.rotation = Quaternion.Lerp(objTrans.rotation, Quaternion.Euler(camFollowSettings.TopViewValue, target.eulerAngles.y - 90, 0), camFollowSettings.RotateSpeed * Time.deltaTime);
    }
}
