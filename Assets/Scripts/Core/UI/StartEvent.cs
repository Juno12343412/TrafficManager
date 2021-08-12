using TM.Manager.UI;
using TM.Manager.Game;

public class StartEvent : ActionButton
{
    public override void OnClick()
    {
        GameManager._instance.ClearUI();
        GameManager._instance.ShowPopUp(UIKind.Game);

        // 뭔가 싸이클 시작하는 코드 쓰기
    }
}
