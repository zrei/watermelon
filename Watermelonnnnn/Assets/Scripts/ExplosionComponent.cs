using UnityEngine;

public class ExplosionComponent : SpecialComponent
{
    [SerializeField]
    private float m_ExplosionTimerMin = 5;
    [SerializeField]
    private float m_ExplosionTimerMax = 7;
    [SerializeField]
    private float m_ExplosionRadius = 3;
    [SerializeField]
    private float m_ExplosionForce = 2000;
    [SerializeField]
    private LayerMask m_AffectedLayers;
    [SerializeField]
    private float m_FlashTimerInterval = 0.3f;

    private float m_ExplosionTimer;
    protected Rigidbody2D m_RB;
    private SpriteRenderer m_SR;
    private float m_FlashTimer = 0;

    protected virtual void Awake()
    {
        m_AffectedLayers += LayerMask.GetMask("Fruit");
        m_RB = GetComponent<Rigidbody2D>();
        m_SR = GetComponent<SpriteRenderer>();
        m_ExplosionTimer = UnityEngine.Random.Range(m_ExplosionTimerMin, m_ExplosionTimerMax);
    }

    protected virtual void Update()
    {
        m_FlashTimer += Time.deltaTime;
        if (m_FlashTimer >= m_FlashTimerInterval)
        {
            m_SR.color = new Color(m_SR.color.r, 1 - m_SR.color.g, 1 - m_SR.color.b);
            m_FlashTimer = 0;
        }

        m_ExplosionTimer -= Time.deltaTime;
        if (m_ExplosionTimer <= 0)
        {
            Collider2D[] m_Objects = Physics2D.OverlapCircleAll(m_RB.position, m_ExplosionRadius, m_AffectedLayers);
            foreach (Collider2D o in m_Objects)
            {
                Vector2 otherPosition = new Vector2(o.gameObject.transform.localPosition.x, o.gameObject.transform.localPosition.y);
                Vector2 directionVector = (otherPosition - m_RB.position).normalized;
                o.gameObject.GetComponent<Rigidbody2D>().AddForce(directionVector * m_ExplosionForce);
            }
            // explode radius... something
            GlobalEvents.OnExplodeEvent?.Invoke(gameObject);
            Destroy(gameObject);
        }
    }
}