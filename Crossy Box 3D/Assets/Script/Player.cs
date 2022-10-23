using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text stepText;
    [SerializeField] ParticleSystem dieParticles;
    [SerializeField, Range(0.01f, 1f)] float moveDuration = 0.2f;
    [SerializeField, Range(0.01f, 1f)] float jumpHeight = 0.5f;
    private int minZPos;
    private int extent;
    private float backBoundary;
    private float leftBoundary;
    private float rightBoundary;

    [SerializeField] private int maxTravel;

    public int MaxTravel { get => maxTravel; }
    [SerializeField] private int currentTravel;
    public int CurrentTravel{get => currentTravel;}

    public bool IsDie { get => this.enabled == false;}

    public void setUp(int minZPos, int extent)
    {
        backBoundary  = minZPos -1 ;
        leftBoundary = -(extent + 1);
        rightBoundary = extent + 1;
    }
    private void Update() 
    {
        var moveDir = Vector3.zero;
        if(Input.GetKeyDown(KeyCode.UpArrow))
            moveDir += new Vector3(0, 0, 0.5f);
        if(Input.GetKeyDown(KeyCode.DownArrow))
            moveDir += new Vector3(0, 0, -0.5f);
        if(Input.GetKeyDown(KeyCode.RightArrow))
            moveDir += new Vector3(0.5f, 0, 0);
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            moveDir += new Vector3(-0.5f, 0, 0);

        if (moveDir == Vector3.zero)
        {
            return;
        }

        if (IsJumping() ==  false)
        {
            Jump(moveDir);
        }
    }

    private void Jump(Vector3 targetDirection)
    {
        Vector3 TargetPosition = transform.position + targetDirection;
        transform.LookAt(TargetPosition);

        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration/2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration/2));

    

    if (TargetPosition.z <= backBoundary || 
        TargetPosition.x <= leftBoundary ||
        TargetPosition.x >= rightBoundary )
    {
        return;
    }
    if (Tree.AllPosition.Contains(TargetPosition))
    {
        return;
    }
        transform.DOMoveX(TargetPosition.x, moveDuration);
        transform.DOMoveZ(TargetPosition.z, moveDuration)
        .OnComplete(UpdateTravel);
    }

    private void UpdateTravel()
    {
        currentTravel = (int) this.transform.position.z;
        if (currentTravel > maxTravel)
        {
           maxTravel = currentTravel;
        }
        stepText.text = "SCORE: " + maxTravel.ToString();
        
    }

    private bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (this.enabled == false)
        {
            return;
        }
        var car = other.GetComponent<Car>();
        if (car != null)
        {
            AnimateDie(car);
        }

        if (other.tag == "Car")
        {

        }
        
    }

    private void AnimateDie(Car car)
    {
        transform.DOScaleY(0.1f,0.2f);
        transform.DOScaleX(2,1);
        transform.DOScaleZ(2,1);

        this.enabled = false;
        dieParticles.Play();
    }
}
