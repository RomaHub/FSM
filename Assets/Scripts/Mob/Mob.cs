using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mob : Unit, IDamageable, IAttackable, IDragHandler, IPointerClickHandler
{
    [Header("Stats")]
    public string mobName;
    public float stayInState = 4f;
    public float rotateSpeed = 20f;
    public float jumpStrength = 2f;
    public float fireRate = 1f;
    public int bullets = 10;
    public bool isGrounded;

    [Header("Assign")]
    public Material idleColor;
    public Material attackColor;
    public Material jumpColor;
    public GameObject particlePrefab;
    public GameObject bulletPrefab;
    public Transform gunBarrel;

    [Header("Data")]
    public MobData data = new MobData();

    [HideInInspector]
    public MeshRenderer mobColor;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Rigidbody rigidBody;

    private int _health;

    #region Properties

    public IMobState CurrentState { get; set; }
    public IMobState IdleState { get; private set; }
    public IMobState AttackState { get; private set; }
    public IMobState JumpState { get; private set; }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                Death();
            }
        }
    }
    
    #endregion
    
    #region MonoBehaviour

    private void Awake()
    {
        IdleState = new IdleState(this);
        AttackState = new AttackState(this);
        JumpState = new JumpState(this);

		CurrentState = IdleState;

        mobColor = GetComponent<MeshRenderer>();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Health = 10;
    }

    private void Update()
    {
        CurrentState.UpdateState();

        if (transform.position.y <= -1)
            Death();
    }

    private void OnEnable()
    {
        GameManager.beforeSave += StoreData;
        GameManager.afterLoad += RestoreData;
        GameManager.destroyAllMobs += Death;
    }

    private void OnDisable()
    {
        GameManager.beforeSave -= StoreData;
        GameManager.afterLoad -= RestoreData;
        GameManager.destroyAllMobs -= Death;
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    #endregion

    #region Override

    public override void StoreData()
    {
        data.mobName = mobName;
        data.health = _health;
        data.xPos = transform.position.x;
        data.yPos = transform.position.y;
        data.zPos = transform.position.z;
		data.yRot = transform.rotation.eulerAngles.y;

		StateSerializer ss = new StateSerializer();
		ss.SaveState(this);
		data.state = ss;

        GameManager.Instance.Data.mobs.Add(data);
    }

    public override void RestoreData()
    {
        mobName = data.mobName;
        Health = data.health;
        transform.position = new Vector3(data.xPos, data.yPos, data.zPos);
		Quaternion rotation = new Quaternion();
		rotation.eulerAngles = new Vector3(0, data.yRot);
		transform.rotation = rotation;

		StateSerializer ss = data.state;
		ss.LoadState(this);

    }

    #endregion

    #region IDamageable

    public void TakeDamage(int damage)
    {
        Health -= damage;
        CurrentState = (CurrentState == AttackState) ? JumpState : AttackState;
    }

    public void Death()
    {
        StartCoroutine(Die(.5f));
    }

    IEnumerator Die(float time)
    {
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        GameObject go = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Destroy(go, 2f);

    }

    #endregion

    #region IAttackable

    public void Attack()
    {
        if (bullets > 0)
        {
            bullets--;

			GameObject bullet = BulletPool.Instance.GetBullet();

			bullet.transform.position = gunBarrel.position;
			bullet.transform.rotation = gunBarrel.rotation;
			bullet.SetActive(true);

			Rigidbody rb = bullet.GetComponent<Rigidbody>();
			rb.WakeUp();
			rb.AddForce(transform.forward * 300f);

        }
        else
        {
            CurrentState = IdleState;
            bullets = 20;
        }

    }

    #endregion

    #region IPointer

    public void OnDrag(PointerEventData eventData)
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

        transform.position = new Vector3(pos_move.x, transform.position.y, pos_move.z);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CurrentState = (CurrentState == AttackState) ? IdleState : JumpState;
    }

    #endregion
}
