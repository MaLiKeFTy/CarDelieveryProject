using System;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUiController
{
    readonly FloatingUiCorotine floatingUiCorotine;
    readonly RectTransform rectToMove;
    readonly CanvasGroup alphaToggle;
    readonly float transparencyAmount;
    readonly bool isLeft;

    TouchesManager touchesManager = new TouchesManager();


    static HashSet<FloatingUiController> floatingGameObjs = new HashSet<FloatingUiController>();
    public static Action<string> TouchedEvent;

    public FloatingUiController(RectTransform rectToMove, CanvasGroup alphaToggle, float transparencyAmount, bool isLeft)
    {
        floatingUiCorotine = new FloatingUiCorotine(CorotineActivator.instance);

        this.rectToMove = rectToMove;
        this.alphaToggle = alphaToggle;
        this.transparencyAmount = transparencyAmount;
        this.isLeft = isLeft;
    }

    public void FloatUi()
    {
        OnTouchDown();
        OnTouchUp();
    }

    void OnTouchDown()
    {
        floatingGameObjs.Add(this);

        foreach (var touch in touchesManager.GetTouches(TouchPhase.Began))
        {

            FloatingUiController closestFloatingUi;

            if (touch.position.x <= Screen.width / 2)
                closestFloatingUi = GetClosestFloatingUi(FilterList(true), touch.position);
            else
                closestFloatingUi = GetClosestFloatingUi(FilterList(false), touch.position);



            closestFloatingUi.rectToMove.position = touch.position;
            closestFloatingUi.alphaToggle.alpha = transparencyAmount;


            /*try and fix the corotine Issue*/

            /*hint: the corotine is stopping the other joystick corotine*/


            //floatingUiCorotine.ActivateMoveToTouch(closestFloatingUi.rectToMove, touch.position, 2);
            // floatingUiCorotine.ActivateAplhaToggle(closestFloatingUi.alphaToggle, transparencyAmount, 1);
        }
    }

    FloatingUiController GetClosestFloatingUi(HashSet<FloatingUiController> floatingUiList, Vector2 touchPos)
    {
        FloatingUiController tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = touchPos;
        foreach (FloatingUiController t in floatingUiList)
        {
            float dist = Vector3.Distance(t.rectToMove.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    HashSet<FloatingUiController> FilterList(bool isLeft)
    {
        HashSet<FloatingUiController> tempFloatingUi = new HashSet<FloatingUiController>();
        foreach (var item in floatingGameObjs)
        {
            if (item.isLeft == isLeft)
            {
                tempFloatingUi.Add(item);
            }
        }
        return tempFloatingUi;
    }

    void OnTouchUp()
    {
        foreach (var touch in touchesManager.GetTouches(TouchPhase.Ended))
        {
            FloatingUiController closestFloatingUi;

            if (touch.position.x <= Screen.width / 2)
                closestFloatingUi = GetClosestFloatingUi(FilterList(true), touch.position);
            else
                closestFloatingUi = GetClosestFloatingUi(FilterList(false), touch.position);

            closestFloatingUi.alphaToggle.alpha = 0;
            
            /*try and fix the corotine Issue*/

            /*hint: the corotine is stopping the other joystick corotine*/

            // floatingUiCorotine.ActivateAplhaToggle(closestFloatingUi.alphaToggle, transparencyAmount, 1);
        }
    }

}
