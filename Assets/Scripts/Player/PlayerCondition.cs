using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamagalbe
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagalbe
{
    public UICodition uiCondition;
    private PlayerController controller;

    Condition health { get { return uiCondition.health; } }
    Condition hunger{ get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;

    public event Action onTakeDamage;

    public float speedMultiplier = 3f;
    public float jumpStamina = 20f;
    public float minJumpStamina = 10f;

    public bool canJump => stamina.curValue >= minJumpStamina;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue == 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    private void Die()
    {
        Debug.Log("Death");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public void SpeedBoost(float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(duration));
    }

    private IEnumerator SpeedBoostCoroutine(float duration)
    {
        float originalSpeed = controller.moveSpeed;
        controller.moveSpeed *= speedMultiplier;

        yield return new WaitForSeconds(duration);

        controller.moveSpeed = originalSpeed;
    }

    public void JumpStamina()
    {
        stamina.Subtract(jumpStamina);
    }
}
