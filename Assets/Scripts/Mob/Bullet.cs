using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 5;
    public float lifeTime = 4f;

    void OnEnable()
    {
        Invoke("DestroyBullet", lifeTime);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    private void DestroyBullet()
    {
        gameObject.GetComponent<Rigidbody>().Sleep();
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            DestroyBullet();
        }
    }

}
