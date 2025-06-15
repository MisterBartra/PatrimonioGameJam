using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    public audioManager am;

    public GameObject arrowUp, arrowDown, arrowLeft, arrowRight;
    public GameObject playerIndicator;
    public GameObject enemyIndicator;
    private List<string> sequence = new List<string>();
    private List<string> playerInput = new List<string>();

    private bool inputEnabled = false;

    private Dictionary<string, GameObject> arrowMap;


    //time
    public Slider timeBar;
    public float timeLimit = 5f; // Segundos para responder
    private float currentTime;
    private bool timing = false;

    //enemy bar
    public Slider enemyHealthBar;
    public int maxEnemyHealth = 8;
    private int enemyHealth;

    //player bar
    public Slider playerHealthBar;
    public int maxPlayerHealth = 5;
    private int playerHealth;

    void SetTurnIndicators(bool isPlayerTurn)
    {
        playerIndicator.SetActive(isPlayerTurn);
        enemyIndicator.SetActive(!isPlayerTurn);
    }
    void Start()
    {
        enemyHealth = maxEnemyHealth;
        enemyHealthBar.maxValue = maxEnemyHealth;
        enemyHealthBar.value = enemyHealth;

        playerHealth = maxPlayerHealth;
        playerHealthBar.maxValue = maxPlayerHealth;
        playerHealthBar.value = playerHealth;

        timeBar.maxValue = timeLimit;
        arrowMap = new Dictionary<string, GameObject> {
        { "Up", arrowUp },
        { "Down", arrowDown },
        { "Left", arrowLeft },
        { "Right", arrowRight }
    };

        SetTurnIndicators(false); // Empieza el turno del enemigo

        // 👇 Generar 5 flechas antes del inicio
        for (int i = 0; i < 3; i++)
        {
            sequence.Add(GetRandomDirection());
        }

        StartCoroutine(ShowSequenceThenEnableInput());
    }

    IEnumerator NextRound()
    {
        inputEnabled = false;
        playerInput.Clear();

        // Añadir una flecha más por ronda
        sequence.Add(GetRandomDirection());

        yield return ShowSequenceThenEnableInput();
    }

    string GetRandomDirection()
    {
        string[] directions = { "Up", "Down", "Left", "Right" };
        return directions[Random.Range(0, directions.Length)];
    }

    IEnumerator ShowSequence()
    {
        foreach (string dir in sequence)
        {
            GameObject arrow = arrowMap[dir];
            SpriteRenderer sr = arrow.GetComponent<SpriteRenderer>();
            Color originalColor = sr.color;
            am.playCualquierSound(am.efectoFlecha);
            sr.color = Color.yellow; // highlight
            yield return new WaitForSeconds(0.5f);
            sr.color = originalColor;
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator ShowSequenceThenEnableInput()
    {
        SetTurnIndicators(false); // Turno del enemigo (mostrar secuencia)

        yield return ShowSequence();
        SetTurnIndicators(true); // Ahora es el turno del jugador

        inputEnabled = true;
        currentTime = timeLimit;
        timeBar.value = 1;
        timing = true;
    }

    void Update()
    {
        if (!inputEnabled) return;

        if (Input.GetKeyDown(KeyCode.UpArrow)) RegisterInput("Up");
        if (Input.GetKeyDown(KeyCode.DownArrow)) RegisterInput("Down");
        if (Input.GetKeyDown(KeyCode.LeftArrow)) RegisterInput("Left");
        if (Input.GetKeyDown(KeyCode.RightArrow)) RegisterInput("Right");

        if (timing && inputEnabled)
        {
            currentTime -= Time.deltaTime;
            timeBar.value = currentTime;
            if (am.audioS.isPlaying == false)
            {
                am.playCualquierSound(am.reloj);
            }
            if (currentTime <= 0)
            {
                am.audioS.Stop();
                Debug.Log("¡Tiempo agotado!");
                timing = false;
                inputEnabled = false;
                playerHealth--;
                playerHealthBar.value = playerHealth;


                if (playerHealth <= 0)
                {
                    Debug.Log("Has perdido.");
                }
                else
                {
                    StartCoroutine(RestartRound());
                }
            }
        }
    }

    public void RegisterInput(string input)
    {
        if (!inputEnabled) return;

        playerInput.Add(input);
        am.playCualquierSound(am.efectoFlecha);

        // Verificamos si se equivocó
        int currentIndex = playerInput.Count - 1;
        if (playerInput[currentIndex] != sequence[currentIndex])
        {
            Debug.Log("¡Secuencia incorrecta!");
            timing = false;
            inputEnabled = false;
            playerHealth--;
            playerHealthBar.value = playerHealth;
            am.playCualquierSound(am.efectoDañoHuman);

            if (playerHealth <= 0)
            {
                StartCoroutine(EndGame(false)); // pierde
            }
            else
            {
                StartCoroutine(DelayedRestartRound());
            }
            return;
        }

        // Si completó toda la secuencia correctamente
        if (playerInput.Count == sequence.Count)
        {
            Debug.Log("¡Secuencia correcta!");
            timing = false;
            inputEnabled = false;
            enemyHealth--;
            enemyHealthBar.value = enemyHealth;
            am.playCualquierSound(am.efectoDañoEnte);



            if (enemyHealth <= 0)
            {
                StartCoroutine(EndGame(true)); // gana
            }
            else
            {
                StartCoroutine(DelayedNextRound());
            }
        }
    }

    IEnumerator DelayedRestartRound()
    {
        
        yield return new WaitForSeconds(3f);
        am.audioS.Stop();
        StartCoroutine(RestartRound());
    }

    IEnumerator DelayedNextRound()
    {
        
        yield return new WaitForSeconds(3f);
        am.audioS.Stop();
        StartCoroutine(NextRound());
    }

    IEnumerator EndGame(bool playerWon)
    {
        yield return new WaitForSeconds(3f);

        if (playerWon)
        {
            Debug.Log("¡Ganaste!");
            // aquí podrías mostrar una pantalla de victoria
        }
        else
        {
            Debug.Log("¡Perdiste!");
            // aquí podrías mostrar una pantalla de derrota
        }
    }

    /* void RegisterInput(string dir)
     {
         playerInput.Add(dir);

         int i = playerInput.Count - 1;

         if (playerInput[i] != sequence[i])
         {
             playerHealth--;
             inputEnabled = false;
             Debug.Log("¡Te equivocaste! "+playerHealth);


             if (playerHealth <= 0)
                 Debug.Log("Has perdido.");
             else
                 StartCoroutine(RestartRound());

             return;
         }

         if (playerInput.Count == sequence.Count)
         {
             enemyHealth--;
             inputEnabled = false;
             Debug.Log("¡Secuencia correcta! "+ enemyHealth);


             if (enemyHealth <= 0)
                 Debug.Log("¡Has ganado!");
             else
                 StartCoroutine(NextRound());
         }
         timing = false;

     }*/

    IEnumerator RestartRound()
    {
        yield return new WaitForSeconds(1f);
        am.audioS.Stop();
        playerInput.Clear();
        yield return ShowSequenceThenEnableInput();
        inputEnabled = true;
    }
}
