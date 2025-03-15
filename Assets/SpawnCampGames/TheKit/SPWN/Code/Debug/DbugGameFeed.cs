    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using System.Collections;

    namespace SPWN
    {
        public class DbugGameFeed : Singleton<DbugGameFeed>
        {
            [SerializeField] TMP_Text DbugFeed;
            [SerializeField] int maxEntries = 10;
            List<string> logEntries = new List<string>();

            [SerializeField] Image activityImage;
            [SerializeField] float pulseSpeed = 1f;
            bool isActivityActive = false;

            [SerializeField] Image progressBar;

            private float progress = 0f;
            private float fillDuration = 5f; // Time it takes to fill the progress bar (in seconds)
            private float fillSpeed = 0.1f; // Speed of progress update per frame

            public void UpdateDisplay(string _dbugString)
            {
                if(DbugFeed == null) return;

                string timestamp = System.DateTime.Now.ToString("HH:mm:ss");
                string preprint = $"[{timestamp}] {_dbugString}";

                if(logEntries.Count >= maxEntries) logEntries.RemoveAt(0);
                logEntries.Add(preprint);

                RefreshDisplay();
            }

            public void LoadThisLong(float duration = 5f)
            {
                fillDuration = duration; // Set the duration for filling the progress bar
                fillSpeed = 1f / fillDuration; // Calculate the speed based on the duration

                progress = 0f;
                progressBar.fillAmount = progress;

                StartCoroutine(UpdateProgressBarCoroutine());
            }

            private IEnumerator UpdateProgressBarCoroutine()
            {
                float timeElapsed = 0f;
                while(timeElapsed < fillDuration)
                {
                    progress = Mathf.Lerp(0f,1f,timeElapsed / fillDuration); // Smoothly fill the progress bar
                    progressBar.fillAmount = progress;
                    timeElapsed += Time.deltaTime; // Update time elapsed based on frame time
                    yield return null;
                }

                progress = 1f; // Ensure the progress bar is fully filled at the end
                progressBar.fillAmount = progress;
                SetActivityStatus(false); // End the activity once loading is complete
            }

            void RefreshDisplay()
            {
                DbugFeed.text = string.Join("\n",logEntries);
            }

            void Update()
            {
                if(isActivityActive) PulseActivityIndicator();
            }

            public void ActivityBlip(float duration)
            {
                StartCoroutine(ActivityBlipCoroutine(duration));
            }

            private IEnumerator ActivityBlipCoroutine(float duration)
            {
                SetActivityStatus(true);
                yield return new WaitForSeconds(duration);
                SetActivityStatus(false);
            }

            public void SetActivityStatus(bool isActive)
            {
                isActivityActive = isActive;
                if(!isActive) ResetProgressBar();
            }

            void PulseActivityIndicator()
            {
                float alpha = Mathf.PingPong(Time.time * pulseSpeed,1f);
                Color color = activityImage.color;
                color.a = alpha;
                activityImage.color = color;
            }

            void ResetProgressBar()
            {
                progressBar.fillAmount = 0f;
            }
        }
    }