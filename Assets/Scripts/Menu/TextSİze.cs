using TMPro;
using UnityEngine;

public class TextSİze : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] texts;

    private void Start()
    {
        SynchronizeTextsSize();
    }
    void SynchronizeTextsSize()
    {
        float min = Mathf.Infinity;
        for (int i = 0; i < texts.Length; i++)
        {
            if (min > texts[i].fontSize)
                min = texts[i].fontSize;

        }
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].enableAutoSizing = false;
            texts[i].fontSize = min;
        }
    }
}