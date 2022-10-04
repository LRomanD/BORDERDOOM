using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]//������� ��������� ���� playerRoot, lookRoot � ���������� unity
    private Transform playerRoot, lookRoot;

    [SerializeField]//������� ��������� ���� invert � ���������� unity
    private bool invert;

    [SerializeField]//������� ��������� ���� can_Unlock � ���������� unity
    private bool can_Unlock = true;//��� ������� ����

    [SerializeField]//������� ��������� ���� sensitivity � ���������� unity
    private float sensitivity = 5f;

    [SerializeField]//������� ��������� ���� smooth_Steps � ���������� unity
    private int smooth_Steps = 10;

    [SerializeField]//������� ��������� ���� smooth_Weight � ���������� unity
    private float smooth_Weight = 0.4f;

    [SerializeField]//������� ��������� ���� roll_Angle � ���������� unity
    private float roll_Angle = 10f;

    [SerializeField]//������� ��������� ���� roll_Speed � ���������� unity
    private float roll_Speed = 3f;

    [SerializeField]//������� ��������� ���� default_Look_Limits � ���������� unity
    private Vector2 default_Look_Limits = new Vector2(-70, 80f);//�����������, �� ������� � �� �������. ������ ������� �������� ������

    private Vector2 look_Angles;

    private Vector2 current_Mouse_Look;

    private Vector2 smooth_Move;

    private float current_Roll_Angle;

    private int last_Look_Frame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//������ ������ ���� � ������ ������
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();

        if (Cursor.lockState == CursorLockMode.Locked)//���� ������ ��������� � ������ ������
        {
            LookAroud();
        }

    }

    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//���� ������ ������� esc
        {
            if (Cursor.lockState == CursorLockMode.Locked)//���� ������ ��������� � ������ ������
            {
                Cursor.lockState = CursorLockMode.None;//���������� ������, ������� ����� ���������
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;//������ ������ ���� � ������ ������
                Cursor.visible = false;//������ ���������
            }
        }
    }//LockAndUnlockCursor

    void LookAroud()
    {
        current_Mouse_Look = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));//����������� �������� ���� �� ����������� � ���������; X ������ Y � ��������, ������� � ��������� ������ ������������ X, ������� ���������� �� ������� ������ �����-���� (�� ��������� �������������)
        look_Angles.x += current_Mouse_Look.x * sensitivity * (invert ? 1f : -1f);//invert �������� �� �������������� ���� ������ ���������� � (? - ���� true, �� 1f; : - ���� false, �� -1f), sensitivity �� ���������������� � ����������� �������� ����, ������ ��� �������� �������� �� ���������� �
        look_Angles.y += current_Mouse_Look.y * sensitivity;//�� ���� �����, �� ��� invert, ������� ����� ������ ���������� �

        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);//Clamp - �������� ��������  look_Angles.x ����� default_x � default_y

        //current_Roll_Angle = Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw(MouseAxis.MOUSE_X) * roll_Angle, Time.deltaTime * roll_Speed);//��� �� a �� b �� ��������� t; cur_rol_ang = 0, inputgetaxis = 3; �� 0 �� 3 ��� �� ��������� Time.deltaTime * roll_Speed

        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);//� ���������� Z ����� �������� current_Roll_Angle, ���� ���������� ������ �� �����������. ������ ������ �����-����
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);//���������� Z ����� ��-�������, ���� ���� ������� ������� �������� ����. ������ ��������� �����-������



    }
}
