using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class Skieur : MonoBehaviour


{
       //===== Variables publiques
     public float vitesse;
    //Mettre public pour pouvoir assigner de l'inspecteur le texte attribué
    public TMP_Text textePoint;
    public TMP_Text texteTemps;
    //Pour l'élément Canvas --> le panneau de mort et de victoire (Dans la collection)
    public GameObject PanneauMort;
    public GameObject PanneauVictoire;
   
      //Points
    public int points = 0;
    //Temps
    public float tempsPasse = 0f; 
    //booléen pour savoir si le skieur est mort ou pas
      public bool estMort = false;

    public InputAction onDeplacementVertical;
    public InputAction onDeplacementHorizontal;

    //Rigid_Body
    Rigidbody2D rigid;

    float deplacementHor = 0;
    float deplacementVert = 0;
   
 
    
    void Start()
    {
          
          //récuperer le rigidbody2D
        rigid = GetComponent<Rigidbody2D>();
      
       
        //Afficher le nombre de points
        textePoint.text = $"{points} pts";
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
    if (estMort == false)
        {
            //On change le temps affiché dans le UI
            tempsPasse += Time.deltaTime;
            texteTemps.text = $"{tempsPasse:F1}S";

            deplacementHor = onDeplacementHorizontal.ReadValue<float>();
            deplacementVert = onDeplacementVertical.ReadValue<float>();
        }
        else
        {
            deplacementHor = 0;
            deplacementVert = -0.1f;
            // if(Keyboard.current.spaceKey.wasPressedThisFrame)
            // {
            //     Invoke("RedemarrerScene", 1f);
            // }
        }
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

