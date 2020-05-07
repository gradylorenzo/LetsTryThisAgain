using UnityEngine;

namespace Core
{
    public class TurretController : MonoBehaviour
    {
        #region Public Fields
        [Header("Rotations")]

        [Tooltip("Transform of the turret's azimuthal rotations.")]
        [SerializeField] public Transform turretBase = null;

        [Tooltip("Transform of the turret's elevation rotations. ")]
        [SerializeField] public Transform turretBarrels = null;

        [Header("Elevation")]
        [Tooltip("Speed at which the turret's guns elevate up and down.")]
        public float azimuthSpeed = 30f;

        [Tooltip("Highest upwards elevation the turret's barrels can aim.")]
        [Range(0, 179)] public float maxAzimuth = 60f;

        [Tooltip("Lowest downwards elevation the turret's barrels can aim.")]
        [Range(0, 179)] public float minAzimuth = 5f;

        [Header("Traverse")]

        [Tooltip("Speed at which the turret can rotate left/right.")]
        public float bearingSpeed = 60f;

        [Tooltip("When true, the turret can only rotate horizontally with the given limits.")]
        [SerializeField] private bool hasLimitedTraverse = false;
        [Range(0, 179)] public float minBearing = 120f;
        [Range(0, 179)] public float maxBearing = 120f;

        [Header("Behavior")]

        [Tooltip("When idle, the turret does not aim at anything and simply points forwards.")]
        public bool IsIdle = false;

        [Tooltip("Position the turret will aim at when not idle. Set this to whatever you want" +
            "the turret to actively aim at.")]
        public Vector3 AimPosition = Vector3.zero;

        [Tooltip("When the turret is within this many degrees of the target, it is considered aimed.")]
        [SerializeField] private float aimedThreshold = 5f;
        private float limitedTraverseAngle = 0f;

        [Header("Debug")]
        public bool DrawDebugRay = true;
        public bool DrawDebugArcs = false;
        #endregion
        #region Private Fields
        private float angleToTarget = 0f;
        private float elevation = 0f;

        private bool hasBarrels = false;

