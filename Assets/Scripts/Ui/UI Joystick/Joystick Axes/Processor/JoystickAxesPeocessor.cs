using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;
using System;

public class JoystickAxesPeocessor
{
    static Dictionary<AxesTypes, JoystickAxes> joystickAxes = new Dictionary<AxesTypes, JoystickAxes>();
    static bool isInitialised;

    static void Initialise()
    {
        joystickAxes.Clear();

        var assembly = Assembly.GetAssembly(typeof(JoystickAxes));

        var allCamViewModes = assembly.GetTypes()
            .Where(t => typeof(JoystickAxes).IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var camViewMode in allCamViewModes)
        {
            JoystickAxes joystickAxis = Activator.CreateInstance(camViewMode) as JoystickAxes;
            joystickAxes.Add(joystickAxis.AxesTypes, joystickAxis);
        }
    }

    public static Vector2 GetAxisTarget(RectTransform joystickRect, RectTransform backgroundRect, AxesTypes axesTypes)
    {
        if (!isInitialised)
            Initialise();

        var joystickAxis = joystickAxes[axesTypes];
        return joystickAxis.AxisSelection(joystickRect, backgroundRect);
    }
}
