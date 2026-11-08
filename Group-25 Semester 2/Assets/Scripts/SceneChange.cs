using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;   
public class SceneChange : MonoBehaviour
{
    public string scenename;

    [Header("Enemy Sequence")]
    [SerializeField]private AITriggerSequence aiTriggerSequence;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (!aiTriggerSequence.enemySequenceFinished)
            return;
        
            SceneManager.LoadScene(scenename);
        }
    }


