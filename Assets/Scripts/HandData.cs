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


    public Animator GetAnimator() => animator;
    public Transform GetRoot() => root;
    public Transform[] GetFingerTransforms() => fingerBones;
}
