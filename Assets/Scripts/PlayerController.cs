using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

     private bool isGround;

    public float jumpForce = 200.0f;

    public float speed = 0;

    public TextMeshProUGUI countText;

    public GameObject winTextObject;

    private float movementX;
    private float movementY;

    private Rigidbody rb;
    private int count;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
         isGround=true;

        count = 0;

        SetCountText();

        winTextObject.SetActive(false);

    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
           
        RaycastHit hit;

        Physics.Raycast(transform.position, new Vector3(0,-50,0), out hit);

        if(hit.distance <0.6){
            isGround = true;
        } else{
            isGround = false;
        }

        print(hit.distance);

    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            count = count + 1;

            SetCountText();
        }
    }

    void OnJump()
    {
        
        if(this.isGround==true){
           rb.AddForce(new Vector3(0, jumpForce, 0));
        }
            
        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 4)
        {
            winTextObject.SetActive(true);
        }
    }




}