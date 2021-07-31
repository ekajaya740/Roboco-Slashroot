using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float movementSpeed {get; private set;} = 5f;
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


    private void Awake() {
        isMoveL = false;
        isMoveR = false;
        isMove = false;
        isFacingRight = true;

        
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();
        playerSlide = GetComponent<PlayerSlide>();
        playerJump = GetComponent<PlayerJump>();
    }
    
    private void Update(){
        playerAnimator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        move();
        if(isMove){
            playerSlide.isSlide = false;
        }
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
                playerSlide.slideTimer = 0;
                isMove = true;
                isFacingRight = false;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
            }else if(isMoveR){
                moveHorizontal = movementSpeed;
                playerAnimator.SetBool("isSlide", true);
                playerSlide.slideTimer = 0;
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
}
