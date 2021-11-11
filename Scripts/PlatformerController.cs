using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformerController : MonoBehaviour
{

    //Speed of character movement and height of the jump. Set these values in the inspector.
    public float speed;
    public float jumpHeight;

    //Assigning a variable where we'll store the Rigidbody2D component.
    private Rigidbody2D rb;

    public Animator animator;
    private float horizontalMove = 0f;

    private bool onGround;
    private bool canJump;
    private bool isJumping;
    public bool isAttacking = false;
    private bool isFacingRight = true;
    public bool isDead = false;
    public bool isFighting = false;

    public int loot;
    public int lives = 5;
    public int numAttacks = 0;

    private UIManager _uiManager;
    [SerializeField]
    private AudioSource _attackSound;
    [SerializeField]
    private AudioSource _deathSound;
    [SerializeField]
    private AudioSource _backgroundMusic;
    [SerializeField]
    private AudioSource _victoryMusic;
    [SerializeField]
    private AudioSource _lossMusic;

    [SerializeField]
    private Transform _startPos;

    [SerializeField]
    private GameObject _loserScreen;
    [SerializeField]
    private GameObject _winnerScreen;

    public bool isWinner = false;

    private bool canMove = true;






    // Start is called before the first frame update
    void Start()
    {
        //Sets our variable 'rb' to the Rigidbody2D component on the game object where this script is attached.
        rb = GetComponent<Rigidbody2D>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Calculates direction and changes sprite to face that direction
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (canMove)
        {
            if (isFacingRight)
            {
                transform.localScale = new Vector3(.5f, .5f, .5f);
            }
            else
            {
                transform.localScale = new Vector3(-.5f, .5f, .5f);
            }

            //Check if the player is on the ground. If we are, then we are able to jump.
            if (onGround == true)
            {
                canJump = true;
                animator.SetBool("isJumping", false);
            }
            //If we're able to jump and the player has pressed the space bar, then we jump!
            if (Input.GetKey(KeyCode.Space) && canJump == true)
            {
                animator.SetBool("isJumping", true);
                rb.velocity = Vector2.up * jumpHeight;
                canJump = false;
            }

            //Movement code for left and right arrow keys.
            if (Input.GetKey(KeyCode.A))
            {
                isFacingRight = false;
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                isFacingRight = true;
                rb.velocity = new Vector2(+speed, rb.velocity.y);
            }
            //ELSE if we're not pressing an arrow key, our velocity is 0 along the X axis, and whatever the Y velocity is (determined by jump)
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }


            if (Input.GetMouseButtonDown(0))
            {
                isAttacking = true;
                animator.SetBool("isAttacking", true);
                if (isFighting)
                {
                    _attackSound.Play();
                    numAttacks++;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isAttacking = false;
                animator.SetBool("isAttacking", false);
            }
        }
        
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If we collide with an object tagged "ground" then our jump resets and we can now jump.
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            print(onGround);
            //print statements print to the Console panel in Unity. 
            //This will print the value of onGround, which is a boolean, so either True or False.
        }
        else if(collision.gameObject.tag == "Coins")
        {
            loot += 5;
            _uiManager.UpdateCoinDisplay(loot);
        }
        else if (collision.gameObject.tag == "Chest")
        {
            loot += 25;
            _uiManager.UpdateCoinDisplay(loot);
        }
        else if(collision.gameObject.tag == "Boat")
        {
            isWinner = true;
            _backgroundMusic.Stop();
            _victoryMusic.Play();
            GameOver();

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //If we exit our collision with the "ground" object, then we are unable to jump.
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
            print(onGround);
        }
    }

    public void Death()
    {
        if (lives > 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            lives--;
            _deathSound.Play();
            _uiManager.UpdateLivesDisplay(lives);
            StartCoroutine(Respawn(2f));
        }
        else if (lives == 0)
        {
            _backgroundMusic.Stop();
            _lossMusic.Play();
            GameOver();
        }
    }

    IEnumerator Respawn(float waitTime)
    {
        // suspend execution for 2 seconds
        yield return new WaitForSeconds(waitTime);
        isDead = false;
        animator.SetBool("isDead", false);
        transform.position = _startPos.transform.position;
        numAttacks = 0;
    }
    

    public void StartOver()
    {
        SceneManager.LoadScene(0);
    }


    private void GameOver()
    {
        if (lives <= 0) {
            _loserScreen.SetActive(true);
            canMove = false;
        }else if (isWinner)
        {
            _winnerScreen.SetActive(true);
            _uiManager.YouWin(loot);
            canMove = false;
        }
    }
}
