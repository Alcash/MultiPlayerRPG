using UnityEngine;

/// <summary>
/// Управление на компьютере
/// </summary>
public class DesktopUserInput : BaseUserInput
{    

    /// <summary>
    /// Возвращает направление атаки
    /// </summary>
    /// <returns></returns>
    public override Vector2 GetAttackDirection()
    {
        Vector2 result = Vector2.zero;
        if (Input.GetMouseButton(0))
        { 
            //От центра экрана в сторону где курсор  
            float heightCenter = Screen.height / 2;
            float widthCenter = Screen.width / 2;

            Vector2 centerPos = new Vector2(widthCenter, heightCenter);

            Vector2 mousePos = Input.mousePosition;
            result = centerPos - mousePos;
        }
        return result.normalized;
    }

    /// <summary>
    /// Возвращает направление передвижения
    /// </summary>
    /// <returns></returns>
    public override Vector2 GetMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(h,v);

        return move;
    }

    /// <summary>
    /// Возвращает стрельбу
    /// </summary>
    /// <returns></returns>
    public override bool GetShoot()
    {
        return Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0);
    }

    private void Awake()
    {

#if !UNITY_EDITOR && !UNITY_STANDALONE
        Destroy(GetComponent<DesktopUserInput>());
#endif
    }
}
