using Model;

namespace StudentsApi
{
    public class Database
    {
        static string fileName = "C:\\Users\\jerzy\\Desktop\\APDB\\cwiczenia_3\\StudentsApi\\dane.csv";

        public static IEnumerable<Student> GetAll()
        {
            return File.ReadAllLines(fileName).Select(a => new Student(a.Split(",")));
        }

        public static Student UpdateStudent(Student student)
        {
            SaveAll(GetAll()
                .Select(a => a.indexNumber == student.indexNumber ? student : a));
            return student;
        }

        public static void SaveAll(IEnumerable<Student> students)
        {
            File.WriteAllLines(fileName, students.Select(a => a.ToString()));
        }


    }
}
