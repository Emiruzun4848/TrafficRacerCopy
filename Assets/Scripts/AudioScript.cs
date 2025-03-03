using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public static AudioScript Instance;
    public AudioClip normalClip;
    public AudioSource carSource;
    PlayerMovement playerMovement;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        playerMovement = GameObject.FindAnyObjectByType<PlayerMovement>();
        carSource.volume = MyAccount.Instance.carVolume;
    }
    private void Start()
    {
        carSource.clip = normalClip;
    }
    private void Update()
    {
        if(GameManager.Instance.isGameEnded){
            carSource.Stop();
            return;
        }
        if (playerMovement == null)
        {
            playerMovement = GameObject.FindAnyObjectByType<PlayerMovement>();
            return;
        }
        float t = playerMovement.rb.velocity.z;

        if (carSource.clip != normalClip)
            carSource.clip = normalClip;
        carSource.pitch = 0.2f + (t / playerMovement.maxSpeed);

        if (!carSource.isPlaying)
            carSource.Play();
    }
    public void ChangeMuteState(bool state)
    {
        carSource.mute = state;
    }

}
