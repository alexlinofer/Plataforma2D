using System.Collections;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachine;

    private GameObject _player;

    private void Awake()
    {
        if (cinemachine == null)
        {
            Debug.LogError("CinemachineVirtualCamera não está atribuída no Inspector.");
        }
    }

    private void Start()
    {
        StartCoroutine(UpdatePlayerAndSetCamera());
    }

    private IEnumerator UpdatePlayerAndSetCamera()
    {
        while (true)
        {
            // Continua tentando encontrar o Player até ele ser instanciado ou respawnado
            if (_player == null)
            {
                _player = GameObject.FindWithTag("Player");

                // Verifica se a CinemachineVirtualCamera e o Player foram encontrados
                if (_player != null && cinemachine != null)
                {
                    cinemachine.Follow = _player.transform;
                    Debug.Log("Player encontrado e câmera configurada para segui-lo.");
                }
            }
            yield return new WaitForSeconds(0.5f); // Verifica a cada meio segundo
        }
    }

    private void LateUpdate()
    {
        if (_player != null && cinemachine != null)
        {
            var framingTransposer = cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>();
            if (framingTransposer != null)
            {
                // Atualiza a posição inicial da câmera para manter a altura fixa
                Vector3 cameraPosition = cinemachine.transform.position;
                cameraPosition.y = framingTransposer.m_TrackedObjectOffset.y;
                cinemachine.transform.position = cameraPosition;
            }
        }
    }
}
