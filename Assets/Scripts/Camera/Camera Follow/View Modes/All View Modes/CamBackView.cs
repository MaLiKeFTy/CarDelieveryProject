using UnityEngine;

public class CamBackView : CamView
{
    public override CamViewModes CamViewModes => CamViewModes.Back;
    public override void ViewMode(Transform objTrans, Transform target, CamFollowSettings camFollowSettings)
    {
        if (Vector3.Dot(target.up, Vector3.up) > 0)
            objTrans.rotation = Quaternion.Lerp(objTrans.rotation, Quaternion.Euler(camFollowSettings.TopViewValue, target.eulerAngles.y, 0), camFollowSettings.RotateSpeed * Time.deltaTime);
        else
            objTrans.rotation = Quaternion.Lerp(objTrans.rotation, Quaternion.Euler(camFollowSettings.TopViewValue, target.eulerAngles.y + 180, 0), camFollowSettings.RotateSpeed * Time.deltaTime);
    }
}
