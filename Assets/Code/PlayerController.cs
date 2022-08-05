using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Rigidbody2D rb;
    public Scene scene;
    public int _countKillEnemy = 0;

    private AudioSource audioSource;
    public AudioClip[] Hit_OutClips;
    public AudioClip[] Hit_InClips;
    public AudioClip[] Hurt_Sounds;
    public AudioSource Death_Sound;

    float HorizontalMove;
    float VerticalMove;

    public Animator animator;
    public bool FacingRight = true;

    public KeyCode[] comboKeyArm;
    public KeyCode[] comboKeyLeg;
    public KeyCode[] comboKeyArmAndLeg;

    public int currentIndexArm = 0;
    public int currentIndexLeg = 0;
    public int currentIndexArmAndLeg = 0;

    public bool isComboAttaking = false;
    public bool isAttaking = false;

    public int _health;
    public int _maxHealth;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamageArm;
    public int attackDamageLeg;
    public int attackDamageCombo;
    public LayerMask enemyLayers;
    public bool isPause = false;

    public Animator camAnimator;
    public Animator bossFightCam;
    public PlayerController player;

    [Header("SaveSystem")]
    public GameObject[] AllTriggers;
    public GameObject[] AllMessages;
    public int countTriggers;
    public int countMessages;

    void Start()
    {
        _health = _maxHealth;
        audioSource = GetComponent<AudioSource>();
        scene = SceneManager.GetActiveScene();
        player = this;
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.H))
        {
            AttackCombo();
            AttackLeg();
            AttackArm();
        }
        AllTriggers = GameObject.FindGameObjectsWithTag("Trigger");
        AllMessages = GameObject.FindGameObjectsWithTag("Message");
        countTriggers = AllTriggers.Length;
        countMessages = AllMessages.Length;
        Walk();
        if (Input.GetKeyDown(KeyCode.P))
        {
            _health += 25;
        }
    }

    public void Hit_Out_Play()
    {
        audioSource.PlayOneShot(Hit_OutClips[Random.Range(0, Hit_OutClips.Length)]);
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(player);
    }

    public void LoadSaveObjects()
    {
        if (countTriggers != 0)
        {
            for (int i = 0; i < (AllTriggers.Length - countTriggers); i++)
            {
                AllTriggers[i].SetActive(false);
            }
        }
        else if (countTriggers == 0)
        {
            for (int i = 0; i < AllTriggers.Length; i++)
            {
                AllTriggers[i].SetActive(false);
            }
        }
        if (countMessages != 0)
        {
            for (int i = 0; i < (AllMessages.Length - countMessages); i++)
            {
                AllMessages[i].SetActive(false);
            }
        }
        else if (countMessages == 0)
        {
            for (int i = 0; i < AllMessages.Length; i++)
            {
                AllMessages[i].SetActive(false);
            }
        }
    }

    public void TakingDamage(int damage)
    {
        _health -= damage;
        int rnd = Random.RandomRange(1, 10);
        if (rnd % 2 == 0)
        {
            audioSource.PlayOneShot(Hurt_Sounds[Random.Range(0, Hurt_Sounds.Length)]);
            camAnimator.SetTrigger("Shake_Hurt");
        }
        if (_health <= 0)
        {
            Death_Sound.Play();
            gameObject.SetActive(false);
            SceneManager.LoadScene(1);
        }
    }

    public void ToDamage(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if(hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                if (ChechHitRange(enemy.gameObject.GetComponents<BoxCollider2D>()[1]))
                {
                    if (enemy.gameObject.name == "Boss")
                    {
                        bossFightCam.SetTrigger("Shake");
                        enemy.GetComponent<EnemyAI>().TakingDamage(damage, "EnemyDead", "EnemyHurt");
                        audioSource.PlayOneShot(Hit_InClips[Random.Range(0, Hit_InClips.Length)]);
                    }
                    else if (enemy.gameObject.name == "Skeleton" || enemy.gameObject.name == "Skeleton(Clone)")
                    {
                        camAnimator.SetTrigger("Shake");
                        enemy.GetComponent<EnemyAI>().TakingDamage(damage, "SkeletonDead", "SkeletonHurt");
                        audioSource.PlayOneShot(Hit_InClips[Random.Range(0, Hit_InClips.Length)]);
                    }
                    else if (enemy.gameObject.name == "SkeletonHeavy" || enemy.gameObject.name == "SkeletonHeavy(Clone)")
                    {
                        camAnimator.SetTrigger("Shake");
                        enemy.GetComponent<EnemyAI>().TakingDamage(damage, "SkeletonHeavyDead", "SkeletonHeavyHurt");
                        audioSource.PlayOneShot(Hit_InClips[Random.Range(0, Hit_InClips.Length)]);
                    }
                }
                //enemy.GetComponent<EnemyAI>().TakingDamage(damage);
                //audioSource.PlayOneShot(Hit_InClips[Random.Range(0, Hit_InClips.Length)]);
            }
        }
        else if (hitEnemies.Length == 0)
        {
            audioSource.PlayOneShot(Hit_OutClips[Random.Range(0, Hit_OutClips.Length)]);
        }
    }

    bool ChechHitRange(BoxCollider2D _currentEnemyPos)
    {
        if (Mathf.Abs(Mathf.Abs(this.gameObject.GetComponents<BoxCollider2D>()[1].transform.position.y) - Mathf.Abs(_currentEnemyPos.transform.position.y)) <= 40f)
        {
            return true;
        }
        else
            return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Walk()
    {
        if (!isPause)
        {
            HorizontalMove = Input.GetAxisRaw("Horizontal");
            direction.x = Input.GetAxisRaw("Horizontal");
            VerticalMove = Input.GetAxisRaw("Vertical");
            direction.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("HorizontalMove", Mathf.Abs(HorizontalMove));
            animator.SetFloat("VerticalMove", Mathf.Abs(VerticalMove));
            if (HorizontalMove > 0 && FacingRight)
            {
                Flip();
            }
            else if (HorizontalMove < 0 && !FacingRight)
            {
                Flip();
            }
        }
    }

    void AttackArm()
    {
        if (currentIndexArm <= comboKeyArm.Length && isComboAttaking == false && isAttaking == false && isPause == false)
        {
            if (Input.GetKeyDown(comboKeyArm[currentIndexArm]))
            {
                isAttaking = true;
                currentIndexArm++;
                if (currentIndexArm == 1)
                {
                    animator.SetTrigger("HitArm");
                    StartCoroutine(WaitForHitArm());
                }
                if (currentIndexArm == 2)
                {
                    animator.SetTrigger("HitArm");
                    StopCoroutine(BetweenHitArm());
                    animator.SetTrigger("HitArm2");
                    StartCoroutine(WaitAttack(0.85f));
                    StartCoroutine(WaitCancelAttack(0.65f));
                    AttackArm();
                }
            }
        }
    }

    IEnumerator BetweenHitArm()
    {
        yield return new WaitForSeconds(0.55f);
    }
    void AttackLeg()
    {
        if (currentIndexLeg <= comboKeyLeg.Length && isComboAttaking == false && isAttaking == false && isPause == false)
        {
            if (Input.GetKeyDown(comboKeyLeg[currentIndexLeg]))
            {
                isAttaking = true;
                currentIndexLeg++;
                if (currentIndexLeg == 1)
                {
                    animator.SetTrigger("HitLeg");
                    StartCoroutine(WaitForHitLeg());
                }
                if (currentIndexLeg == 2)
                {
                    animator.SetTrigger("HitLeg");
                    StartCoroutine(BetweenHitLeg());
                    animator.SetTrigger("HitLeg2");
                    StartCoroutine(WaitAttack(0.85f));
                    StartCoroutine(WaitCancelAttack(0.85f));
                    AttackLeg();
                }
            }
        }
    }

    void CameraShake(int countShakes)
    {
        camAnimator.SetTrigger("Shake");
    }

    IEnumerator BetweenHitLeg()
    {
        yield return new WaitForSeconds(0.55f);
    }

    void AttackCombo()
    {
        if (currentIndexArmAndLeg <= comboKeyArmAndLeg.Length && isComboAttaking == false && isAttaking == false && isPause == false)
        {
            if (Input.GetKeyDown(comboKeyArmAndLeg[currentIndexArmAndLeg]))
            {
                currentIndexArmAndLeg++;
                if (currentIndexArmAndLeg == 4)
                {
                    isComboAttaking = true;
                    animator.SetTrigger("HitCombo");
                    StartCoroutine(WaitComboAttack(1.85f));
                    StartCoroutine(WaitCancelComboAttack(1.85f));
                }
                else
                {
                    StartCoroutine(WaitForHitCombo());
                }
            }
        }
    }

    IEnumerator WaitForHitArm()
    {
        yield return new WaitForSeconds(0.35f);
        if (Input.GetKeyDown(comboKeyLeg[currentIndexArm]))
        {
            AttackArm();
        }
        else
        {
            StartCoroutine(WaitAttack(0.05f));
            StartCoroutine(WaitCancelAttack(0.65f));
        }
    }

    IEnumerator WaitForHitLeg()
    {
        yield return new WaitForSeconds(0.45f);
        if (Input.GetKeyDown(comboKeyLeg[currentIndexArm]))
        {
            AttackLeg();
        }
        else
        {
            StartCoroutine(WaitAttack(0.55f));
            StartCoroutine(WaitCancelAttack(1.25f));
        }
    }

    IEnumerator WaitForHitCombo()
    {
        yield return new WaitForSeconds(2.0f);
        if (Input.GetKeyDown(comboKeyArmAndLeg[currentIndexArmAndLeg]))
        {
            AttackCombo();
        }
        else
        {
            StartCoroutine(WaitCancelComboAttack(20.0f));
        }
    }

    IEnumerator WaitAttack(float time)
    {
        yield return new WaitForSeconds(time);
        isAttaking = false;
    }

    IEnumerator WaitCancelAttack(float time)
    {
        yield return new WaitForSeconds(time);
        currentIndexArm = 0;
        currentIndexLeg = 0;
    }

    IEnumerator WaitCancelComboAttack(float time)
    {
        yield return new WaitForSeconds(time);
        currentIndexArmAndLeg = 0;
    }

    IEnumerator WaitComboAttack(float time)
    {
        yield return new WaitForSeconds(time);
        isComboAttaking = false;
    }

    void FixedUpdate()
    {
        if (isAttaking == false && isComboAttaking == false)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
