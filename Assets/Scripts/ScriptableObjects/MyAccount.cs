using UnityEngine;
using UnityEngine.InputSystem;


public enum InputType{
    Keyboard,
    JoyStick,
    Button
}
[CreateAssetMenu(fileName = "Account", menuName = "Account", order = 0)]
public class MyAccount : ScriptableObject
{
    private static MyAccount instance;

    public static MyAccount Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<MyAccount>("MyAccount"); // "Resources/Account.asset" dosyasını yükler.
            }
            return instance;
        }
    }
    public int Money;
    public int HighScore;
    public CarData SelectedCar;

    public InputType SelectedInputType=InputType.JoyStick;

    public float[] Volume=new float[3];
}
