using UnityEngine;
using System.Collections;

/// <summary>
/// Automatically scales quality up or down based on the current framerate (average).
/// </summary>
///
/// \author Kaspar Manz
/// \date 2014-03-10
/// \version 1.0.0
public class QualityAdaption : MonoBehaviour
{
/// <summary>
/// The number of data points to calculate the average FPS over.
/// </summary>
int numberOfDataPoints;
/// <summary>
/// The current average fps.
/// </summary>
float currentAverageFps;
/// <summary>
/// The time interval in which the class checks for the framerate and adapts quality accordingly.
/// </summary>
public float TimeIntervalToAdaptQualitySettings = 10f;
/// <summary>
/// The lower FPS threshold. Decrease quality when FPS falls below this.
/// </summary>
public float LowerFPSThreshold = 40f;
/// <summary>
/// The upper FPS threshold. Increase quality when FPS is above this.
/// </summary>
public float UpperFPSThreshold = 55f;
/// <summary>
/// Apply expensive changes -needed for phone games.
/// </summary>
public bool applyExpensiveChanges = false;
/// <summary>
/// The stability of the current quality setting. Below 0 if changes have been
/// made, otherwise positive.
/// </summary>
public int maxStability = 15;
int stability;
/// <summary>
/// Tracks whether quality was improved or worsened.
/// </summary>
bool lastMovementWasDown;
/// <summary>
/// Counter that keeps track when the script can't decide between lowering or increasing quality.
/// </summary>
int flickering;


void Start ()
{
	StartCoroutine (AdaptQuality ());
}


void Update ()
{
	UpdateCumulativeAverageFPS (1 / Time.deltaTime);
}


/// <summary>
/// Updates the cumulative average FPS.
/// </summary>
/// <param name="newFPS">New FPS.</param>
float UpdateCumulativeAverageFPS (float newFPS)
{
	++numberOfDataPoints;
	currentAverageFps += (newFPS - currentAverageFps) / numberOfDataPoints;

	return currentAverageFps;
}


/// <summary>
/// Sets the quality accordingly to the current thresholds.
/// </summary>
IEnumerator AdaptQuality ()
{
	while (true) {
		yield return new WaitForSeconds (TimeIntervalToAdaptQualitySettings);

		if (Debug.isDebugBuild) {
			//Debug.Log ("Current Average Framerate is: " + currentAverageFps);
		}

		// Decrease level if framerate too low and not already lowest
		if (currentAverageFps < LowerFPSThreshold && (QualitySettings.GetQualityLevel()!=0)) {
			if (applyExpensiveChanges)
			{
				QualitySettings.DecreaseLevel (true);
				QualitySettings.DecreaseLevel (false);
			}
			else QualitySettings.DecreaseLevel (false);
			--stability;
			if (!lastMovementWasDown) {
				++flickering;
			}
			lastMovementWasDown = true;
			if (Debug.isDebugBuild) {
				//Debug.Log ("Reducing Quality Level, now " + QualitySettings.names [QualitySettings.GetQualityLevel ()]);
			}

			// In case we are "flickering" (switching between two quality settings),
			// stop it, using the lower quality level.
			if (flickering > 1) {
				if (Debug.isDebugBuild) {
					//	Debug.Log (string.Format (
					//			   "Flickering detected, staying at {0} to stabilise.",
					//		   QualitySettings.names [QualitySettings.GetQualityLevel ()]));
				}
				Destroy (this);
			}

		} else
		// Increase level if framerate is too high and not already highest
		if (currentAverageFps > UpperFPSThreshold && (QualitySettings.GetQualityLevel()!=5)) {
			if (applyExpensiveChanges)
			{
				QualitySettings.IncreaseLevel (true);
				QualitySettings.IncreaseLevel (false);
			}
			else QualitySettings.IncreaseLevel (false);
			--stability;
			if (lastMovementWasDown) {
				++flickering;
			}
			lastMovementWasDown = false;
			if (Debug.isDebugBuild) {
				//	Debug.Log ("Increasing Quality Level, now " + QualitySettings.names [QualitySettings.GetQualityLevel ()]);
			}
		} else {
			//	Debug.Log ("Stable at: " + QualitySettings.names [QualitySettings.GetQualityLevel ()] + ", been stable for: " + stability + " times.");
			++stability;
		}

		// If we had a framerate in the range between 25 and 60 frames three times
		// in a row, we consider this pretty stable and remove this script.
		if (maxStability > 0)
		{
			if (stability > maxStability) {
				if (Debug.isDebugBuild) {
					//		Debug.Log ("Framerate is stable now, removing automatic adaptation.");
				}
				Destroy (this);
			}
		}

		// Reset moving average
		numberOfDataPoints = 0;
		currentAverageFps = 0;
	}
}


/*


   EXPERIMENTAL TEXTURE FILL QUALITY SETTING

   eQualityLevel AutoChooseQualityLevel()
   {
        Assert.IsTrue(QualitySettings.names.Length == Enum.GetNames(typeof(eQualityLevel)).Length, "Please update eQualityLevel to the new quality levels.");

        var shaderLevel = SystemInfo.graphicsShaderLevel;
        var vram = SystemInfo.graphicsMemorySize;
        var cpus = SystemInfo.processorCount;
        var fillrate = 0;
        if (shaderLevel < 10)
                fillrate = 1000;
        else if (shaderLevel < 20)
                fillrate = 1300;
        else if (shaderLevel < 30)
                fillrate = 2000;
        else
                fillrate = 3000;
        if (cpus >= 6)
                fillrate *= 3;
        else if (cpus >= 3)
                fillrate *= 2;
        if (vram >= 512)
                fillrate *= 2;
        else if (vram <= 128)
                fillrate /= 2;

        var resx = Screen.width;
        var resy = Screen.height;
        var target_fps = 30.0f;
        var fillneed = (resx*resy + 400f*300f) * (target_fps / 1000000.0f);
        // Change the values in levelmult to match the relative fill rate
        // requirements for each quality level.
        var levelmult = new float[] { 5.0f, 30.0f, 80.0f, 130.0f, 200.0f, 320.0f };

        const int max_quality = (int)eQualityLevel.Fantastic;
        var level = 0;
        while (level < max_quality && fillrate > fillneed * levelmult[level+1])
 ++level;

        var quality = (eQualityLevel)level;
        Debug.Log(string.Format("{0}x{1} need {2} has {3} = {4} level", resx, resy, fillneed, fillrate, quality.ToString()));

        return quality;
   }


 */

}
