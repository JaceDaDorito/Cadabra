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
        private static ArrayList _parkourTimes = new ArrayList();
        private static ParkourRound _currentParkourRound;
        
        public static void StartParkourRun()
        {
            if (_currentParkourRound != null) return;
            
            Debug.Log("Starting parkour run");
            
            _currentParkourRound = new ParkourRound();
            _currentParkourRound.StartTimer();
        }
        public static ParkourRound EndParkourRun()
        {
            if (_currentParkourRound == null) return null;
            
            Debug.Log("Ending parkour run");
            
            _currentParkourRound.EndTimer();
            _parkourTimes.Add(_currentParkourRound.GetTime());
            
            var temp = _currentParkourRound;
            _currentParkourRound = null;
            
            return temp;
        }
        public static void PrintParkourRunTimes()
        {
            // Sort times in ascending order
            _parkourTimes.Sort();
            // Print all times
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("All times:");
            foreach (var time in _parkourTimes)
            {
                sb.AppendLine(time+"s");
            }
            Debug.Log(sb.ToString());
        }
        
        // Checkpoint-related variables and methods
        private static CheckPoint _currentCheckpoint;
        
        public static void ReturnToLastCheckpoint(PlayerBody player)
        {
            if (_currentCheckpoint == null) return;
            
            _currentCheckpoint.TeleportToCheckpoint(player);
        }
        
        public static void SetCurrentCheckpoint(CheckPoint checkpoint)
        {
            _currentCheckpoint = checkpoint;
        }
        
        
        // Movement-related variables and methods
        private static int _numJumps;
        private static int _numWallClimbs;
        
        public static void DemoIncrementJumps()
        {
            _numJumps++;
        }

        // Demo-related methods
        public static void EndDemo()
        {
            WriteAllMetricsToFile();
        }
        
        public static void WriteAllMetricsToFile()
        {
            // Build the metrics information
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----- Cadabra Metrics -----");
            sb.AppendLine();
            sb.AppendLine("Parkour Metrics:");
            sb.AppendLine("---------------");
            sb.AppendLine("Parkour Run Times:");
            
            // List all recorded parkour times
            if (_parkourTimes.Count > 0)
            {
                foreach (var time in _parkourTimes)
                {
                    sb.AppendLine($"{time} s");
                }
            }
            else
            {
                sb.AppendLine("No parkour times recorded.");
            }
            
            sb.AppendLine();
            
            sb.AppendLine("Movement Metrics:");
            sb.AppendLine("---------------");
            sb.AppendLine($"Number of Jumps: {_numJumps}");
            sb.AppendLine($"Number of Wall Climbs: {_numWallClimbs}");
            
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