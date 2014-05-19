﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Common.Logging;
using WotDossier.Common;

namespace WotDossier.Applications
{
    public static class CacheHelper
    {
        private const char SEPARATOR = ';';
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The game servers
        /// </summary>
        private static readonly Dictionary<string, string> GameServers = new Dictionary<string, string>
        {
            {"ru", "worldoftanks.net"},
            {"eu", "worldoftanks.eu"},
            {"cn", "worldoftanks.cn"},
            {"us", "worldoftanks.com"},
        }; 

        /// <summary>
        /// Gets the cache file.
        /// </summary>
        /// <param name="playerId">The player id.</param>
        /// <param name="server"></param>
        /// <returns>null if there is no any dossier cache file for specified player</returns>
        public static FileInfo GetCacheFile(string playerId, string server)
        {
            Log.Trace("GetCacheFile start");

            FileInfo cacheFile = null;

            string[] files = new string[0];

            try
            {
                files = Directory.GetFiles(Folder.GetDossierCacheFolder(), "*.dat");
            }
            catch (DirectoryNotFoundException ex)
            {
                Log.Error("Cann't find dossier cache files", ex);
            }

            if (!files.Any())
            {
                return null;
            }

            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);

                string decodFileName = DecodFileName(info);
                string playerName = decodFileName.Split(SEPARATOR)[1];
                string serverName = decodFileName.Split(SEPARATOR)[0];

                if (playerName.Equals(playerId, StringComparison.InvariantCultureIgnoreCase) && serverName.Contains(GameServers[server]))
                {
                    if (cacheFile == null)
                    {
                        cacheFile = info;
                    }
                    else if (cacheFile.LastWriteTime < info.LastWriteTime)
                    {
                        cacheFile = info;
                    }
                }
            }
            if (cacheFile != null)
            {
                cacheFile = cacheFile.CopyTo(Path.Combine(Path.GetTempPath(), cacheFile.Name), true);
            }
            Log.Trace("GetCacheFile end");
            return cacheFile;
        }

        /// <summary>
        /// Binary dossier cache to plain json.
        /// -f - By setting f the JSON will be formatted for better human readability
        /// -r - By setting r the JSON will contain all fields with their values and recognized names
        /// -k - By setting k the JSON will not contain Kills/Frags
        /// -s - By setting s the JSON will not include unix timestamp of creation as it is useless for calculation of 
        /// </summary>
        /// <param name="cacheFile">The cache file.</param>
        public static string BinaryCacheToJson(FileInfo cacheFile)
        {
            Log.Trace("BinaryCacheToJson start");

            string directoryName = Environment.CurrentDirectory;

            string task = directoryName + @"\External\wotdc2j.exe";
            string arguments = string.Format("\"{0}\" -f", cacheFile.FullName);
            var logPath = directoryName + @"\Logs\wotdc2j.log";
            var workingDirectory = directoryName + @"\External";

            ExecuteTask(task, arguments, logPath, workingDirectory);

            Log.Trace("BinaryCacheToJson end");
            return cacheFile.FullName.Replace(".dat", ".json");
        }

        /// <summary>
        /// Binary dossier cache to plain json.
        /// </summary>
        /// <param name="cacheFile">The cache file.</param>
        public static void ReplayToJson(FileInfo cacheFile)
        {
            string directoryName = Environment.CurrentDirectory;

            string task = directoryName + @"\External\wotrp2j.exe";
            string arguments = string.Format("\"{0}\" ", cacheFile.FullName);
            var logPath = directoryName + @"\Logs\wotrp2j.log";
            var workingDirectory = directoryName + @"\External";

            ExecuteTask(task, arguments, logPath, workingDirectory);
        }

        private static void ExecuteTask(string task, string arguments, string logPath, string workingDirectory = null)
        {
            using(Process proc = new Process())
            {
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.FileName = task;
                proc.StartInfo.Arguments = arguments;

                if (!string.IsNullOrEmpty(workingDirectory))
                {
                    proc.StartInfo.WorkingDirectory = workingDirectory;
                }

                proc.Start();

                //write log
                using (StreamWriter streamWriter = new StreamWriter(logPath, false))
                {
                    streamWriter.WriteLine(proc.StandardOutput.ReadToEnd());   
                }

                proc.WaitForExit();
            }
        }

        /// <summary>
        /// Gets the name of the player from name of dossier cache file.
        /// </summary>
        /// <param name="cacheFile">The cache file in base32 format. Example of decoded filename - login-ct-p1.worldoftanks.com:20015;_Rembel__RU</param>
        /// <returns></returns>
        public static string GetPlayerName(FileInfo cacheFile)
        {
            var decodedFileName = DecodFileName(cacheFile);
            return decodedFileName.Split(SEPARATOR)[1];
        }

        /// <summary>
        /// Decods the name of the file.
        /// </summary>
        /// <param name="cacheFile">The cache file.</param>
        /// <returns></returns>
        public static string DecodFileName(FileInfo cacheFile)
        {
            Base32Encoder encoder = new Base32Encoder();
            string str = cacheFile.Name.Replace(cacheFile.Extension, string.Empty);
            byte[] decodedFileNameBytes = encoder.Decode(str.ToLowerInvariant());
            string decodedFileName = Encoding.UTF8.GetString(decodedFileNameBytes);
            return decodedFileName;
        }
    }
}
