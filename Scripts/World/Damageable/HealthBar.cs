using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject _healthBar; //The actual bar that moves
    [SerializeField]
    private GameObject _healthPivot; //The parent gameobject of the health bar

    private IHealth _health;

    private void Start()
    {
        _health = GetComponent<IHealth>();
    }

    private void Update()
    {
        ChangeHealthBar();
    }

    public void ChangeHealthBar()
    {
        float xScale = _health.GetHealth()/_health.GetMaxHealth();
        _healthBar.transform.localScale = new(xScale, 1, 1);
        _healthBar.transform.localPosition = new((xScale-1)/2.0f, 0, -.001f);

        LookToCamera();
    }

    private void LookToCamera()
    {
        Vector3 p = Camera.main.transform.position - transform.position;
        p.y = 0;
        Quaternion d = Quaternion.LookRotation(p);
        transform.rotation = d;
    }
}
