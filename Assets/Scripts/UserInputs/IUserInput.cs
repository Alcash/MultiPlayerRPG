using UnityEngine;

public interface IUserInput
{
    Vector2 GetMovement();

    Vector2 GetAttackDirection();

    bool GetShoot();
}
