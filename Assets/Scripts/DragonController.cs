using UnityEngine;

public class DragonController : MonoBehaviour
{


    [SerializeField]    
    private float speed ;
    private FixedJoystick fixedJoystick;

    private Rigidbody rigidbody;

    // public Animator animator;

    void Start()
    {
        fixedJoystick = FindObjectsByType<FixedJoystick>(FindObjectsSortMode.None)[0];
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = fixedJoystick.Horizontal;
        float vertical = fixedJoystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        rigidbody.linearVelocity = direction * speed;
        if (direction.magnitude >= 0.1f)
        {
          transform.eulerAngles = new Vector3(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, transform.eulerAngles.z);
           // animator.SetBool("isFlying", true);
        }
        else
        {
           // animator.SetBool("isFlying", false);
        }
    }
}
