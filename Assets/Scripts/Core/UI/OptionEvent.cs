using TM.Manager.UI;
using TM.Manager.Game;
using UnityEngine;
using UnityEngine.UI;
using TM.Manager.Sound;

public class OptionEvent : ActionButton
{
    public GameObject optionObj = null;

    public override void OnClick()
    {
        SoundPlayer.instance.PlaySound("Popup");
        Time.timeScale = 0f;
        GameManager._instance.ShowPopUp(UIKind.Option);
    }

    public void EndOption()
    {
        Time.timeScale = 1f;
        optionObj.SetActive(false);
    }
}
