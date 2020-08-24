using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    
    private BulletPool pool;
    public BulletPool Pool
    {
        get => pool;
        set => pool = value;
    }
    
    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        pool.Return(this);
    }
}
