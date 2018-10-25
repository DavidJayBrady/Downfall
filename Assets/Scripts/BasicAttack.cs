using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    private ControllerMovement _controllerMovement;
    public Vector2 attackVector = new Vector2(-1.0f, -1.0f);

    // Start is called before the first frame update
    void Start()
    {
        _controllerMovement = GetComponent<ControllerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1. find location to end at
        //     a. scale lookVector to gridSpace
        //     b.
        Vector2 lastMoveAttempt = Common.GetScaledVectorInput("Horizontal", "Vertical");
        if (lastMoveAttempt.magnitude > 0.5f)
        {
            Common.VectorNormalize(ref lastMoveAttempt);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),
                out hit, lastMoveAttempt.magnitude))
            {
                Debug.Log("hit");
            }
        }
    }
}