        private bool isAimed = false;
        private bool isBaseAtRest = false;
        private bool isBarrelAtRest = false;
        #endregion
        #region Properties
        /// <summary>
        /// True when the turret cannot rotate freely in the horizontal axis.
        /// </summary>
        public bool limitBearing { get { return hasLimitedTraverse; } set { hasLimitedTraverse = value; } }
        /// <summary>
        /// True when the turret is idle and at its resting position.
        /// </summary>
        public bool IsTurretAtRest { get { return isBarrelAtRest && isBaseAtRest; } }
        /// <summary>
        /// True when the turret is aimed at the given <see cref="AimPosition"/>. When the turret
        /// is idle, this is never true.
        /// </summary>
        public bool IsAimed { get { return isAimed; } }
        /// <summary>
        /// Angle in degress to the given <see cref="AimPosition"/>. When the turret is idle,
        /// the angle reports 999.
        /// </summary>
        public float AngleToTarget { get { return IsIdle ? 999f : angleToTarget; } }
        #endregion
        #region MB Methods
        private void Awake()
        {
            hasBarrels = turretBarrels != null;
            if (turretBase == null)
                Debug.LogError(name + ": TurretAim requires an assigned TurretBase!");
        }
        private void Update()
        {
            if (IsIdle)
            {
                if (!IsTurretAtRest)
                    RotateTurretToIdle();
                isAimed = false;
            }
            else
            {
                RotateBaseToFaceTarget(AimPosition);

                if (hasBarrels)
                    RotateBarrelsToFaceTarget(AimPosition);

                // Turret is considered "aimed" when it's pointed at the target.
                angleToTarget = GetTurretAngleToTarget(AimPosition);

                // Turret is considered "aimed" when it's pointed at the target.
                isAimed = angleToTarget < aimedThreshold;

                isBarrelAtRest = false;
                isBaseAtRest = false;
            }
        }
        #endregion
        #region Private Methods
        private float GetTurretAngleToTarget(Vector3 targetPosition)
        {
            float angle = 999f;

            if (hasBarrels)
            {
                angle = Vector3.Angle(targetPosition - turretBarrels.position, turretBarrels.forward);
            }
            else
            {
                Vector3 flattenedTarget = Vector3.ProjectOnPlane(
                    targetPosition - turretBase.position,
                    turretBase.up);

                angle = Vector3.Angle(
                    flattenedTarget - turretBase.position,
                    turretBase.forward);
            }

            return angle;
        }
        private void RotateTurretToIdle()
        {
            // Rotate the base to its default position.
            if (hasLimitedTraverse)
            {
                limitedTraverseAngle = Mathf.MoveTowards(
                    limitedTraverseAngle, 0f,
                    bearingSpeed * Time.deltaTime);

                if (Mathf.Abs(limitedTraverseAngle) > Mathf.Epsilon)
                    turretBase.localEulerAngles = Vector3.up * limitedTraverseAngle;
                else
                    isBaseAtRest = true;
            }
            else
            {
                turretBase.rotation = Quaternion.RotateTowards(
                    turretBase.rotation,
                    transform.rotation,
                    bearingSpeed * Time.deltaTime);

                isBaseAtRest = Mathf.Abs(turretBase.localEulerAngles.y) < Mathf.Epsilon;
            }

            if (hasBarrels)
            {
                elevation = Mathf.MoveTowards(elevation, 0f, azimuthSpeed * Time.deltaTime);
                if (Mathf.Abs(elevation) > Mathf.Epsilon)
                    turretBarrels.localEulerAngles = Vector3.right * -elevation;
                else
                    isBarrelAtRest = true;
            }
            else // Barrels automatically at rest if there are no barrels.
                isBarrelAtRest = true;
        }
        private void RotateBarrelsToFaceTarget(Vector3 targetPosition)
        {
            Vector3 localTargetPos = turretBase.InverseTransformDirection(targetPosition - turretBarrels.position);
            Vector3 flattenedVecForBarrels = Vector3.ProjectOnPlane(localTargetPos, Vector3.up);

            float targetElevation = Vector3.Angle(flattenedVecForBarrels, localTargetPos);
            targetElevation *= Mathf.Sign(localTargetPos.y);

            targetElevation = Mathf.Clamp(targetElevation, -minAzimuth, maxAzimuth);
            elevation = Mathf.MoveTowards(elevation, targetElevation, azimuthSpeed * Time.deltaTime);

            if (Mathf.Abs(elevation) > Mathf.Epsilon)
                turretBarrels.localEulerAngles = Vector3.right * -elevation;

        #if UNITY_EDITOR
            if (DrawDebugRay)
                Debug.DrawRay(turretBarrels.position, turretBarrels.forward * localTargetPos.magnitude, Color.red);
        #endif
        }
        private void RotateBaseToFaceTarget(Vector3 targetPosition)
        {
            Vector3 turretUp = transform.up;

            Vector3 vecToTarget = targetPosition - turretBase.position;
            Vector3 flattenedVecForBase = Vector3.ProjectOnPlane(vecToTarget, turretUp);

            if (hasLimitedTraverse)
            {
                Vector3 turretForward = transform.forward;
                float targetTraverse = Vector3.SignedAngle(turretForward, flattenedVecForBase, turretUp);

                targetTraverse = Mathf.Clamp(targetTraverse, -minBearing, maxBearing);
                limitedTraverseAngle = Mathf.MoveTowards(
                    limitedTraverseAngle,
                    targetTraverse,
                    bearingSpeed * Time.deltaTime);

                if (Mathf.Abs(limitedTraverseAngle) > Mathf.Epsilon)
                    turretBase.localEulerAngles = Vector3.up * limitedTraverseAngle;
            }
            else
            {
                turretBase.rotation = Quaternion.RotateTowards(
                    Quaternion.LookRotation(turretBase.forward, turretUp),
                    Quaternion.LookRotation(flattenedVecForBase, turretUp),
                    bearingSpeed * Time.deltaTime);
            }

#if UNITY_EDITOR
            if (DrawDebugRay && !hasBarrels)
                Debug.DrawRay(turretBase.position,
                    turretBase.forward * flattenedVecForBase.magnitude,
                    Color.red);
#endif
        }
        #endregion
        #region Public Methods
        public void SetTargetPoint(Vector3 point)
        {
            AimPosition = point;
        }
        #endregion

        #region UNITY_EDITOR
#if UNITY_EDITOR
        // This should probably go in an Editor script, but dealing with Editor scripts
        // is a pain in the butt so I'd rather not.
        private void OnDrawGizmosSelected()
        {
            if (!DrawDebugArcs)
                return;

            if (turretBase != null)
            {
                const float kArcSize = 10f;
                Color colorTraverse = new Color(1f, .5f, .5f, .1f);
                Color colorElevation = new Color(.5f, 1f, .5f, .1f);
                Color colorDepression = new Color(.5f, .5f, 1f, .1f);

                Transform arcRoot = turretBarrels != null ? turretBarrels : turretBase;

                // Red traverse arc
                UnityEditor.Handles.color = colorTraverse;
                if (hasLimitedTraverse)
                {
                    UnityEditor.Handles.DrawSolidArc(
                        arcRoot.position, turretBase.up,
                        transform.forward, maxBearing,
                        kArcSize);
                    UnityEditor.Handles.DrawSolidArc(
                        arcRoot.position, turretBase.up,
                        transform.forward, -minBearing,
                        kArcSize);
                }
                else
                {
                    UnityEditor.Handles.DrawSolidArc(
                        arcRoot.position, turretBase.up,
                        transform.forward, 360f,
                        kArcSize);
                }

                if (turretBarrels != null)
                {
                    // Green elevation arc
                    UnityEditor.Handles.color = colorElevation;
                    UnityEditor.Handles.DrawSolidArc(
                        turretBarrels.position, turretBarrels.right,
                        turretBase.forward, -maxAzimuth,
                        kArcSize);

                    // Blue depression arc
                    UnityEditor.Handles.color = colorDepression;
                    UnityEditor.Handles.DrawSolidArc(
                        turretBarrels.position, turretBarrels.right,
                        turretBase.forward, minAzimuth,
                        kArcSize);
                }
            }
        }
#endif
        #endregion
    }
}
