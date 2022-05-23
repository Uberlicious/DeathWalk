using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Effects : MonoBehaviour {
    Volume _globalVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        _globalVolume = GetComponent<Volume>();
        ToggleGhostEffects(false);
    }

    public void ToggleGhostEffects(bool turnOn)
    {
        VolumeProfile profile = _globalVolume.sharedProfile;
        if (!profile.TryGet<Vignette>(out var vignette))
        {
            vignette = profile.Add<Vignette>(turnOn);
        }
        vignette.active = turnOn;
    
        if (!profile.TryGet<ColorAdjustments>(out var colorAdjustments))
        {
            colorAdjustments = profile.Add<ColorAdjustments>(turnOn);
        }
        colorAdjustments.active = turnOn;
    }
}
