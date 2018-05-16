using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    List<Character> _playerCharacters;

    [SerializeField]
    List<Character> _enemyCharacters;

    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        // Wont need this just for clarification.
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // If the singleton hasn't been initialized yet
            _instance = this;
        }
    }

    private List<Character> AliveCharacters(List<Character> characters)
    {
        return characters.Where(x => x.IsAlive).ToList();
    }

    public Character GetRandomCharacter(List<Character> characters)
    {
        var range = characters.Count;
        if (range != 0)
        {
            int randomIndex = Random.Range(0, range);
            return characters[randomIndex];
        }
        Debug.LogError("Can't fetch a random character from an empty list of characters.");
        return null;
    }

    public List<Character> PruneTargetedCharacters(List<Character> potentialTargets, Character targetingCharacter)
    {
        return potentialTargets.Where(target => !target.TargetIndicatorController.IsTargetedBy(targetingCharacter)).ToList();
    }

    #region PlayerTargetFunctions

    public List<Character> GetAliveEnemies()
    {
        return AliveCharacters(_enemyCharacters);
    }

    public List<Character> GetNonTargetedAliveEnemies(Character targetingCharacter)
    {
        return PruneTargetedCharacters(GetAliveEnemies(), targetingCharacter);
    }

    public List<Character> GetAlivePlayers()
    {
        return AliveCharacters(_playerCharacters);
    }

    public List<Character> GetNonTargetedAlivePlayers(Character targetingCharacter)
    {
        return PruneTargetedCharacters(GetAlivePlayers(), targetingCharacter);
    }

    #endregion

    #region EnemyTargetFunctions

    public Character GetWeakestPlayer()
    {
        List<Character> alivePlayers = AliveCharacters(_playerCharacters);
        List<Character> canididateCharacters = new List<Character>();
        float currentMinHealth = 1.0f;
        foreach (var player in alivePlayers)
        {
            float remainingHealthPercentage = player.HealthPortionRemaining;
            if (remainingHealthPercentage < currentMinHealth)
            {
                canididateCharacters.Clear();
                canididateCharacters.Add(player);
                currentMinHealth = remainingHealthPercentage;
            }
            else if (remainingHealthPercentage == currentMinHealth)
            {
                canididateCharacters.Add(player);
            }
        }

        return GetRandomCharacter(canididateCharacters);
    }

    public Character GetRandomAlivePlayer()
    {
        return GetRandomCharacter(AliveCharacters(_playerCharacters));
    }

    #endregion
}
