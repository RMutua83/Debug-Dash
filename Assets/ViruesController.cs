using UnityEngine;
using System;

public class VirusController : MonoBehaviour
{
    public event Action OnDestroyed;

    public float dispersalSpeed = 2f;
    public float dispersalDuration = 1.5f;

    private Vector3 dispersalDirection;
    private float dispersalTimer;

    private bool dispersing = true;

    void Start()
    {
        // Pick a random direction on the XZ plane
        Vector3 randomDir = UnityEngine.Random.insideUnitSphere;
        randomDir.y = 0;
        dispersalDirection = randomDir.normalized;
        dispersalTimer = dispersalDuration;
    }

    void Update()
    {
        if (dispersing)
        {
            transform.position += dispersalDirection * dispersalSpeed * Time.deltaTime;

            dispersalTimer -= Time.deltaTime;
            if (dispersalTimer <= 0)
            {
                dispersing = false;
                // TODO: Enable your normal virus behavior here
            }
        }
        else
        {
            // Normal virus behavior goes here (e.g., chasing player)
        }
    }

    void OnDestroy()
   {
    // Remove from active virus list if needed
    if (OnDestroyed != null)
        OnDestroyed.Invoke();

    // Add kill to counter
    KillCounter counter = FindObjectOfType<KillCounter>();
    if (counter != null)
    {
        counter.AddKill();
    }
}
}