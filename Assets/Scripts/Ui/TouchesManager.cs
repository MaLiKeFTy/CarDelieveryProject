using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchesManager
{
    public TouchesManager()
    {
    }

    public Touch[] GetTouches(TouchPhase phase)
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
