using UnityEngine;

public class HandData : MonoBehaviour
{
    public enum HandModelType
    {
        Left, Right
    }


    [SerializeField] private HandModelType handModelType;
    [SerializeField] private Transform root;
    [SerializeField] private Animator animator;
    [SerializeField, Tooltip("Order of bones matter")] private Transform[] fingerBones;


    public Animator Animator { get { return animator; } }
    public Transform Root { get { return root; } }
    public Transform[] FingerTransforms { get { return fingerBones; } }
}
