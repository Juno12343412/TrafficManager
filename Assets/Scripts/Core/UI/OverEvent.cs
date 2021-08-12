using TM.Manager.UI;
using TM.Manager.Game;
using UnityEngine;

public class OverEvent : ActionButton
{
    public override void OnClick()
    {
        GameManager._instance.ClearUI();
        GameManager._instance.ShowPopUp(UIKind.Main);

        // 뭔가 초기화하는 코드
    }
}
