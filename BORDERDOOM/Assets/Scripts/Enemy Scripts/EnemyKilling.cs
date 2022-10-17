using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKilling : MonoBehaviour
{
    public float health = 100f;
    //Animator anim;
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;
    private UIScript player_Stats;


    private bool is_Dead;
    public bool is_Player, is_Enemy;

    
    void Awake()
    {
        if (is_Enemy)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
        }
        if (is_Player)
        {
            player_Stats = GetComponent<UIScript>();
        }
    }

    
    public void ApplyDamage(float damage)
    {
        if (is_Dead)
            return;


        health -= damage;

        if (is_Player)
        {
            // show the stats(display the health UI value)
            player_Stats.Display_HealthStats(health);
        }

        if (is_Enemy)
        {
            if (enemy_Controller.enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }
        }

        if (health <= 0f)
        {
            PlayerDied();
            is_Dead = true;
        }
    }

    private void PlayerDied()
    {
        //anim.SetBool("Dead", true);

        if (is_Enemy)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();
        }

        if (is_Player)
        {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            

            GetComponent<PlayerController>().enabled = false;
            GetComponent<Projectiles>().enabled = false;

        }

        if (tag == Tags.PLAYER_TAG)
        {

            Invoke("RestartGame", 3f);

        }
        else
        {

            Invoke("TurnOffGameObject", 3f);

        }
    }
    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
}
