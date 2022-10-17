using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    public EnemyState enemy_State;

    public float walk_Speed = 0.5f;//�������� ������ �����
    public float run_Speed = 4f;//��������� ���� �����

    public float chase_Distance = 7f;//����������, �� ������� ���� ������� ������
    private float current_Chase_Distance;//��������� ������������� ������, ���� ����� ��������� �� ����� � ������� ���������
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;//����� �������������� �� ���� �������, ���� �� ����� ������ ����� ��������� ����������
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;



    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();//��������� ����������
        navAgent = GetComponent<NavMeshAgent>();//��������� ����������

        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }//Awake


    // Start is called before the first frame update
    void Start()
    {
        enemy_State = EnemyState.PATROL;//������� ������� ������� ��������������

        patrol_Timer = patrol_For_This_Time;//����� ����� ���������, ����� �� ����� 15 ������ ���� �������, ����� ���� ����� �������������

        attack_Timer = wait_Before_Attack;//����� ���� ������� �� ������ - ����� ��������

        current_Chase_Distance = chase_Distance;//���������� �������� ���������� �������������, ����� ��� ����� �������
    }//Start

    // Update is called once per frame
    void Update()
    {
        if (enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemy_State == EnemyState.CHASE)
        {
            Chase();
        }

        if (enemy_State == EnemyState.ATTACK)
        {
            Attack();
        }



    }//Update

    void Patrol()
    {
        navAgent.isStopped = false;//������� navAgent, ��� �� ����� ���������
        navAgent.speed = walk_Speed;//���������� �������� ������, ���� ���� �������� ���� �� ����� ������



        patrol_Timer += Time.deltaTime;//���������� � patrol_Timer

        if (patrol_Timer > patrol_For_This_Time)//����� patrol_Timer ������ ������ patrol_For_This_Time, ������� ����� ����������
        {
            SetNewRandomDestination();

            patrol_Timer = 0f;//��� ����� if ������ ������ �� �����, ���� patrol_Timer ���������� ��� ������ � ������, ������� ���������� ������ ��� �������� 0

        }

        if (navAgent.velocity.sqrMagnitude > 0)//���� ���������� �������� ����-������ (�� ���� ������ ����)
        {
            enemy_Anim.Walk(true);//���� �������
        }
        else
        {
            enemy_Anim.Walk(false);//���� �������
        }


        //��������� ���������� ����� ������� � ������
        if (Vector3.Distance(transform.position, target.position) <= chase_Distance)//Distance - �������� �������� ���������� ����� ������� � ������. ���� �� ��� ���������� ������ ��� ����� chase_Distance
        {
            enemy_Anim.Walk(false);//���������� �������� ������

            enemy_State = EnemyState.CHASE;//������������� ��������� ����� - �������������





        }

    }//Patrol

    void Chase()
    {
        navAgent.isStopped = false;//������� navAgent, ��� �� ����� ���������
        navAgent.speed = run_Speed;//����� �������� ����

        navAgent.SetDestination(target.position);//������������� ������� ������ ��� ����� ���������� ��� �����,

        if (navAgent.velocity.sqrMagnitude > 0)//���� ���������� �������� ����-������ (�� ���� ������ ����)
        {
            enemy_Anim.Run(true);//���� �������
        }
        else
        {
            enemy_Anim.Run(false);//���� �������
        }

        if(Vector3.Distance(transform.position, target.position) <= attack_Distance)//���� ���������� ����� ������� � ������ ������, ��� ���������� �����
        {
            enemy_Anim.Run(false);//��������� �������� ����
            enemy_Anim.Walk(false);//��������� �������� ������ (�� ������)
            enemy_State = EnemyState.ATTACK;//������� � ������ �����


            //������������ ���������� ������ �� ������� ��������
            if(chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }
        else if(Vector3.Distance(transform.position, target.position) > chase_Distance)//���� ����� ������ �� ���������� ������������� �����
        {
            enemy_Anim.Run(false);//��������� ������
            enemy_State = EnemyState.PATROL;//������� � ������ ��������������

            patrol_Timer = patrol_For_This_Time;//����������� ������ �������������, ����� ������� ���������� ����� ���������� �������������� �����

            //������������ ���������� ������ �� ������� ��������
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }
    }//Chase

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;//������������� �����

        attack_Timer += Time.deltaTime;

        if(attack_Timer > wait_Before_Attack)
        {
            enemy_Anim.Attack();

            attack_Timer = 0f;//����� ���� �� �������� ����������� ���������� ��� �� �����������


        }

        if(Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance)//transform - ����; target - �����. ���� ���������� ����� ������ � ������ ������ ����� ���������� ����� � ���������� ������������� ����� ����� 
        {
            enemy_State = EnemyState.CHASE;
        }

    }//Attack

    void SetNewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);//�������� ��������� �������� ����� 20 � 60

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;//�������� ��������� ����� �������
        randDir += transform.position;//randDir(����� ��������� �����) + transform position (������� �������������) = ����� ����� ��������������

        NavMeshHit navHit;//������, ���� ���� �� ���� �� ������� ������������, �� �������� �� ����� ������

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);//SamplePosition - �������� ��������� �������, �������� �����, � ���������� � ��������� ����.
                                                                     //���� �� ������������ ��������, �� ������������ ����� ��������������.
                                                                     //����� �������������� ����� ��������� � navHit.
                                                                     //��������� �������� ���������, �� ����� ���� �� ����� �������� (-1 - �������� ��� ���� �������)

        navAgent.SetDestination(navHit.position);





    }//SetNewRandomDestination












}//class
