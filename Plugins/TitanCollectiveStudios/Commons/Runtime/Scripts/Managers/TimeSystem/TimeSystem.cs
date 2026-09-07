using System;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Managers
{
    public class TimeSystem : GameSystem<TimeSystem>
    {
        [Header("Time Settings")]
        [Range(0f, 24f)]
        public float currentTimeOfDay = 12f; // 0 - 24 hours

        [Tooltip("How many in-game minutes pass per real second")]
        public float timeMultiplier = 10f;

        [Header("Day/Night Settings")]
        public Light directionalLight;

        public AnimationCurve lightIntensityCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public Gradient lightColorGradient;

        public event Action<float> OnTimeChanged;

        private void Update()
        {
            UpdateTime();
            UpdateLighting();

            OnTimeChanged?.Invoke(currentTimeOfDay);
        }

        private void UpdateTime()
        {
            currentTimeOfDay += (Time.deltaTime * timeMultiplier) / 60f;

            if (currentTimeOfDay >= 24f)
                currentTimeOfDay -= 24f;
        }

        private void UpdateLighting()
        {
            if (directionalLight == null) return;

            float timeNormalized = currentTimeOfDay / 24f;

            // Rotate sun (360° per day cycle)
            float sunAngle = timeNormalized * 360f - 90f;
            directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

            // Light intensity + color
            directionalLight.intensity = lightIntensityCurve.Evaluate(timeNormalized);
            directionalLight.color = lightColorGradient.Evaluate(timeNormalized);
        }

        public string GetFormattedTime()
        {
            int hours = Mathf.FloorToInt(currentTimeOfDay);
            int minutes = Mathf.FloorToInt((currentTimeOfDay - hours) * 60f);

            return $"{hours:00}:{minutes:00}";
        }
    }
}