using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public static event System.Action OnGuardHasSpottedPlayer;

    public float speed = 5f;
    public float waitTime = 0.25f;
    public float turnSpeed = 90f;

    public float timeToSpot = 0.5f;
    float playerVisibeTimer;

    public LayerMask viewMask;

    public Transform pathHolder;
    Transform player;

    public Light spotLight;
    public float viewDistance;
    float viewAngle;

    Color originalSpotlightColor;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        originalSpotlightColor = spotLight.color;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++) {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
    }

    bool CanSeePlayer() {
        if (Vector3.Distance(transform.position, player.position) < viewDistance) {
            Vector3 _dirToPlayer = (player.position - transform.position).normalized;
            float _angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, _dirToPlayer);
            if (_angleBetweenGuardAndPlayer < viewAngle / 2f) {
                if(!Physics.Linecast(transform.position, player.position, viewMask)){
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator FollowPath(Vector3[] waypoints) {
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true) {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint) {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget) {
        Vector3 _dirToLookTarget = (lookTarget - transform.position).normalized;
        float _targetAngle = 90 - Mathf.Atan2(_dirToLookTarget.z, _dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, _targetAngle)) > 0.05) {
            float _angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, _targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * _angle;
            yield return null;
        }
    }

    void Update() {
        if (CanSeePlayer()) {
            playerVisibeTimer += Time.deltaTime;
        } else {
            playerVisibeTimer -= Time.deltaTime;
        }

        playerVisibeTimer = Mathf.Clamp(playerVisibeTimer, 0, timeToSpot);
        spotLight.color = Color.Lerp(originalSpotlightColor, Color.red, playerVisibeTimer / timeToSpot);

        if(playerVisibeTimer >= timeToSpot){
            if(OnGuardHasSpottedPlayer != null) {
                OnGuardHasSpottedPlayer();
            }
        }
    }

    private void OnDrawGizmos() {
        Vector3 _startPos = pathHolder.GetChild(0).position;
        Vector3 _previousPos = _startPos;

        foreach (Transform waypoint in pathHolder) {
            Gizmos.DrawSphere(waypoint.position, 0.25f);
            Gizmos.DrawLine(_previousPos, waypoint.position);
            _previousPos = waypoint.position;
        }

        Gizmos.DrawLine(_previousPos, _startPos);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
