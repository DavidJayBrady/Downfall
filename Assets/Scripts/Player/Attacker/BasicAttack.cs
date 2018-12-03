using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{


    public int damage;
    public Vector2 attackVector = new Vector2(-1.0f, -1.0f);
    public LayerMask layerMaskOption;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

    }

    public void Attack(Vector2 lookVector)
    {
        RaycastHit2D hit = FindObjectToHit(lookVector);
        HitObject(hit);
    }


    RaycastHit2D FindObjectToHit(Vector2 lookVector)
    {
        return Physics2D.Raycast(origin: transform.position,
                                    direction: lookVector,
                                    distance: Mathf.Sqrt(3),
                                    layerMask: layerMaskOption);
    }

    void HitObject(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            hit.collider.GetComponent<CanFeelThePain>().Damage(damage);
        }
    }
}
