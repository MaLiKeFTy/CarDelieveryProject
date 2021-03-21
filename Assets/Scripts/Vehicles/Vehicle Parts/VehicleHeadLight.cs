using UnityEngine;

public class VehicleHeadLight : MonoBehaviour
{
    [SerializeField] VehiclePartsPlacements placement;
    [SerializeField] bool isReverseHeadLight;

    public VehiclePartsPlacements lightPlacement { get { return placement; } set { placement = value; } }
    public bool IsReverseHeadLight { get { return isReverseHeadLight; } set { isReverseHeadLight = value; } }
}
