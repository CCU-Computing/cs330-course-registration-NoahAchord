using System;
using cs330_proj1;
using System.Collections.Generic;

namespace cs330courses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("My name is Noah");
            ICourseRepository repo = new CourseRepository();
            CourseServices service = new CourseServices(repo);

            Console.WriteLine("---------");
            List<CourseOffering> theList = service.GetOfferingsByGoalIdAndSemester("CG2","Spring 2021");
            foreach(CourseOffering c in theList) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            
            // Make each of the next 6 sections of code work
            
            List<Course> theList2 = service.GetCourses();
            foreach(Course c in theList2) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");
            
            List<CourseOffering> theList3 = service.GetCourseOfferingsBySemester("Fall 2020");
            foreach(CourseOffering c in theList3) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            
            List<CourseOffering> theList4 = service.GetCourseOfferingsBySemesterAndDept("Fall 2020","CSCI");
            foreach(CourseOffering c in theList4) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            /*
            List<Course> theList5 = service.getCoursesByGoalId("CG2");
            foreach(Course c in theList5) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            List<Course> theList6 = service.getCoursesByGoalIds("CG2","CG1");
            foreach(Course c in theList6) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            List<CoreGoal> theList7 = service.getCoreGoalsThatAreNotCoveredBySemester("Fall 2020");
            foreach(CoreGoal c in theList7) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");
           */


            

        }//end main
    }
}
