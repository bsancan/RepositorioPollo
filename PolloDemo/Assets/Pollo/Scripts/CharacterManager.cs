using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager CharacterManagerInstance;
    public Character _Character;
    public Camera PlayerCamera;

    private LeftJoystick _LeftJoystick;
    private RightJoystick _RightJoystick;

    private float nextFire;

    private void Awake()
    {
        if (CharacterManagerInstance == null)
        {
            CharacterManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        _LeftJoystick = GameManager.GameManagerInstance._UiManager.TouchController.leftJoystickBackgroundImage.GetComponent<LeftJoystick>();
        _RightJoystick = GameManager.GameManagerInstance._UiManager.TouchController.rightJoystickBackgroundImage.GetComponent<RightJoystick>();

        MovimientoCharEnXY();

        MovimientoCrossHair();

    }

    private void MovimientoCharEnXY()
    {

        // if there is no input on the left joystick
        if (_LeftJoystick.GetInputDirection() == Vector3.zero)
        {
            _Character.AnimacionEnX(0f);

        }else if (_LeftJoystick.GetInputDirection() != Vector3.zero)
        {
            Vector3 accelXY = new Vector3(_LeftJoystick.GetInputDirection().x * _Character.VelocidadXY,
                _LeftJoystick.GetInputDirection().y * _Character.VelocidadXY,
                0f);
            Vector3 velocity = transform.localPosition + accelXY * Time.deltaTime;
            Vector3 velFixed = new Vector3(
                Mathf.Clamp(velocity.x, -_Character.MinRange.x, _Character.MaxRange.x),
                 Mathf.Clamp(velocity.y, -_Character.MinRange.y, _Character.MaxRange.y),
                 velocity.z
                );

            transform.localPosition = velFixed;
            _Character.AnimacionEnX(_LeftJoystick.GetInputDirection().x);


            //aniPlayer.SetFloat(horizontalHash, xAxis, 0.1f, animationSpeed * Time.deltaTime);
        }

        Quaternion newRot = Quaternion.Euler(-10f * _RightJoystick.GetInputDirection().y, 20f * _RightJoystick.GetInputDirection().x, 0f);
        _Character.Model.transform.localRotation = Quaternion.Lerp(_Character.Model.transform.localRotation,
            newRot, Time.deltaTime * _Character.VelocidadRotacion);

        transform.localPosition += (transform.forward * _Character.VelocidadZ);
    }

    private void MovimientoCrossHair()
    {
        //Obtengo un nuevo vector para rotar al Player y los lasers
        Vector3 Target = PlayerCamera.ScreenToWorldPoint(
            new Vector3(GameManager.GameManagerInstance._UiManager.RectCrossHairA.position.x,
            GameManager.GameManagerInstance._UiManager.RectCrossHairA.position.y,
            _Character.DistanciaRaycast));

        Vector3 JsPosition;
        if (_RightJoystick.GetInputDirection() != Vector3.zero)
        {
            JsPosition = new Vector3(
               Screen.width / 2 + (_RightJoystick.GetInputDirection().x * Screen.width * 0.75f),
               Screen.height / 2 + (_RightJoystick.GetInputDirection().y * Screen.height * 0.75f),
               0f);
        }
        else
        {
            JsPosition = new Vector3(
               Screen.width / 2,
               Screen.height / 2,
               0f);
            _Character.AnimacionDeDisparo(false);
        }

        //transformo el vector jposition en screen position
        Vector2 viewPortPosA = PlayerCamera.ScreenToViewportPoint(JsPosition);
        Vector2 screenPosA = new Vector2(
                ((viewPortPosA.x * GameManager.GameManagerInstance._UiManager.RectCrossHairParent.sizeDelta.x) - (GameManager.GameManagerInstance._UiManager.RectCrossHairParent.sizeDelta.x * 0.5f)),
                ((viewPortPosA.y * GameManager.GameManagerInstance._UiManager.RectCrossHairParent.sizeDelta.y) - (GameManager.GameManagerInstance._UiManager.RectCrossHairParent.sizeDelta.y * 0.5f)));

        //controlo que el crosshair no pase los limites de la pantalla
        screenPosA = new Vector3(Mathf.Clamp(screenPosA.x, -(Screen.width / 2), Screen.width / 2),
            Mathf.Clamp(screenPosA.y, -(Screen.height / 2), Screen.height / 2), 0f);

        GameManager.GameManagerInstance._UiManager.RectCrossHairA.anchoredPosition = Vector2.Lerp(
            GameManager.GameManagerInstance._UiManager.RectCrossHairA.anchoredPosition, screenPosA,
            Time.deltaTime * _Character.VelocidadCrossHair
            );

        if (_RightJoystick.GetInputDirection() != Vector3.zero)
        {
            _Character.AnimacionDeDisparo(true);

            //disparo de ammo
            if (Time.time > nextFire)
            {
                nextFire = Time.time + _Character.VelocidadFuego;
                GameManager.GameManagerInstance._AmmoManager.ObtenerPlayerAmmo(_Character.PosicionLaserCentral);

                //funciona con hit
                Ray _ray = PlayerCamera.ScreenPointToRay(JsPosition);
                RaycastHit _rayHit;
                if (Physics.Raycast(_ray, out _rayHit, _Character.DistanciaRaycast))
                {
                    if (_rayHit.collider.gameObject.CompareTag("Asteroid") || _rayHit.collider.gameObject.CompareTag("Enemy"))
                    {
                        GameManager.GameManagerInstance._UiManager.SetCrossHairColor3();
                        _Character.PosicionLaserCentral.LookAt(_rayHit.collider.transform);
                    }
                    else
                    {
                        GameManager.GameManagerInstance._UiManager.SetCrossHairColor2();
                        _Character.PosicionLaserCentral.LookAt(Target);
                    }
                }
                else
                {
                    GameManager.GameManagerInstance._UiManager.SetCrossHairColor2();
                    _Character.PosicionLaserCentral.LookAt(Target);
                }

            }

        }
        _Character.PosicionLaserCentral.LookAt(Target);
    }

}
