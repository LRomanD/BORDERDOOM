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

    public float walk_Speed = 0.5f;//скорость ходьбы врага
    public float run_Speed = 4f;//скороость бега врага

    public float chase_Distance = 7f;//расстояние, на котором враг заметит игрока
    private float current_Chase_Distance;//активатор преследования игрока, если игрок выстрелил во врага с дальней дистанции
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;//время патрулирования до того момента, пока не будет задано новое случайное расстояние
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;



    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();//получение компонента
        navAgent = GetComponent<NavMeshAgent>();//получение компонента

        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }//Awake


    // Start is called before the first frame update
    void Start()
    {
        enemy_State = EnemyState.PATROL;//задание вражине функции патрулирования

        patrol_Timer = patrol_For_This_Time;//задаём здесь равенство, чтобы не ждать 15 секунд того момента, когда враг начнёт патрулировать

        attack_Timer = wait_Before_Attack;//когда враг доходит до игрока - сразу аттакует

        current_Chase_Distance = chase_Distance;//запоминает значение расстояния преследования, чтобы его потом вернуть
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
        navAgent.isStopped = false;//говорит navAgent, что он может двигаться
        navAgent.speed = walk_Speed;//возвращает скорость ходьбы, если была скорость бега во время погони



        patrol_Timer += Time.deltaTime;//добавление к patrol_Timer

        if (patrol_Timer > patrol_For_This_Time)//когда patrol_Timer станет больше patrol_For_This_Time, задаётся новое расстояние
        {
            SetNewRandomDestination();

            patrol_Timer = 0f;//без этого if больше вызван не будет, ведь patrol_Timer становится все больше и больше, поэтому приходится задать ему значение 0

        }

        if (navAgent.velocity.sqrMagnitude > 0)//если происходит движение куда-нибудь (то есть больше нуля)
        {
            enemy_Anim.Walk(true);//ходи вражина
        }
        else
        {
            enemy_Anim.Walk(false);//стой вражина
        }


        //тестирует расстояние между игроком и врагом
        if (Vector3.Distance(transform.position, target.position) <= chase_Distance)//Distance - получает значение расстояния между игроком и врагом. Если всё это расстояние меньше или равно chase_Distance
        {
            enemy_Anim.Walk(false);//завершение анимации ходьбы

            enemy_State = EnemyState.CHASE;//присвавивание состоянию врага - преследование





        }

    }//Patrol

    void Chase()
    {
        navAgent.isStopped = false;//говорит navAgent, что он может двигаться
        navAgent.speed = run_Speed;//задаёт скорость бега

        navAgent.SetDestination(target.position);//устанавливает позицию игрока как пункт назначения для врага,

        if (navAgent.velocity.sqrMagnitude > 0)//если происходит движение куда-нибудь (то есть больше нуля)
        {
            enemy_Anim.Run(true);//беги вражина
        }
        else
        {
            enemy_Anim.Run(false);//стой вражина
        }

        if(Vector3.Distance(transform.position, target.position) <= attack_Distance)//если расстояние между игроком и врагом меньше, чем расстояние атаки
        {
            enemy_Anim.Run(false);//остановка анимации бега
            enemy_Anim.Walk(false);//остановка анимации ходьбы (на всякий)
            enemy_State = EnemyState.ATTACK;//переход в стадию атаки


            //возобновляет расстояние погони на прошлое значение
            if(chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }
        else if(Vector3.Distance(transform.position, target.position) > chase_Distance)//если игрок убежал из расстояния преследования врага
        {
            enemy_Anim.Run(false);//отставить бежать
            enemy_State = EnemyState.PATROL;//переход в стадию патрулирования

            patrol_Timer = patrol_For_This_Time;//возобноляет таймер преследования, чтобы функция просчитала новое расстояние патрулирования сразу

            //возобновляет расстояние погони на прошлое значение
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }
    }//Chase

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;//останавливает врага

        attack_Timer += Time.deltaTime;

        if(attack_Timer > wait_Before_Attack)
        {
            enemy_Anim.Attack();

            attack_Timer = 0f;//чтобы враг не атаковал бесконечное количество раз за милисекунду


        }

        if(Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance)//transform - враг; target - игрок. Если расстояние между врагом и игрокм больше суммы расстояния атаки и расстояния преследования после атаки 
        {
            enemy_State = EnemyState.CHASE;
        }

    }//Attack

    void SetNewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);//получает случайное значение между 20 и 60

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;//получает случайное число радиуса
        randDir += transform.position;//randDir(новое случайное число) + transform position (текущее местоположние) = новое число местоположения

        NavMeshHit navHit;//создаём, чтоб враг не ушел за границы пространства, по которому он может ходить

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);//SamplePosition - получает случайную позицию, заданную ранее, и сравнивает с границами мира.
                                                                     //Если не соответсвует границам, то рассчитивыет новое местоположение.
                                                                     //Новое местоположение будет храниться в navHit.
                                                                     //Последнее значение указывает, на каком слое всё будет работать (-1 - включает все слои объекта)

        navAgent.SetDestination(navHit.position);





    }//SetNewRandomDestination












}//class
