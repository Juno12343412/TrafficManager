using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.Manager.Sound;

public class Title : MonoBehaviour
{
    public void TitleSound()
    {
        SoundPlayer.instance.PlaySound("Popup");
    }
}
