using UnityEngine;
using UnityEngine.UI;
using Game2DWaterKit;
using Game2DWaterKit.Animation;
using System.Collections;

public class FillGlass : MonoBehaviour {

    public Game2DWater waterObject;
    public Game2DWaterfall waterfallObject;
    public Text instructionText;
    public Text percentageText;

    public float targetWaterHeight = 1f;
    public float targetWaterfallHeight = 1f;
    public float fillingDuration = 3f;
    public float waterfallStartStopDuration = 0.3f;
    public float deltaHeightToStopWaterfall = 0.3f;

    private bool _isFilling;
    private bool _isFilled;

    private void Update()
    {
        if(!_isFilled && !_isFilling && Input.GetKeyDown(KeyCode.F))
        {
            StartWaterfall(); // "Expand" the waterfall
            StartFillingGlass();

            instructionText.enabled = false;
        }

        if (_isFilling)
        {
            _isFilled = Mathf.Abs(waterObject.MainModule.Height - targetWaterHeight) < deltaHeightToStopWaterfall;

            if (_isFilled)
            {
                _isFilling = false;
                StopWaterfall();
            }
        }

        percentageText.text = string.Format("{0} %", Mathf.RoundToInt(waterObject.MainModule.Height / targetWaterHeight * 100f));
    }

    private void StartFillingGlass()
    {
        var targetSize = new Vector2(waterObject.MainModule.Width, targetWaterHeight);
        var duration = fillingDuration;
        var constraint = WaterAnimationConstraint.Bottom; // constraints the position of the bottom edges, so only the top edge "moves"
        var wrapMode = WaterAnimationWrapMode.Once; // play the animation once

        waterObject.AnimationModule.AnimateWaterSize(targetSize, duration, constraint, wrapMode);

        _isFilling = true;
    }

    // "Expand" Waterfall
    private void StartWaterfall()
    {
        AnimateWaterfall(new Vector2(waterfallObject.MainModule.Width, targetWaterfallHeight), WaterAnimationConstraint.Top); // constrain the top edge => bottom edge moves
    }

    // "Collapse" Waterfall
    private void StopWaterfall()
    {
        AnimateWaterfall(new Vector2(waterfallObject.MainModule.Width, 0.001f), WaterAnimationConstraint.Bottom); // constrain the bottom edge => top edge moves
        StartCoroutine(DisableWaterfallObject());
    }

    private void AnimateWaterfall(Vector2 targetSize, WaterAnimationConstraint constraint)
    {
        var duration = waterfallStartStopDuration;
        var wrapMode = WaterAnimationWrapMode.Once; // play the animation once

        waterfallObject.AnimationModule.AnimateWaterfallSize(targetSize, duration, constraint, wrapMode);
    }

    private IEnumerator DisableWaterfallObject()
    {
        yield return new WaitForSeconds(waterfallStartStopDuration);

        waterfallObject.gameObject.SetActive(false);
    }
}
