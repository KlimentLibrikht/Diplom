using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [Header("Main_Parameters")]
    public int _maxHealth;
    [SerializeField] public int _healthEnemy;
    [SerializeField] public float _speed;
    public bool _isDisable = false;
    public Transform _positionPartolPoint;
    private Vector2 _direction;
    public Transform _player;
    public PlayerController _playerController;
    public Animator _animator;
    public bool isDead = false;
    public bool isAwake = false;

    [Header("Moving_Variables")]
    bool _moveingRight;
    float HorizontalMove;
    float VerticalMove;
    public int _positionofPatrol;
    public float _stopAroundThePlayerDistance;
    public float _saveDistance;
    public bool FacingRight = true;
    public bool isFlipped = true;
    public bool isStay = false;

    [Header("Status")]
    public bool _angry = true;

    [Header("Attack_Variables")]
    public Transform _attackPoint;
    public float _attackRange;
    public LayerMask _playerLayer;
    [SerializeField] public int _attackDamage;
    [SerializeField] private float _attackSpeed;
    private float _rechargeAttack;

    private void Start()
    {
        _rechargeAttack = _attackSpeed;
        _animator = GetComponent<Animator>();
        _healthEnemy = _maxHealth;
        _player = GameObject.FindGameObjectWithTag("Actor").transform;
        _playerController = GameObject.FindGameObjectWithTag("Actor").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (isDead == false && isAwake == true)
        {
            _rechargeAttack += Time.deltaTime;
        }
    }
    public void EnemyAttack(string AttackTrigger)
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerLayer);
        if (hitPlayer.Length > 0 && transform.position.y == _player.transform.position.y)
        {
            foreach (Collider2D player in hitPlayer)
            {
                if (_rechargeAttack > _attackSpeed)
                {
                    _angry = false;
                    _animator.SetTrigger(AttackTrigger);
                    isStay = true;
                    _rechargeAttack = 0;
                    StartCoroutine(StayOver(1.75f));
                    StartCoroutine(AngryOver(1.5f));
                }
            }
        }
    }

    IEnumerator Who_IsDead(float sec, string deathTrigger)
    {
        yield return new WaitForSeconds(sec);
        if (_healthEnemy <= 0 && isDead == false)
        {
            isDead = true;
            _animator.SetTrigger(deathTrigger);
            ObjectToggleState();
        }
    }

    IEnumerator StayOver(float sec)
    {
        if (this.gameObject.name == "SkeletonHeavy")
        {
            _animator.SetTrigger("SkeletonHeavyShield");
        }
        yield return new WaitForSeconds(sec);
        isStay = false;
    }

    public void Chill()
    {
        if (transform.position.x > _positionPartolPoint.position.x + _positionofPatrol)
        {
            transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
            Flip();
            Walk();
        }
        else if (transform.position.x < _positionPartolPoint.position.x - _positionofPatrol)
        {
            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
            Flip();
            Walk();
        }
    }

    public void Angry()
    {
        Walk();
        if (_angry == true && isStay == false)
        {
            if (_player.position.x < transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position + new Vector3(_stopAroundThePlayerDistance, 0, 0), _speed * Time.deltaTime);
            }
            else if (_player.position.x > transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position - new Vector3(_stopAroundThePlayerDistance, 0, 0), _speed * Time.deltaTime);
            }
        }
        else 
        {
            if(isStay == false)
            {
                if (_player.position.x < transform.position.x)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _player.position + new Vector3(_saveDistance, 0, 0), _speed * Time.deltaTime);
                    if (transform.position.x == _player.position.x + _saveDistance)
                    {
                        isStay = true;
                        StartCoroutine(StayOver(4));
                    }
                }
                else if (_player.position.x > transform.position.x)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _player.position - new Vector3(_saveDistance, 0, 0), _speed * Time.deltaTime);
                    if (transform.position.x == _player.position.x - _saveDistance)
                    {
                        isStay = true;
                        StartCoroutine(StayOver(4));
                    }
                }
                Walk();
                StartCoroutine(AngryOver(7));
            }
        }
    }

    IEnumerator AngryOver(float sec)
    {
        yield return new WaitForSeconds(sec);
        _angry = true;
    }

    public void ObjectToggleState()
    {
        if (_isDisable)
        {
            _isDisable = !_isDisable;
            Die();
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public void TakingDamage(int damage, string deathTrigger, string hurtTrigger)
    {
        _healthEnemy -= damage;
        if (_healthEnemy <= 0 && isDead == false)
        {
            isDead = true;
            ObjectToggleState();
            _animator.SetTrigger(deathTrigger);
        }
        else if (_healthEnemy <= (_maxHealth/100*30) && isDead == false)
        {
            _animator.SetTrigger(hurtTrigger);
            _angry = false;
        }
        else if (isDead == false)
        {
            _animator.SetTrigger(hurtTrigger);
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");
        _playerController._countKillEnemy += 1;
        gameObject.SetActive(false);
        this.enabled = false;
        if (this.gameObject.name != "Boss")
        {
            Destroy(gameObject);
        }
        else if (this.gameObject.name == "Boss")
        {
            SceneTransition.SwitchToSceneByIndex(1);
            Cursor.visible = true;
        }
    }

    public void ToDamage()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerLayer);
        if (hitPlayer.Length > 0)
        {
            foreach (Collider2D player in hitPlayer)
            {
                if (ChechHitRange(player.gameObject.GetComponents<BoxCollider2D>()[1]))
                {
                    player.GetComponent<PlayerController>().TakingDamage(_attackDamage);
                }
            }
        }
    }

    bool ChechHitRange(BoxCollider2D _currentEnemyPos)
    {
        if (Mathf.Abs(Mathf.Abs(this.gameObject.GetComponents<BoxCollider2D>()[1].transform.position.y) - Mathf.Abs(_currentEnemyPos.transform.position.y)) <= 25f)
        {
            return true;
        }
        else
            return false;
    }

    public void Walk()
    {
        if (isStay == false)
        {
            HorizontalMove = transform.position.x;
            _direction.x = transform.position.x;
            VerticalMove = transform.position.y;
            _direction.y = transform.position.y;
            _animator.SetFloat("Horizontal", Mathf.Abs(HorizontalMove));
            _animator.SetFloat("Vertical", Mathf.Abs(VerticalMove));
            if (_player.position.x < transform.position.x && FacingRight)
            {
                Flip();
            }
            else if (_player.position.x > transform.position.x && !FacingRight)
            {
                Flip();
            }
        }
        else
        {
            HorizontalMove = 0;
            _direction.x = transform.position.x;
            VerticalMove = 0;
            _direction.y = transform.position.y;
            _animator.SetFloat("Horizontal", Mathf.Abs(HorizontalMove));
            _animator.SetFloat("Vertical", Mathf.Abs(VerticalMove));
            if (_player.position.x < transform.position.x && FacingRight)
            {
                Flip();
            }
            else if (_player.position.x > transform.position.x && !FacingRight)
            {
                Flip();
            }
        }
    }

    public void Healing(int count)
    {
        _attackDamage += 50;
        _speed += 100;
        _attackSpeed = 2;
        for (int i = 0; i < count; i++)
        {
            StartCoroutine(HealEnemy());
        }
    }

    IEnumerator HealEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        _healthEnemy += 10;
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
