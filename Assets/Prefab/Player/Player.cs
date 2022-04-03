using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class Player : NetworkBehaviour
{
    PlayerInput playerInput;
    Vector2 moveInput;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Camera playerEye;
    Animator animator;

    private NetworkVariable<Vector2> netMoveInput = new NetworkVariable<Vector2>();

    private void Awake()
    {
        if(playerInput == null)
        {
            playerInput = new PlayerInput();
        }
    }

    private void OnEnable()
    {
        if (playerInput != null)
        {
            playerInput.Enable();
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.Disable();
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkDespawn();
        if (IsServer)
        {
            PlayerStart playerStart = FindObjectOfType<PlayerStart>();
            transform.position = playerStart.GetRandomSpawnArea();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (IsOwner)
        {
            playerInput.Gameplay.Move.performed += Move;
            playerInput.Gameplay.Move.canceled += Move;
            animator = GetComponent<Animator>();
            if(playerEye!= null)
            {
                playerEye.enabled = true;
            }
        }

        animator = GetComponent<Animator>();


    }

    private void Move(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInputUpdatedServerRpc(obj.ReadValue<Vector2>());
    }


    [ServerRpc]
    private void OnInputUpdatedServerRpc(Vector2 newInputValue)
    {
        netMoveInput.Value = newInputValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            transform.position += new Vector3(netMoveInput.Value.x, 0, netMoveInput.Value.y) * Time.deltaTime * moveSpeed;
        }

        float currentMoveSpeed = netMoveInput.Value.magnitude * moveSpeed;
        if(animator!= null)
        {
            animator.SetFloat("speed", currentMoveSpeed);
        }
    }
}
