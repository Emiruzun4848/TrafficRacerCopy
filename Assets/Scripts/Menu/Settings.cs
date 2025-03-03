using System.Diagnostics;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public void ChangeInputType(int a)
    {
        switch (a)
        {
            case 0:
                MyAccount.Instance.SelectedInputType = InputType.Keyboard;
                break;
            case 1:
                MyAccount.Instance.SelectedInputType = InputType.JoyStick;
                break;
            case 2:
                MyAccount.Instance.SelectedInputType = InputType.Button;
                break;
            case 3:
                MyAccount.Instance.SelectedInputType = InputType.Tilt;
                break;
        }
    }
    public void ChangeSound(float b)
    {
        MyAccount.Instance.carVolume = b;
    }
}
