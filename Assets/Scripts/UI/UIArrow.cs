using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UIArrow : MonoBehaviour
{
    /*[SerializeField] private Transform _spaceShip;
    [SerializeField] private Transform _UFO;

    private bool _rotationStarted;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_rotationStarted)            
            StartCoroutine(Rotation());
    }

    void Update()
    {
        Debug.DrawLine(Vector2.zero, _spaceShip.up * 5, Color.red);
        Debug.DrawLine(Vector2.zero, (_UFO.position - _spaceShip.position), Color.blue);
    }

    IEnumerator Rotation()
    {
        _rotationStarted = true;
        
        float targetAngle = Vector2.SignedAngle(_spaceShip.up, (_UFO.position - _spaceShip.position));
        float saveTargetAngle = targetAngle;

        //targetAngle += _spaceShip.eulerAngles.z;
        
        Debug.Log($"Было: {saveTargetAngle} стало: {targetAngle}");

        float currentVelocity = 0, angle = transform.eulerAngles.z;

        while (Mathf.Floor(angle) != Mathf.Floor(targetAngle))
        {
            Debug.Log($"angle: {angle}, targetangle {targetAngle}");    
            angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentVelocity, 0.3f, 90);
            transform.eulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }

        transform.eulerAngles = new Vector3(0, 0, targetAngle);
        _rotationStarted = false;
    }*/

    [SerializeField] private Transform _spaceShip;
    [SerializeField] private Transform _UFO;
    [SerializeField] private Camera _camera;

    private void Update()
    {
        Vector2 fromPlayerToEnemy = _UFO.position - _spaceShip.position;
        Ray ray = new Ray(_spaceShip.position, fromPlayerToEnemy);
        Debug.DrawLine(_spaceShip.position, fromPlayerToEnemy);

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        float minDistance = Mathf.Infinity;

        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance))
            {
                if (distance < minDistance)
                    minDistance = distance;
            }
        }

        minDistance = Mathf.Clamp(minDistance, 0, fromPlayerToEnemy.magnitude);
        Vector2 worldPosition = ray.GetPoint(minDistance - 0.5f);

        transform.position = _camera.WorldToScreenPoint(worldPosition);
    }


}
