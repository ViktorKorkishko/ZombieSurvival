using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Camera _camera;

    [SerializeField] private Rig aimRig;
    [SerializeField] private float aimDuration;

    private void Update()
    {
        float yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            aimRig.weight += Time.deltaTime / aimDuration;
        }
        else
        {
            aimRig.weight -= Time.deltaTime / aimDuration;
        }
    }
}
