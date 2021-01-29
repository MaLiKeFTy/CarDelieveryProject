using UnityEngine;

public class CamFollow : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] CamFollowSettings camFollowSettings;
    [SerializeField] Transform target; //target to Follow
    #endregion

    #region Parameters
    public Transform Target => target;
    public CamFollowSettings CamFollowSettings => camFollowSettings;
    #endregion

    #region Private Fields
    CamFollowController followController;
    #endregion

    void Awake() => followController = new CamFollowController(this);

    void LateUpdate() => followController.Tick();
}
