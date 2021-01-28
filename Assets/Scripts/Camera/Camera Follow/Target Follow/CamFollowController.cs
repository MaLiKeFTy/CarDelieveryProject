using UnityEngine;

public class CamFollowController
{
    readonly CamFollowSettings camFollowSettings;
    readonly Transform objTrans;
    readonly Transform targetTrans;

    Vector3 objVelocity;
    CamViewModes camView = CamViewModes.Back;

    public CamFollowController(CamFollowSettings camFollowSettings, Transform objTrans, Transform targetTrans)
    {
        this.camFollowSettings = camFollowSettings;
        this.objTrans = objTrans;
        this.targetTrans = targetTrans;
    }

    public void Tick() => FollowTarget();

    void FollowTarget()
    {
        var targetPos = targetTrans.position - (objTrans.forward * camFollowSettings.DistanceFromTarget);
        objTrans.localPosition = Vector3.SmoothDamp(objTrans.localPosition, targetPos, ref objVelocity, camFollowSettings.SoomthFollowValue);
        CamViewModesProcessor.ChangeCamView(objTrans, targetTrans, camFollowSettings, camView);
    }
}
