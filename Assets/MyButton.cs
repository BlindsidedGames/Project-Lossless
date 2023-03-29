using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

public class MyButton : Button
{
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        if (state == SelectionState.Disabled)
        {
            ProceduralImage img = GetComponentInChildren<ProceduralImage>();
            var color = img.color;
            color.a = 0.7f;
            img.color = color;
        }
        else if (state == SelectionState.Normal)
        {
            ProceduralImage img = GetComponentInChildren<ProceduralImage>();
            var color = img.color;
            color.a = 1;
            img.color = color;
        }
    }
}
