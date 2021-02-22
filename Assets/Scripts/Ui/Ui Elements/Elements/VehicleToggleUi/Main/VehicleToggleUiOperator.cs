using System.Collections.Generic;
using UnityEngine;

public class VehicleToggleUiOperator : UiElement
{
    [SerializeField] VehicleToggleOperations operation;

    public bool OperationToggle { get; private set; }

    public VehicleToggleOperations Operation => operation;

    public override RectTransform ThisRect => thisRect;

    public override bool IsLeft => isLeft;

    public static HashSet<VehicleToggleUiOperator> selectedToggleOperators = new HashSet<VehicleToggleUiOperator>();

    VehicleToggleUiOperator selectedToggleOperator;

    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {

        UiElementTouchSelector<VehicleToggleUiOperator>.uiElements.Add(this);

        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Began))
        {
            selectedToggleOperator = UiElementTouchSelector<VehicleToggleUiOperator>.SelectedUiElement(touch);
            selectedToggleOperators.Add(selectedToggleOperator);
            selectedToggleOperator.OperationToggle = true;
        }

        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Ended))
        {
            selectedToggleOperator.OperationToggle = false;
        }

    }
}
