using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

public class MyButton : Button
{
    public bool invertInteractableColor;

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        if (state == SelectionState.Disabled)
        {
            var img = GetComponentInChildren<ProceduralImage>();
            var color = img.color;
            color.a = invertInteractableColor ? 0.7f : 1f;
            img.color = color;
        }
        else if (state == SelectionState.Normal)
        {
            var img = GetComponentInChildren<ProceduralImage>();
            var color = img.color;
            color.a = invertInteractableColor ? 1f : 0.7f;
            img.color = color;
        }
    }
}