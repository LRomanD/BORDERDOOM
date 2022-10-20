using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]//выводит приватные окна playerRoot, lookRoot в компоненте unity
    private Transform playerRoot, lookRoot;

    [SerializeField]//выводит приватное окно invert в компоненте unity
    private bool invert;

    [SerializeField]//выводит приватное окно can_Unlock в компоненте unity
    private bool can_Unlock = true;//для курсора мыши

    [SerializeField]//выводит приватное окно sensitivity в компоненте unity
    private float sensitivity = 5f;

    [SerializeField]//выводит приватное окно smooth_Steps в компоненте unity
    private int smooth_Steps = 10;

    [SerializeField]//выводит приватное окно smooth_Weight в компоненте unity
    private float smooth_Weight = 0.4f;

    [SerializeField]//выводит приватное окно roll_Angle в компоненте unity
    private float roll_Angle = 10f;

    [SerializeField]//выводит приватное окно roll_Speed в компоненте unity
    private float roll_Speed = 3f;

    [SerializeField]//выводит приватное окно default_Look_Limits в компоненте unity
    private Vector2 default_Look_Limits = new Vector2(-70, 80f);//ограничения, за которые Х не заходит. Нельзя сделать сальтуху мышкой

    private Vector2 look_Angles;

    private Vector2 current_Mouse_Look;

    private Vector2 smooth_Move;

    private float current_Roll_Angle;

    private int last_Look_Frame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//крепит курсор мыши в центре экрана
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();

        if (Cursor.lockState == CursorLockMode.Locked)//если курсор закреплен к центру экрана
        {
            LookAroud();
        }

    }

    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//если нажата клавиша esc
        {
            if (Cursor.lockState == CursorLockMode.Locked)//если курсор закреплен к центру экрана
            {
                Cursor.lockState = CursorLockMode.None;//появляется курсор, которым можно управлять
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;//крепит курсор мыши в центре экрана
                Cursor.visible = false;//курсор испарился
            }
        }
    }//LockAndUnlockCursor

    void LookAroud()
    {
        current_Mouse_Look = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));//обнаружение движения мыши по горизонтали и вертикали; X значит Y и наоборот, поэтому в следующей строке используется X, который отвечается за поворот камеры вверх-вниз (по умолчанию инвертировано)
        look_Angles.x += current_Mouse_Look.x * sensitivity * (invert ? 1f : -1f);//invert отвечает за инвертирование мыши только координаты Х (? - если true, то 1f; : - если false, то -1f), sensitivity за чувствительность и сглаживание движения мыши, первые две операции отвечают за координату Х
        look_Angles.y += current_Mouse_Look.y * sensitivity;//всё тоже самое, но без invert, который нужен только координате Х

        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);//Clamp - зажимает значение  look_Angles.x между default_x и default_y

        //current_Roll_Angle = Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw(MouseAxis.MOUSE_X) * roll_Angle, Time.deltaTime * roll_Speed);//идёт от a до b со скоростью t; cur_rol_ang = 0, inputgetaxis = 3; от 0 до 3 идёт со скоростью Time.deltaTime * roll_Speed

        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);//в координату Z можно вставить current_Roll_Angle, если предыдущая строка не закоменчена. Крутит КАМЕРУ вверх-вниз
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);//координата Z нужна по-приколу, если есть желание сделать сальтуху вбок. Крутит ПЕРСОНАЖА влево-вправо



    }
}
