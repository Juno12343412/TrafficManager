using TM.Manager.UI;
using TM.Manager.Game;
using UnityEngine;

public class OptionEvent : ActionButton
{
    public override void OnClick()
    {
        Time.timeScale = 0f;
        GameManager._instance.ShowPopUp(UIKind.Option);
    }

    public void EndOption()
    {
        Time.timeScale = 1f;
        GameManager._instance.ClearUI();
        GameManager._instance.ShowPopUp(UIKind.Game);
    }
}
