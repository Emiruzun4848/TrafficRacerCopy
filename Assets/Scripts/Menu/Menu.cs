using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Animator animator;
    [SerializeField] string[] animName;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayAnimation(int a)
    {
        animator.Play(animName[a]);
    }
    public void OpenGame()
    {
        SceneManager.LoadScene(1);
    }
}