using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hp = 100.0f;

    private CharacterController character_Controller;//���������� ����������

    private Vector3 move_Direction;//

    public float speed = 5f;//��������
    private float gravity = 20f;//����������

    public float jump_Force = 10f;//���� ������
    private float vertical_Velocity;//


    void Awake()
    {
        character_Controller = GetComponent<CharacterController>();//��������� ����������
    }


    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));//��������� ��������� ���������; ����������; Horizontal - A D; Vertical - W S

        move_Direction = transform.TransformDirection(move_Direction);//������������� �� ���������� ������������ � �������
        move_Direction *= speed * Time.deltaTime;//��������� �� �������� � �� deltaTime ��� ����� �������� ��������. ��� deltaTime ����� ������� ������

        ApplyGravity();//���������� ���������� ����� ��������� ���������

        character_Controller.Move(move_Direction);//Move �����, ���� ������ ������� � ����������� ������� ������. � ch_con ������������� mov_dir


    }//move player

    void ApplyGravity()
    {
        vertical_Velocity -= gravity * Time.deltaTime;//������������� ������������ �� ������������ � ����� �� ����, ���������� ����������

        PlayerJump();//���������� ������

        move_Direction.y = vertical_Velocity * Time.deltaTime;//����������� �������� �� y (�����-����) - deltaTime; ���������� ����������

    }//apply gravity


    void PlayerJump()
    {
        if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))//���� ����� �������� ����� � ����� �� ������ space
        {
            vertical_Velocity = jump_Force;//� ������ ����������� ���������� ���� ������, ������������� ���������� �������� �� ��������� �����
        }//���� ����� �������� ����� � ����� �� ������ space
    }

    public void HealthChange(float health)
    {
        hp = (int)health;
    }
}
