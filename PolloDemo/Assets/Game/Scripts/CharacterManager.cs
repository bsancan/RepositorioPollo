using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager CharacterManagerInstance;
    public Character _Character;
    public Camera PlayerCamera;
    public LineRenderer LineRayCast;

    private LeftJoystick _LeftJoystick;
    private RightJoystick _RightJoystick;

    Vector3 JsPosition;
    Vector2 jsPosFixec;
    float joystickHandleDistance = 1f;

    private float nextFire;

    private Transform charTransform;
    //SOLO PARA PRUEBAS
    private Vector3 mandoIzq;
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

        charTransform = _Character.GetComponent<Transform>();

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_Character.Muerto)
            return;

        _LeftJoystick = GameManager.GameManagerInstance._UiManager.TouchController.leftJoystickBackgroundImage.GetComponent<LeftJoystick>();
        _RightJoystick = GameManager.GameManagerInstance._UiManager.TouchController.rightJoystickBackgroundImage.GetComponent<RightJoystick>();

        if(GameManager.GameManagerInstance.ActualNivel == GameManager.GameManagerInstance.e_Nivel01  && _Character.EnMovimiento)
        {
            MovimientoCharEnXY();

            MovimientoCrossHair2();
        }
        if (GameManager.GameManagerInstance.ActualNivel == GameManager.GameManagerInstance.e_Nivel02 && _Character.EnMovimiento)
        {
            MovimientoCharEnXYTunel();

            MovimientoCrossHair2();
        }

        


    }

    public void ResetCharacterManager()
    {
        transform.position = Vector3.zero;
        _Character.ResetCharacter();

    }
    

    private void MovimientoCharEnXY()
    {

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            mandoIzq = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (mandoIzq == Vector3.zero)
            {
                _Character.AnimacionEnX(0f);
            }
            else if (mandoIzq != Vector3.zero)
            {
                Vector3 accelXY = new Vector3(mandoIzq.x * _Character.VelocidadXY,
               mandoIzq.y * _Character.VelocidadXY,
               0f);
                Vector3 velocity = charTransform.localPosition + accelXY * Time.deltaTime;
                Vector3 velFixed = new Vector3(
                    Mathf.Clamp(velocity.x, -_Character.MinRange.x, _Character.MaxRange.x),
                     Mathf.Clamp(velocity.y, -_Character.MinRange.y, _Character.MaxRange.y),
                     velocity.z
                    );

                charTransform.localPosition = velFixed;
                _Character.AnimacionEnX(mandoIzq.x);
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            // if there is no input on the left joystick
            if (_LeftJoystick.GetInputDirection() == Vector3.zero)
            {
                _Character.AnimacionEnX(0f);

            }
            else if (_LeftJoystick.GetInputDirection() != Vector3.zero)
            {
                Vector3 accelXY = new Vector3(_LeftJoystick.GetInputDirection().x * _Character.VelocidadXY,
                    _LeftJoystick.GetInputDirection().y * _Character.VelocidadXY,
                    0f);
                Vector3 velocity = charTransform.localPosition + accelXY * Time.deltaTime;
                Vector3 velFixed = new Vector3(
                    Mathf.Clamp(velocity.x, -_Character.MinRange.x, _Character.MaxRange.x),
                     Mathf.Clamp(velocity.y, -_Character.MinRange.y, _Character.MaxRange.y),
                     velocity.z
                    );

                charTransform.localPosition = velFixed;
                _Character.AnimacionEnX(_LeftJoystick.GetInputDirection().x);
                //aniPlayer.SetFloat(horizontalHash, xAxis, 0.1f, animationSpeed * Time.deltaTime);
            }
        }

        Quaternion newRot = Quaternion.Euler(-10f * _RightJoystick.GetInputDirection().y, 20f * _RightJoystick.GetInputDirection().x, 0f);
        _Character.Model.transform.localRotation = Quaternion.Lerp(_Character.Model.transform.localRotation,
            newRot, Time.deltaTime * _Character.VelocidadRotacion);

        transform.position += (transform.forward * _Character.VelocidadZ * Time.deltaTime);
    }

    private void MovimientoCharEnXYTunel()
    {

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            mandoIzq = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (mandoIzq == Vector3.zero)
            {
                _Character.AnimacionEnX(0f);
            }
            else if (mandoIzq != Vector3.zero)
            {
                Vector3 accelXY = new Vector3(mandoIzq.x * _Character.VelocidadXY,
               mandoIzq.y * _Character.VelocidadXY,
               0f);
                Vector3 velocity = charTransform.localPosition + accelXY * Time.deltaTime;
                Vector3 velFixed = new Vector3(
                    Mathf.Clamp(velocity.x, -_Character.MinRange.x, _Character.MaxRange.x),
                     Mathf.Clamp(velocity.y, -_Character.MinRange.y, _Character.MaxRange.y),
                     velocity.z
                    );

                charTransform.localPosition = velFixed;
                _Character.AnimacionEnX(mandoIzq.x);
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            // if there is no input on the left joystick
            if (_LeftJoystick.GetInputDirection() == Vector3.zero)
            {
                _Character.AnimacionEnX(0f);

            }
            else if (_LeftJoystick.GetInputDirection() != Vector3.zero)
            {
                Vector3 accelXY = new Vector3(_LeftJoystick.GetInputDirection().x * _Character.VelocidadXY,
                    _LeftJoystick.GetInputDirection().y * _Character.VelocidadXY,
                    0f);
                Vector3 velocity = charTransform.localPosition + accelXY * Time.deltaTime;
                Vector3 velFixed = new Vector3(
                    Mathf.Clamp(velocity.x, -_Character.MinRange.x, _Character.MaxRange.x),
                     Mathf.Clamp(velocity.y, -_Character.MinRange.y, _Character.MaxRange.y),
                     velocity.z
                    );

                charTransform.localPosition = velFixed;
                _Character.AnimacionEnX(_LeftJoystick.GetInputDirection().x);
                //aniPlayer.SetFloat(horizontalHash, xAxis, 0.1f, animationSpeed * Time.deltaTime);
            }
        }

        Quaternion newRot = Quaternion.Euler(-10f * _RightJoystick.GetInputDirection().y, 20f * _RightJoystick.GetInputDirection().x, 0f);
        _Character.Model.transform.localRotation = Quaternion.Lerp(_Character.Model.transform.localRotation,
            newRot, Time.deltaTime * _Character.VelocidadRotacion);

        
        transform.position = Nivel02Manager.Nivel02ManagerInstance.TunelRecorrido.position;
        transform.rotation = Nivel02Manager.Nivel02ManagerInstance.ObtenerNuevaRotacionDelRecorrido();
    }

    private void MovimientoCrossHair()
    {

        //Posicion del CrossHair desde la camara al mundo
        Vector3 Target = PlayerCamera.ScreenToWorldPoint(
            new Vector3(GameManager.GameManagerInstance._UiManager.RectCrossHairB.position.x,
            GameManager.GameManagerInstance._UiManager.RectCrossHairB.position.y,
            _Character.DistanciaRaycast));
        
        //Obtengo el movimiento del Joystick derecho y desactivo la animacion de disparo
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

        //transformo el vector jposition en viewport
        Vector2 viewPortPosA = PlayerCamera.ScreenToViewportPoint(JsPosition);
        Vector2 screenPosA = new Vector2(
                ((viewPortPosA.x * GameManager.GameManagerInstance._UiManager.RectCrossHairParent.sizeDelta.x) - (GameManager.GameManagerInstance._UiManager.RectCrossHairParent.sizeDelta.x * 0.5f)),
                ((viewPortPosA.y * GameManager.GameManagerInstance._UiManager.RectCrossHairParent.sizeDelta.y) - (GameManager.GameManagerInstance._UiManager.RectCrossHairParent.sizeDelta.y * 0.5f)));

        //controlo que el crosshair no pase los limites de la pantalla
        screenPosA = new Vector3(Mathf.Clamp(screenPosA.x, -(Screen.width / 2), Screen.width / 2),
            Mathf.Clamp(screenPosA.y, -(Screen.height / 2), Screen.height / 2), 0f);
        
        //Muevo el crosshairB dependiendo del vector ScreenPosA
        GameManager.GameManagerInstance._UiManager.RectCrossHairB.anchoredPosition = Vector2.Lerp(
            GameManager.GameManagerInstance._UiManager.RectCrossHairB.anchoredPosition, screenPosA,
            Time.deltaTime * _Character.VelocidadCrossHair
            );


        if (_RightJoystick.GetInputDirection() != Vector3.zero)
        {
            _Character.AnimacionDeDisparo(true);

            //disparo de ammo
            if (Time.time > nextFire)
            {
                nextFire = Time.time + GameManager.GameManagerInstance._AmmoManager.VelocidadFuego;
                GameManager.GameManagerInstance._AmmoManager.ObtenerPlayerAmmo(_Character.PosicionLaserCentral);

                //funciona con hit
                Ray _ray = PlayerCamera.ScreenPointToRay(JsPosition);
                RaycastHit _rayHit;
                if (Physics.Raycast(_ray, out _rayHit, _Character.DistanciaRaycast))
                {
                    //Para trazar una linea de alcance del raycast
                    //Debug.DrawRay(_ray.origin, _ray.direction * _Character.DistanciaRaycast, Color.yellow, 2.0f);

                    if (_rayHit.collider.gameObject.CompareTag("Asteroid") || _rayHit.collider.gameObject.CompareTag("Enemy"))
                    {
                        GameManager.GameManagerInstance._UiManager.SetCrossHairColor3();
                        _Character.PosicionLaserCentral.LookAt(_rayHit.collider.transform);
                    }
                    else
                    {
                        GameManager.GameManagerInstance._UiManager.SetCrossHairColor2();
                        _Character.PosicionLaserCentral.LookAt(_rayHit.collider.transform);
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

    private void MovimientoCrossHair2()
    {

        //Obtengo el movimiento del Joystick derecho y desactivo la animacion de disparo
        JsPosition = _RightJoystick.GetInputDirection();
       
        JsPosition = new Vector2(
          Mathf.Clamp(JsPosition.x, -0.5f, 0.5f),
          Mathf.Clamp(JsPosition.y, -0.5f, 0.5f));
       
        jsPosFixec = new Vector2(
            JsPosition.x * (GameManager.GameManagerInstance._UiManager.RectCrossHairParent.sizeDelta.x / joystickHandleDistance),
            JsPosition.y * (GameManager.GameManagerInstance._UiManager.RectCrossHairParent.sizeDelta.y / joystickHandleDistance));

        //Muevo el crosshairB dependiendo del vector ScreenPosA
        GameManager.GameManagerInstance._UiManager.RectCrossHairB.anchoredPosition = Vector2.Lerp(
            GameManager.GameManagerInstance._UiManager.RectCrossHairB.anchoredPosition, 
            jsPosFixec,
            Time.deltaTime * _Character.VelocidadCrossHair);

        //Posicion del CrossHair desde la camara al mundo

        Vector3 screenToVport = PlayerCamera.ScreenToViewportPoint(GameManager.GameManagerInstance._UiManager.RectCrossHairB.position);

        if (JsPosition != Vector3.zero)
        {
            _Character.AnimacionDeDisparo(true);
            //disparo de ammo
            if (Time.time > nextFire)
            {
                nextFire = Time.time + GameManager.GameManagerInstance._AmmoManager.VelocidadFuego;

                //StartCoroutine(MostrarLinea());
                //LineRayCast.SetPosition(0, _Character.PosicionLaserCentral.position);

                //funciona con hit
                Ray _ray = PlayerCamera.ViewportPointToRay(screenToVport);

                //target alterno cuando no se logra un Hit en raycast
                Vector3 targetNoRayCast = PlayerCamera.ScreenToWorldPoint(
                        new Vector3(GameManager.GameManagerInstance._UiManager.RectCrossHairB.position.x,
                        GameManager.GameManagerInstance._UiManager.RectCrossHairB.position.y,
                        _Character.DistanciaRaycast));

                RaycastHit _rayHit;
                if (Physics.Raycast(_ray, out _rayHit, _Character.DistanciaRaycast))
                {
                    if (_rayHit.collider.gameObject.CompareTag("Asteroid") || _rayHit.collider.gameObject.CompareTag("Enemy"))
                    {
                        //LineRayCast.SetPosition(1, _rayHit.point);
                        GameManager.GameManagerInstance._UiManager.SetCrossHairColor3();
                    }
                    else
                    {
                        GameManager.GameManagerInstance._UiManager.SetCrossHairColor2();
                    }
                    _Character.PosicionLaserCentral.LookAt(_rayHit.point);
                    //print("HIT - " + _rayHit.collider.gameObject.name + " < " + _rayHit.collider.transform.position + ">");
                }
                else
                {
                    //LineRayCast.SetPosition(1, _Character.PosicionLaserCentral.position + (PlayerCamera.transform.forward * _Character.DistanciaRaycast));
                    GameManager.GameManagerInstance._UiManager.SetCrossHairColor2();
                    _Character.PosicionLaserCentral.LookAt(targetNoRayCast);
                    //print("=== NO HIT - " + targetNoRayCast);
                }

                GameManager.GameManagerInstance._AmmoManager.ObtenerPlayerAmmo(_Character.PosicionLaserCentral);
            }
        }
       
        

    }

    IEnumerator MostrarLinea()
    {
        LineRayCast.enabled = true;

        yield return new WaitForSeconds(0.7f);

        LineRayCast.enabled = false;
    }


}
