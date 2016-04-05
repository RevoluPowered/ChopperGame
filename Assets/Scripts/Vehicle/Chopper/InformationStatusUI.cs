using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Information Status UI
/// Used to count 'score'.
/// </summary>
[RequireComponent(typeof(InformationStatusUI))]
public class InformationStatusUI : MonoBehaviour {

    private static InformationStatusUI mInformationStatusInstance;
    /// <summary>
    /// Register handler which allows you to register the enemies callback so when they die we re-count the alive enemies.
    /// This is more efficient than doing a lookup each tick.
    /// </summary>
    /// <param name="EnemyCallbackHandler"></param>
    public static void RegisterDamageBase( DamageBase EnemyCallbackHandler )
    {
        // Register this callback statically this saves allowing anything else to leak into the global stack.
        EnemyCallbackHandler.DeathEvent += mInformationStatusInstance.DamageBase_Death;
        mInformationStatusInstance.CountEnemies(); // Count the enemies availble for murdering.
    }
    /// <summary>
    /// UI Text
    /// </summary>
    private Text mText;

    /// <summary>
    /// Awake - let's assign our local object which is used to register global death events of the objects
    /// </summary>
    void Awake()
    {
        mInformationStatusInstance = this;
    }

	// Use this for initialization
	void Start () {
        mText = GetComponent<Text>();
	}

    /// <summary>
    /// The value which is displayed on the screen showing the current enemy count
    /// </summary>
    public int mEnemyCount = 0;

    /// <summary>
    /// Callback to force the ui to upload.
    /// </summary>
    private void UpdateUI()
    {
        // Supply our template
        string ui = "<color=red>Targets left:</color> " + mEnemyCount +"\n" + 
        "<color=red>Objective:</color> Destroy all AA sites in the area before the timer elapses!\n";

        // Update the ui
        mText.text = ui;
    }

    /// <summary>
    /// Count the enemies in the game
    /// </summary>
    private void CountEnemies()
    {
        // Count the enemies on the map
        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");

        // Store the new enemy count
        mEnemyCount = targets.Length;

        // Display enemy count in console for debugging purposes.
        Console.Log("Enemy count: " + mEnemyCount);

        UpdateUI();
    }

    /// <summary>
    /// Triggers a count of the enemies left.
    /// </summary>
    /// <param name="damageBase"></param>
    /// <param name="damageAmount"></param>
    /// <param name="overDamage"></param>
    /// <param name="source"></param>
    private void DamageBase_Death(DamageBase damageBase, float damageAmount, float overDamage, GameObject source)
    {
        // Force re-count;
        CountEnemies();
    }




}
