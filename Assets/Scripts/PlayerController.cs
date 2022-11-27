using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speedModifier = 1;
    public int timeToWin = 10;
    public bool isDead = false;
    public GameObject buttons;
    public GameObject deathScreen;
    public GameObject winScreen;
    public Text winText;
    public Text timeAlive;

    float startTime;
    Rigidbody rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        startTime = Time.fixedTime;
        buttons.SetActive(false);
        deathScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    void FixedUpdate() {
        if (isDead) return;
        rb.velocity = transform.rotation * Vector3.right * speedModifier;
        transform.Rotate(Input.GetAxis("Horizontal") * -1, 0, Input.GetAxis("Vertical") * -1);
        timeAlive.text = (Time.fixedTime - startTime).ToString();
        if (Time.fixedTime - startTime > timeToWin) {
            isDead = true;
            winScreen.SetActive(true);
            buttons.SetActive(true);
            winText.text = winText.text.Replace("X", (Time.fixedTime - startTime).ToString());
        }
    }

    void OnCollisionEnter(Collision other) {
        rb.velocity = rb.velocity - new Vector3(0, rb.velocity.y, 0);
        animator.enabled = false;
        isDead = true;
        deathScreen.SetActive(true);
        buttons.SetActive(true);
    }
    
}
