﻿using Graphics.Settings;
using UnityEngine;
using static Graphics.Inspector.Util;

namespace Graphics.Inspector
{
    internal static class SettingsInspector
    {
        private const float FOVMin = 10f;
        private const float FOVMax = 120f;
        private const float FOVDefault = 23.5f;

        internal static void Draw(CameraSettings cameraSettings, GlobalSettings renderingSettings, bool showAdvanced)
        {
            GUILayout.BeginVertical(GUIStyles.Skin.box);
            {
                Label("Camera", "", true);
                cameraSettings.ClearFlag = Selection("Clear Flags", cameraSettings.ClearFlag, flag => cameraSettings.ClearFlag = flag);
                if (showAdvanced)
                {
                    //changing studio camera's culling mask breaks studio, possibly due to cinemachine
                    GUI.enabled = false; 
                    SelectionMask("Culling Mask", cameraSettings.CullingMask, mask => cameraSettings.CullingMask = mask);                    
                    GUI.enabled = true;
                }
                Slider("Near Clipping Plane", cameraSettings.NearClipPlane, 0.01f, 15000f, "N2", ncp => { cameraSettings.NearClipPlane = ncp; });
                Slider("Far Clipping Plane", cameraSettings.FarClipPlane, 0.01f, 15000f, "N2", ncp => { cameraSettings.FarClipPlane = ncp; });
                Selection("Rendering Path", cameraSettings.RenderingPath, path => cameraSettings.RenderingPath = path);
                Slider("Field of View", cameraSettings.Fov, FOVMin, FOVMax, "N0", fov => { cameraSettings.Fov = fov; });
                cameraSettings.OcculsionCulling = Toggle("Occlusion Culling", cameraSettings.OcculsionCulling);
                cameraSettings.HDR = Toggle("Allow HDR", cameraSettings.HDR);
                cameraSettings.MSAA = Toggle("Allow MSAA (Forward Only)", cameraSettings.MSAA);
                cameraSettings.DynamicResolution = Toggle("Allow Dynamic Resolution", cameraSettings.DynamicResolution);
                GUILayout.Space(10);                
                Label("Rendering", "", true);
                GUILayout.Space(1);
                Label("Colour Space", QualitySettings.activeColorSpace.ToString());
                Label("Quality Level", QualitySettings.names[QualitySettings.GetQualityLevel()]);
                renderingSettings.PixelLightCount = Text("Pixel Light Count", renderingSettings.PixelLightCount);
                Selection("Anisotropic Textures", renderingSettings.AnisotropicFiltering, filtering => renderingSettings.AnisotropicFiltering = filtering);
                Slider("MSAA Multiplier", renderingSettings.AntiAliasing, 0, 8, aa => renderingSettings.AntiAliasing = aa);
                renderingSettings.RealtimeReflectionProbes = Toggle("Realtime Reflection Probes", renderingSettings.RealtimeReflectionProbes);
                GUILayout.Space(10);                
                Label("Shadows", "", true);
                GUILayout.Space(1);
                Selection("Shadowmask Mode", renderingSettings.ShadowmaskModeSetting, mode => renderingSettings.ShadowmaskModeSetting = mode);
                Selection("Shadows", renderingSettings.ShadowQualitySetting, setting => renderingSettings.ShadowQualitySetting = setting);
                Selection("Shadow Resolution", renderingSettings.ShadowResolutionSetting, resolution => renderingSettings.ShadowResolutionSetting = resolution);
                Selection("Shadow Projection", renderingSettings.ShadowProjectionSetting, projection => renderingSettings.ShadowProjectionSetting = projection);
                renderingSettings.ShadowDistance = Text("Shadow Distance", renderingSettings.ShadowDistance);
                renderingSettings.ShadowNearPlaneOffset = Text("Shadow Near Plane Offset", renderingSettings.ShadowNearPlaneOffset);
                GUILayout.Space(10);
                Selection("Language", LocalizationManager.CurrentLanguage, language => LocalizationManager.CurrentLanguage = language);
                Slider("Font Size", renderingSettings.FontSize, 12, 17, size => renderingSettings.FontSize = size);
                Slider("Window Width", Inspector.Width, 400, Screen.width / 2, size => Inspector.Width = size);
                Slider("Window Height", Inspector.Height, 400, Screen.height, size => Inspector.Height = size);
                GUILayout.Space(10);
                renderingSettings.ShowAdvancedSettings = Toggle("Show Advanced Settings", renderingSettings.ShowAdvancedSettings);
            }
            GUILayout.EndVertical();
        }
    }
}