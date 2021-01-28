using UnityEngine;

public class CamBackView : CamView
{
    public override CamViewModes CamViewModes => CamViewModes.Back;
    public override void ViewMode(Transform objTrans, Transform target, CamFollowSettings camFollowSettings)
    {
        objTrans.rotation = Quaternion.Lerp(objTrans.rotation, Quaternion.Euler(camFollowSettings.TopViewValue, target.eulerAngles.y, 0), camFollowSettings.RotateSpeed * Time.deltaTime);
    }
}
