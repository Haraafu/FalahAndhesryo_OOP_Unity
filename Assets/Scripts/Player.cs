using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private PlayerMovement playerMovement;
    private Animator animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        animator = FindAnyObjectByType<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        playerMovement.Move();
        playerMovement.MoveBound();
    }

    void LateUpdate()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
        else
        {
            Debug.LogWarning("Animator component on Player is missing!");
        }
    }
}
