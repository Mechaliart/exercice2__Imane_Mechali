using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Skieur : MonoBehaviour


{
    //===== Variables publiques
    public float vitesse;
    public float vitesseverticale;
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
    public bool estGagnant = false;
    public InputAction onDeplacementVertical;
    public InputAction onDeplacementHorizontal;

    //Rigid_Body
    Rigidbody2D rigid;

    float deplacementHor = 0;
    float deplacementVert = 0;
    //Sons


    AudioSource audioSource;
    public AudioClip sonPointCollision;
    public AudioClip sonMort;
    public AudioClip sonVictoire;




    void Start()
    {

        //récuperer le rigidbody2D
        rigid = GetComponent<Rigidbody2D>();


        //Afficher le nombre de points
        textePoint.text = $"{points} pts";
        //sons
        audioSource = GetComponent<AudioSource>();
    }

    // Utiliser ces fonctions pour activer et desactiver les touches de clavier
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
    void Update()
    {
        /*Si le personnage est mort*/
        if (estMort == false && estGagnant == false)
        {
            //On change le temps affiché dans le UI
            tempsPasse += Time.deltaTime;
            texteTemps.text = $"{tempsPasse:F1}S";
            //Récupérer les valeurs des InputActions (touches)

            deplacementHor = onDeplacementHorizontal.ReadValue<float>();
            deplacementVert = onDeplacementVertical.ReadValue<float>();
        }
        else
        {   /*Si le personnage est mort ne plus pouvoir se déplacer et si on clique sur espace
        redemarrer le jeu*/
            deplacementHor = 0;
            deplacementVert = 0;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Invoke("RedemarrerScene", 1f);
            }
        }
        //Déplacer le skieur en fonction des touches appuyées
        rigid.linearVelocity = new Vector2(
                deplacementHor * vitesse,
                deplacementVert * vitesseverticale
                );

    }

    void RedemarrerScene()
    {
        string nomSceneCourante = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomSceneCourante);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Quand le skieur rentre en collision avec le yeti (avec le tag catégorie danger) le skieur devient mort
        /*la caméra se déconnecte*/
        if (estMort == false && collision.gameObject.tag == "Danger")
        {
            audioSource.PlayOneShot(sonMort);

            /*le skieur devient mort*/
            estMort = true;
            //afficher la fonction pour afficher le panneau de mort
            Mourir();
            //la caméra se déconnecte
            DeconnecterCamera();
        }


    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        /*===================================Ligne d'arrivée===========================*/
        //Quand le skieur rentre en collision avec le drapeau (avec le tag catégorie Finish) le skieur gagne et la caméra se déconnecte
        if (estGagnant == false && collision.gameObject.tag == "Finish")
        {
            estGagnant = true;
            Victoire();
            DeconnecterCamera();
            audioSource.PlayOneShot(sonVictoire);
        }
        /*==================================Portails=============================*/

        /*Quand ca rentre en contact avec le box collider du portail augmenter de points*/
        if (estMort == false && collision.gameObject.tag == "Points")
        {
            // Debug.Log("Collision avec: portail" + collision.gameObject.name);
            points += 1;
            textePoint.text = $"{points} pts";
            audioSource.PlayOneShot(sonPointCollision);
        }

        /*==========================bonhommes de neige=============================*/

        /*Quand on rentre en collision avec un bonhomme de neige (avec le tag "collectable") On ajoute un point et on désactive(cache l'objet)*/
        if (estMort == false && collision.gameObject.CompareTag("Collectable"))
        {
            points += 1;
            textePoint.text = $"{points} pts";
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(sonVictoire);
            // Debug.Log("Collision avec: bonhomme" + collision.gameObject.name);
        }
    }

    //fonction pour afficher le panel de mort
    void Mourir()
    {
        PanneauMort.SetActive(true);
    }
    //fonction pour afficher le panel pour la victoire
    void Victoire()
    {
        PanneauVictoire.SetActive(true);
    }
    // Il faut appeller cette fonction dans la collision avec le yeti.
    void DeconnecterCamera()
    {
        Camera.main.GetComponent<PositionConstraint>().enabled = false;
    }
}

