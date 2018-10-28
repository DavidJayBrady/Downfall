using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    private ControllerMovement _controllerMovement;
    private BoxCollider2D _boxCollider2D;
    public Vector2 attackVector = new Vector2(-1.0f, -1.0f);
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        _controllerMovement = GetComponent<ControllerMovement>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 lastMoveAttempt = Common.GetScaledVectorInput("Horizontal", "Vertical");
        Common.VectorNormalize(ref lastMoveAttempt);

        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position - (3f*_boxCollider2D.offset), lastMoveAttempt, layerMask);
        if (hit.collider != null)
        {
            Debug.Log(hit.ToString());
            Debug.Log(hit.collider.transform.position);
        }

    }
}
