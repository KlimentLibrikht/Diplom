using System.Collections;
using UnityEngine;

public class Enemy_SkeletonScript : MonoBehaviour
{
    public Animator animator;
    public EnemyAI _enemyController;

    void Start()
    {
        _enemyController.isAwake = true;
        animator = GetComponent<Animator>();
        _enemyController = GetComponent<EnemyAI>();
    }

    void Update()
    {
        if (_enemyController.isDead == false && _enemyController.isAwake == true)
        {
            _enemyController.Angry();
            _enemyController.EnemyAttack("SkeletonHit");
        }
        StartCoroutine(Who_IsDead(10));
    }

    IEnumerator Who_IsDead(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (_enemyController._healthEnemy <= 0 && _enemyController.isDead == false)
        {
            _enemyController.isDead = true;
            _enemyController._animator.SetTrigger("SkeletonDead");
            _enemyController.ObjectToggleState();
        }
    }
}
