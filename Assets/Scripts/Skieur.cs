using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class Skieur : MonoBehaviour


{
    public float vitesse;

    public InputAction onDeplacementVertical;
    public InputAction onDeplacementHorizontal;


    Rigidbody2D rigid;

    float deplacementHor = 0;
    float deplacementVert = 0;
    void Start()
    {
          rigid = GetComponent<Rigidbody2D>();
    }

    // Utiliser ces fonctions pour activer et desactiver les InputActions
    private void OnEnable()
    {
        onDeplacementHorizontal.Enable();
        onDeplacementVertical.Enable();

    }

    private void OnDisable()
    {
        onDeplacementHorizontal.Disable();
        onDeplacementVertical.Disable();


    }
void Update(){
    rigid.linearVelocity += new Vector2(
            deplacementHor * vitesse,
            deplacementVert * vitesse
            );
}


    // Il faut appeller cette fonction dans la collision avec le yeti.
    void DeconnecterCamera()
    {
        Camera.main.GetComponent<PositionConstraint>().enabled = false;
    }
}

// IMANE, ENLEVE LES SIGNES D'INTERROGATION, CELA INDIQUE CHATGPT