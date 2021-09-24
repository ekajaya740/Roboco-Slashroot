using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float movementSpeed;
    public float initialMovementSpeed {get; private set;}
    private float moveHorizontal;

    private bool isMoveL;
    private bool isMoveR;
    public bool isMove {get; private set;}

    public bool isFacingRight {get; private set;}

    private BoxCollider2D playerCollider;
    private Rigidbody2D playerRB;
    private Animator playerAnimator;

    private PlayerJump playerJump;
    private PlayerSlide playerSlide;
    [SerializeField] private EnemyHealth enemyHealth;
    
    [SerializeField] private BoxCollider2D enemyCollider;
    public bool isFalling;



    private void Awake() {
        isMoveL = false;
        isMoveR = false;
        isMove = false;
        isFalling = false;
        isFacingRight = true;
        initialMovementSpeed = 5f;

    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();
        playerSlide = GetComponent<PlayerSlide>();
        playerJump = GetComponent<PlayerJump>();
        movementSpeed = initialMovementSpeed;
    }
    
    private void Update(){
        if(playerSlide.isSlide){

            playerAnimator.SetFloat("Speed", 0);
        }else{
            playerAnimator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        }
        move();
        SuperSimpleSecure();
    }

    private void FixedUpdate(){
        playerRB.velocity = new Vector2(moveHorizontal, playerRB.velocity.y);

    }

    // Move
    public void move(){
        if(!this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            if(isMoveL){
                moveHorizontal = -movementSpeed;
                playerAnimator.SetBool("isSlide", true);
                isMove = true;
                isFacingRight = false;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
            }else if(isMoveR){
                moveHorizontal = movementSpeed;
                playerAnimator.SetBool("isSlide", true);
                isMove = true;
                isFacingRight = true;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
            }else{
                isMove = false;
                moveHorizontal = 0;
            }
        }
    }

    // Pointers
    public void PointerDownMoveL(){
        isMoveL = true;
    }

    public void PointerUpMoveL(){
        isMoveL = false;
    }

    public void PointerDownMoveR(){
        isMoveR = true;
    }

    public void PointerUpMoveR(){
        isMoveR = false;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Enemy" && !enemyHealth.isDead){
            Physics2D.IgnoreCollision(playerCollider, enemyCollider);


        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Fall"){
            isFalling = true;
        }
    }

    private void SuperSimpleSecure(){
        if(initialMovementSpeed > 5f){
            initialMovementSpeed = 0f;
        }
    }
}
