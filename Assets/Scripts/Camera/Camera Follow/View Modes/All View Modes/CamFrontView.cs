using UnityEngine;

public class CamFrontView : CamView
{
    public override CamViewModes CamViewModes => CamViewModes.Front;
    public override void ViewMode(Transform objTrans, Transform target, CamFollowSettings camFollowSettings)
    {
        objTrans.rotation = Quaternion.Lerp(objTrans.rotation, Quaternion.Euler(camFollowSettings.TopViewValue, target.eulerAngles.y + 180, 0), camFollowSettings.RotateSpeed * Time.deltaTime);
    }
}
