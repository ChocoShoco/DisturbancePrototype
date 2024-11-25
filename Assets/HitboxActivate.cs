using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxActivate : MonoBehaviour
{
    public GameObject hitbox;

    public void ActivateHitbox()
    {
        hitbox.SetActive(true);
    }

    public void DeactivateHitbox()
    {
        hitbox.SetActive(false);
    }
}
