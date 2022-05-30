using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Effects : MonoBehaviour {
    VolumeProfile _globalVolume;

    // Start is called before the first frame update
    void Start()
    {
        _globalVolume = GetComponent<Volume>().sharedProfile;
        ToggleGhostEffects(false);
    }

    public void ToggleGhostEffects(bool turnOn)
    {
        if (!_globalVolume.TryGet<Vignette>(out var vignette))
        {
            vignette = _globalVolume.Add<Vignette>(turnOn);
        }
        vignette.active = turnOn;
    
        if (!_globalVolume.TryGet<ColorAdjustments>(out var colorAdjustments))
        {
            colorAdjustments = _globalVolume.Add<ColorAdjustments>(turnOn);
        }
        colorAdjustments.active = turnOn;
    }

    public void ToggleDeathEffects(bool turnOn)
    {
        if (!_globalVolume.TryGet<Vignette>(out var vignette))
        {
            vignette = _globalVolume.Add<Vignette>(turnOn);
        }
        vignette.active = turnOn;
        
        if (!_globalVolume.TryGet<ColorAdjustments>(out var colorAdjustments))
        {
            colorAdjustments = _globalVolume.Add<ColorAdjustments>(turnOn);
        }
        colorAdjustments.active = turnOn;
    }
}
