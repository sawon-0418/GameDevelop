using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMoveController : MonoBehaviour
{
    public float speed = 2f;
    public float jumpPower = 5f;
    private Rigidbody2D rigid;
    private bool isGrounded = true;
    [SerializeField] private LayerMask groundLayer;
    RaycastHit hit;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            move += new Vector3(-1,0,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += new Vector3(1,0,0);
        }

        move = move.normalized;
        if (move.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (move.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        transform.Translate(move * speed * Time.fixedDeltaTime);

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.5f);
        Debug.DrawRay(rigid.position, Vector3.down * 1.5f, new Color(0,1,0));
        if (rayHit.collider != null)
        {
            isGrounded = true;
        }
    }
}
