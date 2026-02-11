using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("---- Audio Source ----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---- Audio Clip ----")]
    public AudioClip background;





    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    // Update is called once per frame
    
}
