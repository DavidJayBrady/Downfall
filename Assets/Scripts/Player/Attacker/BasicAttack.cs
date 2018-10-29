using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{

    private Vector2 _lookVector;

    public int damage;
    public Vector2 attackVector = new Vector2(-1.0f, -1.0f);
    public LayerMask layerMaskOption;

    // Start is called before the first frame update
    void Start()
    {
        damage = 5;
    }

    // Update is called once     per frame
    void Update()
    {

    }

    public void Attack()
    {
        UpdateLookVector();
        RaycastHit2D hit = FindObjectToHit();
        HitObject(hit);
    }



    void UpdateLookVector()
    {
        Vector2 lastMoveAttempt = Common.GetScaledVectorInput(1, "Horizontal", "Vertical");
        if (lastMoveAttempt.magnitude > .5f)
        {
            _lookVector = lastMoveAttempt;
        }
    }

    RaycastHit2D FindObjectToHit()
    {
        return Physics2D.Raycast(origin: transform.position,
                                    direction: _lookVector,
                                    distance: Mathf.Sqrt(3),
                                    layerMask: layerMaskOption);
    }

    void HitObject(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Building>().Damage(damage);
        }
    }
}
