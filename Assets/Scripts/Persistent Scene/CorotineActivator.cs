using System.Collections;
using UnityEngine;

public class CorotineActivator : MonoBehaviour
{
    public static CorotineActivator instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public void ActivateCorotine(IEnumerator original, ref IEnumerator corotineToStop)
    {
        if (corotineToStop != null)
            StopCoroutine(corotineToStop);
        corotineToStop = original;
        StartCoroutine(corotineToStop);
    }

}
