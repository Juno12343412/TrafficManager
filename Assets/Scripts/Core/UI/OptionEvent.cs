using TM.Manager.UI;
using TM.Manager.Game;
using UnityEngine;
using UnityEngine.UI;
using TM.Manager.Sound;

public class OptionEvent : ActionButton
{
    public GameObject optionObj = null;
    private bool isPause = false;

    private void Update()
    {
        if(isPause)
        {
            if(Input.GetMouseButtonDown(0))
            {
                EndOption();
            }
        }
    }

    public override void OnClick()
    {
        if (!isPause)
        {
            isPause = true;
            SoundPlayer.instance.PlaySound("Popup");
            Time.timeScale = 0f;
            GameManager._instance.ShowPopUp(UIKind.Option);
        }
    }

    public void EndOption()
    {
        isPause = false;
        Time.timeScale = 1f;
        optionObj.SetActive(false);
    }
}
