using UnityEngine;
using UnityEngine.UI;

public class UI_AutoButton : UI_ButtonParent
{
    protected override void InitExecute()
    {
        _isPressed = Player.Stat.IsBulletAutoFireOn;
    }

    protected override void ClickExecute()
    {
        Player.SwitchAutoPlayMode();
    }
}