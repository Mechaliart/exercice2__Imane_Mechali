using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Jeu : MonoBehaviour
{
    //Sons
    public AudioClip sonBonhomme;
    public AudioClip sonEchec;
    AudioSource audioSource;
    
    void Start()
    { 
           //Variable de l'audio
        audioSource = GetComponent<AudioSource>();  
    }

    void Update()
    {
   
    }

    private void RedemarrerScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
