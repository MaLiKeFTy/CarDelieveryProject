using UnityEngine;

public class CamFollowController
{
    readonly CamFollowSettings camFollowSettings;
    readonly Transform objTrans;
    readonly Transform targetTrans;

    Vector3 objVelocity;
    CamViewModes camView = CamViewModes.Back;

    public CamFollowController(CamFollow camFollow)
    {
        camFollowSettings = camFollow.CamFollowSettings;
        objTrans = camFollow.transform;
        targetTrans = camFollow.Target;
    }

    public void Tick() => FollowTarget();

    void FollowTarget()
    {
        var targetPos = targetTrans.position - (objTrans.forward * camFollowSettings.DistanceFromTarget);
        objTrans.localPosition = Vector3.SmoothDamp(objTrans.localPosition, targetPos, ref objVelocity, camFollowSettings.SoomthFollowValue);
        CamViewModesProcessor.ChangeCamView(objTrans, targetTrans, camFollowSettings, camView);
        ChangeCamView();
    }

    void ChangeCamView()
    {
        var vehicle = targetTrans.GetComponent<Vehicle>();
        if (vehicle)
            if (vehicle.VehicleState.IsReversing)
            {
                camView = CamViewModes.Front;
                camFollowSettings.RotateSpeed = camFollowSettings.InitialRotateSpeed / 3;
            }
            else
            {
                camView = CamViewModes.Back;
                camFollowSettings.RotateSpeed = camFollowSettings.InitialRotateSpeed;
            }
    }
}
