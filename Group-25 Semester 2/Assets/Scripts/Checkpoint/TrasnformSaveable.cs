using UnityEngine;
using System;

public class TransformSaveable : MonoBehaviour, ISaveable
{
    [Serializable]
    private class TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    public string CaptureState()
    {
        return JsonUtility.ToJson(new TransformData
        {
            position = transform.position,
            rotation = transform.rotation
        });
    }

    public void RestoreState(string state)
    {
        TransformData data = JsonUtility.FromJson<TransformData>(state);

        if (data == null)
            return;

        CharacterController controller = GetComponent<CharacterController>();

        if (controller != null)
            controller.enabled = false;

        transform.SetPositionAndRotation(data.position, data.rotation);

        if (controller != null)
            controller.enabled = true;

        Debug.Log("PLAYER POSITION RESTORED TO: " + data.position);
    }
}