using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hp = 100.0f;

    private CharacterController character_Controller;//объявление компонента

    private Vector3 move_Direction;//

    public float speed = 5f;//скорость
    private float gravity = 20f;//гравитация

    public float jump_Force = 10f;//сила прыжка
    private float vertical_Velocity;//


    void Awake()
    {
        character_Controller = GetComponent<CharacterController>();//получение компонента
    }


    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));//изменение координат персонажа; управление; Horizontal - A D; Vertical - W S

        move_Direction = transform.TransformDirection(move_Direction);//трансформация из локального пространства в мировое
        move_Direction *= speed * Time.deltaTime;//умножение на скорость и на deltaTime для более гладкого движения. Без deltaTime будет слишком быстро

        ApplyGravity();//применение гравитации перед движением персонажа

        character_Controller.Move(move_Direction);//Move нужен, чтоб игрока двигало в направлении нажатой кнопки. К ch_con присваивается mov_dir


    }//move player

    void ApplyGravity()
    {
        vertical_Velocity -= gravity * Time.deltaTime;//предотвращает отпрыгивание от поверхностей и полёта на луну, применение гравитации

        PlayerJump();//применение прыжка

        move_Direction.y = vertical_Velocity * Time.deltaTime;//сглаживание движения по y (вверх-вниз) - deltaTime; применение гравитации

    }//apply gravity


    void PlayerJump()
    {
        if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))//если игрок касается земли и нажал на кнопку space
        {
            vertical_Velocity = jump_Force;//к игроку применяется переменная силы прыжка, следовательно происходит движение по вертикали вверх
        }//если игрок касается земли и нажал на кнопку space
    }

    public void HealthChange(float health)
    {
        hp = (int)health;
    }
}
