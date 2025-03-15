using UnityEngine;
namespace SPWN
{
    public class TurretController : MonoBehaviour
    {
        public GameObject firingPosition;
        public GameObject bulletPrefab;
        public int bulletSpeed = 100;

        public GameObject turretBody;
        public GameObject turretBarrel;
        private Transform target;
        public bool activated = false;

        [SerializeField] private float detectionRadius = 1000f; // Radius for detecting targets
        private Collider[] detectedTargets = new Collider[100]; // Adjust size as needed


        [SerializeField] private float horizontalRotationSpeed = 5f;
        [SerializeField] private float verticalRotationSpeed = 3f;
        [SerializeField] private float verticalLookLimit = 80f;
        [SerializeField] private KeyCode fireKey = KeyCode.KeypadPeriod; // Key to fire the turret

        public float fireRate = 0.1f; // Time in seconds between bullet fires
        private float timeSinceLastShot; // Time since the last bullet was fired

        private Quaternion defaultBodyRotation;
        private Quaternion defaultBarrelRotation;

        void Start()
        {
            if (turretBody != null)
                defaultBodyRotation = turretBody.transform.rotation;
            if (turretBarrel != null)
                defaultBarrelRotation = turretBarrel.transform.localRotation;
        }
        void Update()
        {
            // Update the time since the last shot
            timeSinceLastShot += Time.deltaTime;

            // Perform the overlap sphere check
            int numColliders = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, detectedTargets);
            Debug.Log($"Colliders: {numColliders}");
            // Find the closest target or perform other logic to select a target
            Transform closestTarget = FindClosestTarget(numColliders);

            if (closestTarget != null)
            {
                if (!activated)
                {
                    ActivateTurret(closestTarget);
                }
                else
                {
                    target = closestTarget;
                    RotateTurretBody();
                    RotateTurretBarrel();

                    // Check if the fire key is held down and the fire rate cooldown has passed
                    if (Input.GetKey(fireKey) && timeSinceLastShot >= fireRate)
                    {
                        FireBullet();
                        timeSinceLastShot = 0f; // Reset the timer
                    }
                    // Log the target information
                    Debug.Log($"Turret Target: {target.name}, Distance: {Vector3.Distance(transform.position, target.position)}");
                }
            }
            else
            {
                if (activated)
                {
                    DeactivateTurret();
                }
                RotateBackToDefault();
            }
        }

        private Transform FindClosestTarget(int numColliders)
        {
            
                Transform closest = null;
                float minDistance = float.MaxValue;

                for (int i = 0; i < numColliders; i++)
                {
                    Collider collider = detectedTargets[i];

                    // Check if the collider has the IncomingProjectile tag
                    if (collider.CompareTag("IncomingProjectile"))
                    {
                        Transform current = collider.transform;
                        float distance = Vector3.Distance(transform.position, current.position);

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closest = current;
                        }
                    }
                }

                return closest;
            
        }

        public void ActivateTurret(Transform newTarget)
        {
            target = newTarget;
            activated = true;
        }

        public void DeactivateTurret()
        {
            target = null;
            activated = false;
        }

        private void RotateTurretBody()
        {
            Vector3 directionToTarget = target.position - turretBody.transform.position;
            directionToTarget.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            turretBody.transform.rotation = Quaternion.Slerp(turretBody.transform.rotation, targetRotation, horizontalRotationSpeed * Time.deltaTime);
        }

        private void RotateTurretBarrel()
        {
            Vector3 directionToTarget = target.position - turretBarrel.transform.position;
            float distance = new Vector2(directionToTarget.x, directionToTarget.z).magnitude;
            float angleToTarget = Mathf.Atan2(directionToTarget.y, distance) * Mathf.Rad2Deg;
            angleToTarget = Mathf.Clamp(angleToTarget, -verticalLookLimit, verticalLookLimit); //something like this

            Quaternion barrelRotation = Quaternion.Euler(-angleToTarget, turretBarrel.transform.localEulerAngles.y, 0);
            turretBarrel.transform.localRotation = Quaternion.Slerp(turretBarrel.transform.localRotation, barrelRotation, verticalRotationSpeed * Time.deltaTime);
        }

        private void RotateBackToDefault()
        {
            if (turretBody != null)
                turretBody.transform.rotation = Quaternion.Slerp(turretBody.transform.rotation, defaultBodyRotation, (horizontalRotationSpeed * .25f) * Time.deltaTime);

            if (turretBarrel != null)
                turretBarrel.transform.localRotation = Quaternion.Slerp(turretBarrel.transform.localRotation, defaultBarrelRotation, (verticalRotationSpeed * .25f) * Time.deltaTime);
        }

        private void FireBullet()
        {
            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, firingPosition.transform.position, firingPosition.transform.rotation);

            // Set the velocity of the bullet's Rigidbody
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firingPosition.transform.forward * bulletSpeed;
            }
        }

        private void OnDrawGizmos()
        {
            Vector3 barrelPos = turretBarrel.transform.position;
            Vector3 turretFwd = turretBody.transform.forward;
            Vector3 barrelRight = turretBarrel.transform.right;

            Dbug.Circle(turretBody.transform.position, detectionRadius, Vector3.up, Color.cyan);
            Dbug.Line(barrelPos, barrelPos + Quaternion.AngleAxis(verticalLookLimit, barrelRight) * turretFwd * 3f, Color.cyan);
            Dbug.Line(barrelPos, barrelPos + Quaternion.AngleAxis(-verticalLookLimit, barrelRight) * turretFwd * 3f, Color.cyan);
            Dbug.Circle(barrelPos, 3f, barrelRight, Color.cyan);
        }
    }
}