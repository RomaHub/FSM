using UnityEngine;

public class AdvancedAttack : IAttackable
{

    public Mob Holder { get; set; }
    public int Bullets { get; set; }
    public int ClipSize { get; set; }
    public int Damage { get; set; }
    public float BulletSpeed { get; set; }
    public float FireRate { get; set; }

    public AdvancedAttack(Mob mob)
    {
        Holder = mob;

        Bullets = 30;
        ClipSize = 30;
        BulletSpeed = 300f;
        FireRate = 0.1f;
        Damage = 2;
    }

    public void Shoot()
    {
        if (Bullets > 0)
        {
            Bullets--;

            GameObject bullet = BulletPool.Instance.GetBullet();

            bullet.transform.position = Holder.gunBarrel.position;
            bullet.transform.rotation = Holder.gunBarrel.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().damage = Damage;

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.WakeUp();
            rb.AddForce(Holder.transform.forward * BulletSpeed);

        }
        else
        {
            Holder.CurrentState = Holder.IdleState;
            Bullets = ClipSize;
        }
    }
}
