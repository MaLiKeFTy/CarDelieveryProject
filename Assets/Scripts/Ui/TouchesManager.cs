using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TouchesManager
{
    /// <summary>
    /// Gets all the current touches.
    /// </summary>
    public static Touch[] GetTouches(TouchPhase phase)
    {
        HashSet<Touch> touches = new HashSet<Touch>();
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                if (touch.phase == phase)
                {
                    touches.Add(touch);
                }
            }
        }
        return touches.ToArray();
    }
}
