using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed = 0.0f;
    public UnityEngine.UI.Image resetImage;
    private float valorAlphaResetImage;
    private  bool isDeath = false;
    
    void Update()
    {
        if (isDeath == true)
            return;

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        transform.position = transform.position + movement * Time.deltaTime * speed;
        
    }

    private void Start()
    {
        resetImage.color = new Color(0, 0, 0, 1);
        valorAlphaResetImage = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetTrigger("Death");
        isDeath = true;
    }

    public void iniciarFadeOut()
    {
        valorAlphaResetImage = 1;
    }

    private void FixedUpdate()
    {
        // Fade out
        float valorAlfa = Mathf.Lerp(resetImage.color.a, valorAlphaResetImage, .05f);
        resetImage.color = new Color(0, 0, 0, valorAlfa);

        // Reiniciar la escena
        if (valorAlfa > 0.9f && valorAlphaResetImage == 1)
            SceneManager.LoadScene("Scenes/Game");
    }
}
