using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float gravity = 9.82f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    [SerializeField] private Vector3 velocity;
    private bool isGrounded;

    [SerializeField]
    private Vector3 respawnPosition;

    private CharacterController cc;

    private GameObject[] collectibles;
    
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.right * x + transform.forward * z;

        cc.Move(dir.normalized * (moveSpeed * Time.deltaTime));

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity);

        if (Input.GetKey(KeyCode.LeftShift))
            Time.timeScale = 5f;
        else
        {
            Time.timeScale = 1f;
        }
        
        int n = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            n = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            n = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            n = 2;
        
        if (n != -1)
            SceneManager.LoadScene(n);
    }

    public void Die()
    {
        transform.position = respawnPosition;

        foreach (var collectible in collectibles)
        {
            collectible.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            respawnPosition = other.transform.position;
        }
    }
}
