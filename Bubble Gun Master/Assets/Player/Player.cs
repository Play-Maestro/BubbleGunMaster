using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject redPrefab, orangePrefab,
        yellowPrefab, greenPrefab,
        bluePrefab, purplePrefab;
    private GameObject[] bubArray = new GameObject[6];
    private float shotTimer = 0;
    public float shotVel = 20f;
    public float jumpVel = 10;
    private Animator animator;
    public EdgeSense groundSense;
    public Transform gunPoint;
    public GameObject sprites;
    public GameObject armSprite;
    public GameObject arm;
    public Transform armAncor;
    public GameObject gunBubbleRed,
        gunBubbleOrange, gunBubbleYellow,
        gunBubbleGreen, gunBubbleBlue, gunBubblePurple;
    private GameObject gunBubble;
    private GameObject[] gunBubArray = new GameObject[6];
    private bool realoading = false;
    private int selectedColor = 0;
    private AudioSource audio;
    public AudioSource jumpAudio;
    Rigidbody2D rb;
    Camera mainCamera;

    public SceneController sceneController;
    float movement = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        //Populate the arrays
        bubArray[0] = redPrefab;
        bubArray[1] = orangePrefab;
        bubArray[2] = yellowPrefab;
        bubArray[3] = greenPrefab;
        bubArray[4] = bluePrefab;
        bubArray[5] = purplePrefab;

        gunBubArray[0] = gunBubbleRed;
        gunBubArray[1] = gunBubbleOrange;
        gunBubArray[2] = gunBubbleYellow;
        gunBubArray[3] = gunBubbleGreen;
        gunBubArray[4] = gunBubbleBlue;
        gunBubArray[5] = gunBubblePurple;

        selectedColor = Random.Range(0, bubArray.Length);
        gunBubble = Instantiate(gunBubArray[selectedColor], gunPoint.position, Quaternion.identity, gunPoint);

        audio = GetComponent<AudioSource>();
    }

    public void AddCoin()
    {
        sceneController.AddCoin();
    }

    // Update is called once per frame
    void Update()
    {
        //Reaload
        if(shotTimer > 0)
        {
            shotTimer -= Time.deltaTime;
        }
        if(shotTimer <= 0 && realoading)
        {
            selectedColor = Random.Range(0, bubArray.Length);
            gunBubble = Instantiate(gunBubArray[selectedColor], gunPoint.position, Quaternion.identity, gunPoint);
            realoading = false;
        }

        //Move the player
        movement = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * movement * 0.1f);

        if(movement != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && groundSense.HasGroundToWalk() == true)
        {
            rb.AddForce(Vector2.up * jumpVel, ForceMode2D.Impulse);
            animator.SetBool("Jump", true);
            jumpAudio.Play();
        }

        animator.SetBool("Jump", !groundSense.HasGroundToWalk());

        Vector3 mousePoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //Arm stuff
        arm.transform.position = armAncor.position;
        float AngleRad = Mathf.Atan2(mousePoint.y - arm.transform.position.y, mousePoint.x - arm.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        arm.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        Vector2 mouseDirection = (Vector2)(mousePoint - armAncor.position);
        mouseDirection = mouseDirection.normalized;
        if ((mousePoint - transform.position).x < 0)
        {
            sprites.transform.localScale = new Vector3(-1,1,1);
            armSprite.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            sprites.transform.localScale = Vector3.one;
            armSprite.transform.localScale = Vector3.one;
        }
         //Shooting
        if (Input.GetButton("Fire1") && shotTimer <= 0)
        {
            shotTimer = 0.5f;
            Destroy(gunBubble);
            audio.Play();
            GameObject myBub = Instantiate(bubArray[selectedColor], gunPoint.position, Quaternion.identity);
            myBub.GetComponent<Rigidbody2D>().velocity = mouseDirection * shotVel;
            realoading = true;
        }

        sceneController.SetPlayerPosition(transform.position.x);
    }
}
