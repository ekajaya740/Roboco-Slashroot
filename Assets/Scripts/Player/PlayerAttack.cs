using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public bool isAttack { get; private set;}
    private bool isMelee;
    private char whichWeaponNotBuffed;
    private char weaponNow;
    public float atkCooldown { get; private set;}
    public float atkCooldownCount;

    private float playerBaseDamage;
    public float playerDamage { get; private set;}
    private Animator playerAnimator;

    private PlayerMove playerMove;
    private PlayerJump playerJump;
    private PlayerSlide playerSlide;
    private PlayerMeleeDetect playerMeleeDetect;

    [SerializeField] private Object playerBulletRef;
    [SerializeField] private GameObject playerBulletPos;
    
    [SerializeField] private ParticleSystem weaponBuffParticle;
    private GameObject attackCooldownView;
    

    void Awake(){
        isAttack = false;
        isMelee = false;
        atkCooldown = 2.5f;
        playerBaseDamage = 5f;
        atkCooldownCount = 0f;
        
    }

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerJump = GetComponent<PlayerJump>();
        playerSlide = GetComponent<PlayerSlide>();
        playerAnimator = GetComponent<Animator>();
        playerMeleeDetect = GetComponent<PlayerMeleeDetect>();
    }

    void Update()
    {
        if(attackCooldownView == null){
            attackCooldownView = GameObject.Find("AttackCooldown");
        }

        if(atkCooldownCount <= 0f){
            attackCooldownView.SetActive(false);
        }

        var weaponBuff = weaponBuffParticle.main;
        weaponBuff.startColor = WeaponParticle();
        attackCooldownHandler();
        attackHandler();
    }

    private void attackHandler(){
        isMelee = playerMeleeDetect.isMelee;
        if(isAttack && atkCooldownCount <= 0f && !playerSlide.isSlide && !playerMove.isMove){
            playerAnimator.SetTrigger("attack");

            if(isMelee && playerJump.isGrounded()){
                weaponNow = 'M';
                playerAnimator.SetBool("isMelee", true);
                
            }else if(!isMelee){
                weaponNow = 'R';
                playerAnimator.SetBool("isMelee", false);
                GameObject bulletObj = (GameObject)Instantiate(playerBulletRef);

                if(playerMove.isFacingRight){
                    bulletObj.transform.right = transform.right.normalized;
                    bulletObj.transform.rotation = Quaternion.Euler(transform.eulerAngles.y, 0f, transform.eulerAngles.z);

                }else if(!playerMove.isFacingRight){
                    bulletObj.transform.rotation = Quaternion.Euler(transform.eulerAngles.y, 180f, transform.eulerAngles.z);
                }

                bulletObj.transform.position = new Vector3(playerBulletPos.transform.position.x, playerBulletPos.transform.position.y, playerBulletPos.transform.position.z);
            }

            attackDamageCalc();
            isAttack = false;
            atkCooldownCount = atkCooldown;
        }
    }

    private void attackCooldownHandler(){
        if(atkCooldownCount >= 0f){
            atkCooldownCount -= Time.fixedDeltaTime;
            isAttack = false;
            attackCooldownView.SetActive(true);
        }

        
    }
    
    private void attackDamageCalc(){
        if(whichWeaponNotBuffed != weaponNow){
            playerDamage = playerBaseDamage * 100;
            whichWeaponNotBuffed = weaponNow;
        }else if (whichWeaponNotBuffed == weaponNow){
            playerDamage = playerBaseDamage;
        }
    }

    public void PointerAttack(){
        isAttack = true;
    }

    private Color WeaponParticle(){
        Color meleeBuff = new Color(255,0,0,255);
        Color rangedBuff = new Color(0,0,255,255);

        if(whichWeaponNotBuffed == 'M'){
            return rangedBuff;
        }else if(whichWeaponNotBuffed == 'R'){
            return meleeBuff;
        }
        return new Color(0,0,0,0);
    }
}
