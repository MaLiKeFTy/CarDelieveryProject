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

    protected override void Awake() => base.Awake();

    void Update()
    {
        UiElementTouchSelector.uiElements.Add(this);
#if UNITY_EDITOR
        OnMouse();
#else
        OnTouch();
#endif
    }

    void OnTouch()
    {
        OnTouchDown();
        OnTouchUp();
    }


    void OnTouchDown()
    {
        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Began))
        {
            var selectedUiElement = UiElementTouchSelector.SelectedUiElement(touch.position);

            if (!(selectedUiElement is VehicleToggleUiOperator))
                return;

            selectedToggleOperator = (VehicleToggleUiOperator)selectedUiElement;
            selectedToggleOperators.Add(selectedToggleOperator);
            selectedToggleOperator.OperationToggle = true;

        }
    }


    void OnTouchUp()
    {
        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Ended))
        {
            if (selectedToggleOperator)
                selectedToggleOperator.OperationToggle = false;
        }
    }


    void OnMouse()
    {
        OnMouseBtnDown();
        OnMouseBtnUp();
    }


    void OnMouseBtnDown()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        var selectedUiElement = UiElementTouchSelector.SelectedUiElement(Input.mousePosition);
        if (!(selectedUiElement is VehicleToggleUiOperator))
            return;
        selectedToggleOperator = (VehicleToggleUiOperator)selectedUiElement;
        selectedToggleOperators.Add(selectedToggleOperator);
        selectedToggleOperator.OperationToggle = true;
    }

    void OnMouseBtnUp()
    {
        if (!selectedToggleOperator || !Input.GetMouseButtonUp(0))
            return;
        selectedToggleOperator.OperationToggle = false;
    }

   
}
