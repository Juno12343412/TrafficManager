using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.Manager.Game;

public class LoadingGimori : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager._instance._setting.isRestart = true;
        GameManager._instance.ClosePopUp(UIKind.Over);
    }
    public void ResetGame()
    {
        gameObject.SetActive(false);
        GameManager._instance.ResetGame();
    }
}