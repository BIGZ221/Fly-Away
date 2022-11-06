using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speedModifier = 1;
    public int timeToWin = 10;
    public bool isDead = false;

    float startTime;
    Rigidbody rb;
    Animator animator;
    GameObject myGO;
    GameObject myText;
    Canvas myCanvas;
    Text text;
    RectTransform rectTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        setupCanvas();
        startTime = Time.fixedTime;
    }

    void FixedUpdate() {
        if (isDead) return;
        rb.velocity = transform.rotation * Vector3.right * speedModifier;
        transform.Rotate(Input.GetAxis("Horizontal") * -1, 0, Input.GetAxis("Vertical") * -1);
        if (Time.fixedTime - startTime > timeToWin) {
            isDead = true;
            renderText("You won ... good job");
        }
    }

    void setupCanvas() {
        myGO = new GameObject();
        myGO.name = "UI Canvas";
        myGO.AddComponent<Canvas>();

        myCanvas = myGO.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        myGO.AddComponent<CanvasScaler>();
        myGO.AddComponent<GraphicRaycaster>();
        // Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "TextContent";
        text = myText.AddComponent<Text>();
        text.font = (Font)Resources.Load("ARIAL");
    }

    public void renderText(string content) {
        text.text = content;
        text.color = Color.black;
        text.fontSize = 70;
        text.alignment = TextAnchor.MiddleCenter;
        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 175, 0);
        rectTransform.sizeDelta = new Vector2(800, 200);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.name.ToLower().Contains("ground")) {
            rb.velocity = rb.velocity - new Vector3(0, rb.velocity.y, 0);
            animator.enabled = false;
            // rb.useGravity = true;
            isDead = true;
            renderText("You died valiantly...");
        }
    }

    
}
