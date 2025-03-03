using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    bool r, l, b;
    public void RightClick(bool x)
    {
        r = x;
        UpdateInput();
    }

    public void LeftClick(bool x)
    {
        l = x;
        UpdateInput();
    }

    public void BreakClick(bool y)
    {
        b = y;
        UpdateInput();
    }

    void UpdateInput()
    {
        Vector2 vec2 = Vector2.zero;
        vec2.x += r ? 1 : 0;
        vec2.x -= l ? 1 : 0;
        vec2.y = b ? -1 : 1;
        PlayerMovement.Instance.input = vec2;
    }

    private void OnEnable()
    {
        InputManager.Instance.gameObject.SetActive(false);
    }
    void OnDisable()
    {
        InputManager.Instance.gameObject.SetActive(true);
    }

}