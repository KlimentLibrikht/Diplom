using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    EnemyAI _enemyController;
    public AudioSource _ambient;
    public AudioSource _battleTheme;
    public GameObject _bossHealthBar;
    public GameObject _mainCamera;
    public GameObject _bossFightCamera;
    Animator animator;
    public bool isHeal;

    // Start is called before the first frame update
    void Start()
    {
        isHeal = false;
        animator = GetComponent<Animator>();
        if (this.isActiveAndEnabled == true)
        {
            _ambient.Stop();
            _battleTheme.Play();
            _bossHealthBar.SetActive(true);
            _mainCamera.SetActive(false);
            _bossFightCamera.SetActive(true);
        }
        _enemyController = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyController.isAwake == false)
        {
            StartCoroutine(BossAwake());
        }
        if (_enemyController.isDead == false && _enemyController.isAwake == true)
        {
            _enemyController.Angry();
            _enemyController.EnemyAttack("EnemyAttack");
        }
        if (_battleTheme.isPlaying == false)
        {
            _battleTheme.Play();
        }
        if (_enemyController._healthEnemy < 0)
        {
            _bossHealthBar.SetActive(false);
            _mainCamera.SetActive(true);
            _bossFightCamera.SetActive(false);
            _battleTheme.Stop();
            _ambient.Play();
        }
        else if (_enemyController._healthEnemy < (_enemyController._maxHealth/100*10) && isHeal == false)
        {
            _enemyController.Healing(10);
            isHeal = true;
        }
        StartCoroutine(Who_IsDead(10));
    }


    IEnumerator Who_IsDead(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (_enemyController._healthEnemy <= 0 && _enemyController.isDead == false)
        {
            _enemyController.isDead = true;
            _enemyController._animator.SetTrigger("EnemyDead");
            _enemyController.ObjectToggleState();
            _battleTheme.Stop();
            _ambient.Play();
        }
    }

    public IEnumerator BossAwake()
    {
        yield return new WaitForSeconds(0.05f);
        _enemyController.isAwake = true;
        animator.SetTrigger("EnemyAwake");
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("EnemyAwake");
    }
}
