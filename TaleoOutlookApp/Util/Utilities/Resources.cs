using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Util.Utilities
{
    public class Resources
    {
        public Resources GetResources()
        {
            return new Resources();
        }

        #region Property
        private static string resourcesPath = @"C:\Users\ASNazrul\Downloads\Taleo\resource";
        private static string sourcePath = @"C:\Users\ASNazrul\Downloads\Taleo\resource\src";

        public string ResourcesPath
        {
            get { return resourcesPath; }
            set { resourcesPath = value; }
        }

        public string SourcePath
        {
            get { return sourcePath; }
            set { sourcePath = value; }
        }

        public bool IsResourceExist(string fileName)
        {
            try
            {
                var resourcesFiles = Directory.GetFiles(resourcesPath, "*.txt").Select(Path.GetFileName).ToArray();
                return resourcesFiles.Any(resourcesFile => resourcesFile == fileName);
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get latest version
        /// </summary>
        /// <param name="fileName">filename</param>
        /// <returns>Latest version of file names</returns>
        public string LatestVersion(string fileName)
        {
            try
            {
                int[] latestVersion = { -1 };
                var resourcesFiles = Directory.GetFiles(resourcesPath, "*.txt").Select(Path.GetFileNameWithoutExtension).ToArray();
                foreach (var version in from resourcesFile in resourcesFiles let idx = resourcesFile.LastIndexOf('_') let resourceFileName = resourcesFile.Substring(0, idx) let version = resourcesFile.Substring(idx + 1) where resourceFileName == fileName where latestVersion[0] < Convert.ToInt16(version) select version)
                {
                    latestVersion[0] = Convert.ToInt16(version);
                }
                return latestVersion[0] < 0 ? "Requested file not found" : latestVersion[0].ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return "Requested file not found";
            }
        }

        /// <summary>
        /// Get nested list of Resource info
        /// </summary>
        /// <param name="source">2D string array of source</param>
        /// <param name="checkUpdate">needed to checkupdate</param>
        /// <param name="checkNew">is new</param>
        /// <returns>Nested list of info</returns>
        public List<List<string>> ResourceInfo(string[,] source, bool checkUpdate, bool checkNew)
        {
            var matrix = new List<List<string>>();
            try
            {
                var numberOfRow = source.GetLength(0);

                var resourcesFiles = Directory.GetFiles(resourcesPath, "*.txt").Select(Path.GetFileNameWithoutExtension).ToArray();

                if (checkNew && checkUpdate)
                {
                    for (var i = 0; i < numberOfRow; i++)
                    {
                        var newFile = true;
                        var updateFile = false;
                        int latestV = -1;

                        foreach (var version in from resourcesFile in resourcesFiles let idx = resourcesFile.LastIndexOf('_') let fileName = resourcesFile.Substring(0, idx) let version = resourcesFile.Substring(idx + 1) where source[i, 0] == fileName where Convert.ToInt16(version) > latestV select version)
                        {
                            latestV = Convert.ToInt16(version);
                        }
                        foreach (var resourcesFile in resourcesFiles)
                        {
                            var idx = resourcesFile.LastIndexOf('_');
                            var fileName = resourcesFile.Substring(0, idx);

                            if (source[i, 0] == fileName)
                            {
                                newFile = false;
                            }
                            if ((source[i, 0] == fileName) && (latestV > -1 && latestV < Convert.ToInt16(source[i, 1])))
                            {
                                updateFile = true;
                            }
                        }
                        if (updateFile || newFile)
                        {
                            var resourceInfo = new List<string>();
                            resourceInfo.Add(source[i, 0]);
                            resourceInfo.Add(source[i, 1]);
                            matrix.Add(resourceInfo);
                        }

                    }
                }
                else if (checkUpdate)
                {
                    for (var i = 0; i < numberOfRow; i++)
                    {
                        var updateFile = false;
                        int latestV = -1;

                        foreach (var resourcesFile in resourcesFiles)
                        {
                            var idx = resourcesFile.LastIndexOf('_');
                            var fileName = resourcesFile.Substring(0, idx);
                            var version = resourcesFile.Substring(idx + 1);
                            if (source[i, 0] == fileName)
                            {
                                if (Convert.ToInt16(version) > latestV)
                                {
                                    latestV = Convert.ToInt16(version);
                                }
                            }
                        }

                        foreach (var resourcesFile in resourcesFiles)
                        {
                            var idx = resourcesFile.LastIndexOf('_');
                            var fileName = resourcesFile.Substring(0, idx);
                            if ((source[i, 0] == fileName) && (latestV > -1 && latestV < Convert.ToInt16(source[i, 1])))
                            {
                                updateFile = true;
                            }
                        }
                        if (updateFile)
                        {
                            var resourceInfo = new List<string>();
                            resourceInfo.Add(source[i, 0]);
                            resourceInfo.Add(source[i, 1]);
                            matrix.Add(resourceInfo);
                        }

                    }
                }
                else if (checkNew)
                {
                    for (var i = 0; i < numberOfRow; i++)
                    {
                        var fileNew = true;
                        foreach (var resourcesFile in resourcesFiles)
                        {
                            var idx = resourcesFile.LastIndexOf('_');
                            var fileName = resourcesFile.Substring(0, idx);
                            if (source[i, 0] == fileName)
                            {
                                fileNew = false;
                            }

                        }
                        if (fileNew)
                        {
                            var resourceInfo = new List<string>();
                            resourceInfo.Add(source[i, 0]);
                            resourceInfo.Add(source[i, 1]);
                            matrix.Add(resourceInfo);
                        }
                    }
                }


                return matrix;
            }
            catch (Exception)
            {
                return matrix;
            }
        }

        /// <summary>
        /// Add a resource
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>true if successfully added</returns>
        public bool AddResource(string fileName)
        {
            try
            {
                if (!Directory.Exists(resourcesPath))
                {
                    Directory.CreateDirectory(resourcesPath);
                }
                var sourceFile = Path.Combine(sourcePath, fileName);
                var destFile = Path.Combine(resourcesPath, fileName);
                File.Copy(sourceFile, destFile, true);

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        #endregion
    }
}
