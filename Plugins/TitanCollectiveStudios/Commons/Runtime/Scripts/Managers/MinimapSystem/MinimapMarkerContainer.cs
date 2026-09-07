using TitanCollectiveStudios.Commons.Managers;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class MinimapMarkerContainer : MonoBehaviour
{
    [SerializeField] Vector3 lockRotation = new Vector3(0, 0, 1);
    Canvas _canvas;
    Camera _camera;
    Quaternion _initialRotation;
    void Start()
    {
        //no need for this if is static
        if (gameObject.isStatic)
        {
            this.enabled = false;
            return;
        }

        _canvas = GetComponent<Canvas>();
        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.worldCamera = _camera = MinimapSystem.i.MinimapCamera;
        _initialRotation = transform.localRotation;


        //This cannot be done, because when the main object rotate this also rotates
        //this.transform.position = new Vector3(this.transform.position.x,
        //                                    _camera.transform.position.y - 1,
        //                                    this.transform.position.z);
    }

    private void Update()
    {
        transform.localRotation = new Quaternion(
            lockRotation.x > 0 ? transform.localRotation.x : _initialRotation.x,
            lockRotation.y > 0 ? transform.localRotation.y : _initialRotation.y,
            lockRotation.z > 0 ? transform.localRotation.z : _initialRotation.z,
            transform.localRotation.w);
    }
}
