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
        }
    }
    public void ChangeSound(int a, float b)
    {
        MyAccount.Instance.Volume[a] = b;
    }
}
