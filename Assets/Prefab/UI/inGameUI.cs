using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class inGameUI : MonoBehaviour
{

    [SerializeField] Button StartHostBtn;
    [SerializeField] Button StartServerBtn;
    [SerializeField] Button StartClientBtn;

    // Start is called before the first frame update
    void Start()
    {
        StartHostBtn.onClick.AddListener(StartHost);
        StartHostBtn.onClick.AddListener(StartServer);
        StartHostBtn.onClick.AddListener(StartClient);
    }

    private void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    private void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
