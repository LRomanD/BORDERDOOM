using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{
    private PlayerController playerMovement; //������ ������, �� �������� ����� ����� �������� ��������

    public float sprint_Speed = 10f; //�������� ����
    public float move_Speed = 5f; //�������� ������
    public float crouch_Speed = 2f; //�������� ������

    private Transform look_Root; //����� ��� ��������� ��������� (�����) ������, �� ���� ��� ���������� (Y)
    private float stand_Height = 1.6f; //������ ������� � ������� ���������
    private float crouch_Height = 1f; //������ ������� � ������� ���������

    private bool is_Crouching; //������ �� ��� ���

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerController>();

        look_Root = transform.GetChild(0); //�������� ������ ������, �������� ������ �������� ������� ���� ����� (���� ������ 0)

    }//awake

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }//update

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching) //���� ����������, �� �� ������
        {
            playerMovement.speed = sprint_Speed; //�������� ������
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching) //���� ����������, �� �� ������
        {
            playerMovement.speed = move_Speed; //������� ������� ��������
        }

    }//sprint

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //���� ����� - ������
            if (is_Crouching)
            {
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f); //localPostition, ������ ��� look root � unity �������� �������� ���������. ���� �� �� ����� �������� - �������� ����� ������ �������
                playerMovement.speed = move_Speed; //������������ ������� �������� ��������� �������� ����� �������

                is_Crouching = false; //�������
            }
            //���� �� ����� - �����
            else
            {
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.speed = crouch_Speed;

                is_Crouching = true; //����
            }
        }//���� ������ �
    }//crouch
}
