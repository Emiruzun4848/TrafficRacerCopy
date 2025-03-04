using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider carVolumeSlider;
    void Start()
    {
        carVolumeSlider.value = MyAccount.Instance.carVolume;
    }
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
#if UNITY_ANDROID
                MyAccount.Instance.SelectedInputType = InputType.Tilt;
#else
                MyAccount.Instance.SelectedInputType = InputType.Keyboard;
#endif
                break;
        }
        SetInf.Instance.Set();
    }
    public void ChangeSound(float b)
    {
        MyAccount.Instance.carVolume = b;
        SetInf.Instance.Set();
    }
}
