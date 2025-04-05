using System;
using System.Collections;
using System.IO;
using System.Text;
using Cadabra.Core;
using UnityEngine;
using File = UnityEngine.Windows.File;

namespace Cadabra.Scripts.Core.Demo
{
    public class DemoHandler
    {
        // Parkour-related variables and methods
        private static ArrayList _demoRuns = new ArrayList();
        private static DemoRound _currentDemoRound;
        
        public static void StartDemoRound()
        {
            if (_currentDemoRound != null) return;
            
            Debug.Log("DemoHandler: Starting Demo run");
            
            _currentDemoRound = new DemoRound();
        }
        public static void EndDemoRound()
        {
            if (_currentDemoRound == null) return;
            
            Debug.Log("DemoHandler: Ending Demo run");
            
            _currentDemoRound.AddTime(Time.time);
            
            _demoRuns.Add(_currentDemoRound);
            WriteAllMetricsToFile();

            _currentDemoRound = null;
        }

        public static void FailDemo()
        {
            _currentDemoRound = null;
        }
        
        public static DemoRound GetCurrentDemoRound()
        {
            return _currentDemoRound;
        }
        
        public static void ReturnToLastCheckpoint(PlayerBody player)
        {
            if (GameManager.instance.currentCheckpoint == null) return;

            DamageInfo damageInfo = new DamageInfo();
            damageInfo.damage = 50f;
            
            player._healthController.RequestDamage(damageInfo);
            GameManager.instance.currentCheckpoint.TeleportToCheckpoint(player);
        }
        
        public static void SetCurrentCheckpoint(CheckPoint checkpoint)
        {
            if (GameManager.instance.currentCheckpoint == checkpoint && checkpoint != null)
            {
                return;
            }
            
            Debug.Log("DemoHandler: Setting checkpoint: " + checkpoint.checkpointID);
            _currentDemoRound.AddTime(Time.time);
            GameManager.instance.currentCheckpoint = checkpoint;
        }
        
        
        // Movement-related variables and methods
        private static int _numWallClimbs;
        
        public static void WriteAllMetricsToFile()
        {
            int demoRunNum = 1;
            // Build the metrics information
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----- Cadabra Metrics -----");
            sb.AppendLine("---------------");
            sb.AppendLine("Demo Run " + demoRunNum++ + ":");
            sb.AppendLine("---------------");
            
            // List all recorded parkour times
            if (_demoRuns.Count > 0)
            {
                int runNumber = 1;
                foreach (DemoRound round in _demoRuns)
                {
                    sb.AppendLine("Run " + runNumber++);
                    int checkpointNumber = 0;
                    foreach (var time in round.GetTimes())
                    {
                        sb.AppendLine($"Checkpoint {checkpointNumber++}: {time} seconds");
                    }
                    float timeTaken = (float)round.GetTimes()[round.GetTimes().Count - 1] - (float)round.GetTimes()[0];
                    sb.AppendLine($"Total Time: {timeTaken} seconds");
                    
                    sb.AppendLine($"Number of Jumps: {round.GetNumJumps()}");
                    sb.AppendLine($"Number of Primary Shots Fired: {round.GetPrimaryShotsFired()}");
                    sb.AppendLine($"Number of Secondary Shots Fired: {round.GetSecondaryShotsFired()}");
                    sb.AppendLine($"Damage Taken: {round.GetDamageTaken()}");
                    sb.AppendLine($"Health Gained: {round.GetHealthGained()}");
                    sb.AppendLine($"Mana Lost: {round.GetManaLost()}");
                }
            }
            else
            {
                sb.AppendLine("No demo runs recorded.");
            }

            sb.AppendLine().AppendLine();
            
            // Get the path to the user's desktop
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string filePath = Path.Combine(desktopPath, "CadabraMetrics.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(sb.ToString());
                }
                Debug.Log("Metrics written to: " + filePath);
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to write metrics to file: " + ex.Message);
            }
        }
        
    }
}