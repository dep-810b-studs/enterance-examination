using System;
using System.IO;
using System.Linq;

namespace TextFilesProcessing
{
    public record Department(string Title, string HeadLastName, int GraduatedBachelorsVolume, uint Year)
    {
        public static Department GetFromLine(string line)
        {
            var splittedLine = line.Split(" ");
            
            var title = splittedLine[0];
            var headLastName = splittedLine[1];
            
            if (!int.TryParse(splittedLine[2], out var graduatedBachelorsVolume))
                throw new ArgumentException(
                    $"GraduatedBachelorsVolume for department {title} was not in correct format");

            if (!uint.TryParse(splittedLine[3], out var year))
                throw new ArgumentException(
                    $"Graduate year for department {title} was not in correct format");

            return new(title, headLastName, graduatedBachelorsVolume, year);
        }
    }

    public static class DepartmentsProcessor
    {
        public static void ProcessFile(string fullFileName)
        {
            if (!File.Exists(fullFileName))
            {
                throw new FileNotFoundException("File with departments didn't found", fullFileName);
            }

            var departments = File.ReadLines(fullFileName)
                .Select(Department.GetFromLine)
                .ToList();

            var averageGraduatedBachelorsVolume = departments
                .Select(department => department.GraduatedBachelorsVolume)
                .Average();

            var needPercentage = (averageGraduatedBachelorsVolume / 100) * 15;

            var needDepartments = departments
                .GroupBy(department => department.Title)
                .Select(department =>
                {
                    var averageDepartmentGraduatedBachelorsVolume = department
                        .Select(currentDepartment => currentDepartment.GraduatedBachelorsVolume)
                        .Average();

                    return (department.Key, averageDepartmentGraduatedBachelorsVolume);
                })
                .Where(department =>
                    (averageGraduatedBachelorsVolume -  department.averageDepartmentGraduatedBachelorsVolume) == needPercentage)
                .OrderBy(department => department.Key)
                .Select(department => department.Key)
                .ToList();

            foreach (var department in needDepartments)
            {
                Console.WriteLine($"Department {department}");
            }
        }
    }
}