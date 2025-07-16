using UnityEngine; //abstract class

public abstract class MovableObjectBase : MonoBehaviour
{ 
    [SerializeField] protected float moveSpeed = 5;
    public virtual void MoveStep(float deltaTime)
    {  
        transform.position += Vector3.left * moveSpeed * deltaTime;
    }
    public Vector3 Position => transform.position;
}