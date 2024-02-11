using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private Rigidbody2D _rb2D;
    public float movementSpeed;
    private float xAxis;
    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        _rb2D.velocity = new Vector2(xAxis * movementSpeed, _rb2D.velocity.y);
    }
}
