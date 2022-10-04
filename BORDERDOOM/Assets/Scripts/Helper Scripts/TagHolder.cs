using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis//для упрощения, чтобы не исправлять орфографические ошибки
{
    public const string HORIZONTAL = "Horizontal";//статичная переменная, нельзя изменить, можно использовать
    public const string VERTICAL = "Vertical";//статичная переменная, нельзя изменить, можно использовать
}

public class MouseAxis
{
    public const string MOUSE_X = "Mouse X";//статичная переменная, нельзя изменить, можно использовать
    public const string MOUSE_Y = "Mouse Y";//статичная переменная, нельзя изменить, можно использовать
}

public class AnimationTags
{
    public const string ZOOM_IN_ANIM = "ZoomIn";
    public const string ZOOM_OUT_ANIM = "ZoomOut";

    public const string SHOOT_TRIGGER = "Shoot";
    public const string AIM_PARAMETER = "Aim";

    public const string WALK_PARAMETER = "Walk";
    public const string RUN_PARAMETER = "Run";
    public const string ATTACK_TRIGGER = "Attack";
    public const string DEAD_TRIGGER = "Dead";


}