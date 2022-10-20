using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{
    private PlayerController playerMovement; //другой скрипт, из которого нужно взять скорость движения

    public float sprint_Speed = 10f; //скорость бега
    public float move_Speed = 5f; //скорость ходьбы
    public float crouch_Speed = 2f; //скорость гопоты

    private Transform look_Root; //нужно для изменения положения (длины) игрока, то есть для приседания (Y)
    private float stand_Height = 1.6f; //высота объекта в стоячем положении
    private float crouch_Height = 1f; //высота объекта в сидячем положении

    private bool is_Crouching; //присел ли или нет

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerController>();

        look_Root = transform.GetChild(0); //получает первый индекс, получает первый дочерний элемент всей ветки (если вписан 0)

    }//awake

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }//update

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching) //если спринтанул, но не присел
        {
            playerMovement.speed = sprint_Speed; //ускорить игрока
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching) //если спринтанул, но не присел
        {
            playerMovement.speed = move_Speed; //вернуть обычную скорость
        }

    }//sprint

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //если сидим - встать
            if (is_Crouching)
            {
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f); //localPostition, потому что look root в unity является дочерним элементом. Если он не будет дочерним - значения будут совсем другими
                playerMovement.speed = move_Speed; //присваивание скрипту скорости персонажа скорость этого скрипта

                is_Crouching = false; //вставай
            }
            //если не сидим - сесть
            else
            {
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.speed = crouch_Speed;

                is_Crouching = true; //сядь
            }
        }//если нажата С
    }//crouch
}
