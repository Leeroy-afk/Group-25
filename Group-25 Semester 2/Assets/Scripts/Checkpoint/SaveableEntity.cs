using UnityEngine;
using System;

public class SaveableEntity : MonoBehaviour
{
    [SerializeField] private string uniqueId;

    public string UniqueID => uniqueId;

    [ContextMenu("Generate New ID")]
    private void GenerateNewID()
    {
        uniqueId = Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        if (string.IsNullOrEmpty(uniqueId))
        {
            Debug.LogError(
                $"SaveableEntity on {gameObject.name} has no ID!",
                this
            );
        }
    }
}