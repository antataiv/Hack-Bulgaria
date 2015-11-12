namespace _03_Depedencies_Resolving
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Program
    {
        static string installedModulesPath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "installed_modules" + Path.DirectorySeparatorChar;

        static void Main()
        {
            var allPackagesFile = File.ReadAllText("../../all_packages.json");
            var projectFile = File.ReadAllText("../../dependencies.json");

            List<string> allProjectDependencies = new List<string>();

            Dictionary<string, List<string>> allDependencies = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(allPackagesFile);

            var prj = (JObject)JsonConvert.DeserializeObject(projectFile);
            var prjDeps = prj["dependencies"].ToArray();

            foreach (var dep in prjDeps)
            {
                allProjectDependencies.Add(dep.Value<string>());
            }

            CheckForDependency(allProjectDependencies, allDependencies);
            Console.WriteLine("All done.");
        }

        private static void CheckForDependency(List<string> allProjectDependencies, Dictionary<string, List<string>> allDependencies)
        {
            foreach (var projectDependency in allProjectDependencies)
            {
                InstallDependency(projectDependency, installedModulesPath);

                if (allDependencies.ContainsKey(projectDependency))
                {
                    var dependentPackages = allDependencies[projectDependency];

                    if (dependentPackages.Count != 0)
                    {
                        Console.WriteLine("In order to install {0}, we need {1}.", projectDependency, string.Join(" and ", dependentPackages));
                    }

                    CheckForDependency(dependentPackages, allDependencies);
                }
            }
        }

        private static void InstallDependency(string projectDependency, string path)
        {
            if (!CheckIfFileExists(projectDependency, path))
            {
                Console.WriteLine("Installing {0}.", projectDependency);

                using (StreamWriter sw = new StreamWriter(path + projectDependency))
                {
                    sw.WriteLine("This is the {0} dependency file", projectDependency);
                }
            }
            else
            {
                Console.WriteLine("{0} is already installed.", projectDependency);
            }
        }

        private static bool CheckIfFileExists(string item, string path)
        {
            string currFile = path + item;

            return File.Exists(currFile);
        }
    }
}
