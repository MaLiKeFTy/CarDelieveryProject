using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;
using System;

public static class CamViewModesProcessor
{
    static Dictionary<CamViewModes, CamView> camModes = new Dictionary<CamViewModes, CamView>();
    static bool isInitialised;


    static void Initialise()
    {
        camModes.Clear();

        var assembly = Assembly.GetAssembly(typeof(CamView));

        var allCamViewModes = assembly.GetTypes()
            .Where(t => typeof(CamView).IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var camViewMode in allCamViewModes)
        {
            CamView camView = Activator.CreateInstance(camViewMode) as CamView;
            camModes.Add(camView.CamViewModes, camView);
        }
    }

    public static void ChangeCamView(Transform objTrans, Transform target, CamFollowSettings camFollowSettings, CamViewModes camViewMode)
    {
        if (!isInitialised)
            Initialise();

        var viewMode = camModes[camViewMode];
        viewMode.ViewMode(objTrans, target, camFollowSettings);
    }
}
