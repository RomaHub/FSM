public interface IAttackable
{
    Mob Holder { get; set; }
    int Bullets { get; set; }
    int ClipSize { get; set; }
    int Damage { get; set; }
    float BulletSpeed { get; set; }
    float FireRate { get; set; }
    void Shoot();
}
