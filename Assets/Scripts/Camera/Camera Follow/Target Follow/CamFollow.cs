using UnityEngine;

public class CamFollow : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] CamFollowSettings camFollowSettings;
    [SerializeField] Transform target; //target to Follow
    #endregion

    #region Parameters
    public Transform Target { get { return target; } }
    public CamFollowSettings CamFollowSettings => camFollowSettings;
    #endregion

    #region Private Fields
    CamFollowController followController;
    #endregion

    void Awake() => Initialise();


    void Initialise()
    {
        followController = new CamFollowController(CamFollowSettings, transform, Target);
    }


    void LateUpdate() => followController.Tick();

    #region CamViewModes
    void ChangeCamView()
    {
        /* var vehicleMovement = target.GetComponent<Car>();
         if (vehicleMovement)
             if (vehicleMovement.isReversing)
             {
                 camView = CamView.Back;
                 rotateSpeed = 1;
             }
             else
             {
                 camView = CamView.Front;
                 rotateSpeed = initialRotateSpeed;
             }*/
    }
    #endregion
}
