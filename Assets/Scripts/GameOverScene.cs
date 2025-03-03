using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    public void Open()
    {

        Animator animator = GetComponent<Animator>();
        animator.Play("SetPlace");

    }
    public void LoadText(string Money, string score, string HighScore)
    {
        moneyText.text = Money;
        scoreText.text = score;
        highScoreText.text = HighScore; 
    }
    public void LoadScene(int ind)
    {
        SceneManager.LoadScene(ind);
    }
}