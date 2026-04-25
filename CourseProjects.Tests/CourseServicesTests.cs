using System;
using Xunit;
using cs330_proj1;
using System.Collections.Generic;
using Moq;
using System.Linq;


namespace CourseProject.Tests
{
    public class CourseServicesTests
    {
        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalNotFound_ExceptionThrown()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(GetTestCourses());
            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
            new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
            });

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = GetTestCourses().First()
                }
            });

            var courseServices = new CourseServices(mockRepository.Object);
            var goalId = "CG5";
            var semester = "Spring 2021";

            // Act/Assert
            Assert.Throws<Exception>(() => courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester));
        }


        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndOneCourseOfferingIsInSemester_OfferingIsReturned()
        {
            // Arrange
            var course = new Course() {
                Name= "ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {
                course});

            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
            new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
            });

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course
                }
            });

            
            var goalId = "CG1";
            var semester = "Spring 2021";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester);

            // Assert
            var itemInList = Assert.Single(result);
            // Assert.Equal(2, result.Count());
            Assert.Equal(semester, itemInList.Semester);
            Assert.Equal(course.Name, itemInList.TheCourse.Name);
            
           
        }
        
        //Add unit tests for GetOfferingsByGoalIdAndSemester_GoalIsFoundAndMultipleCourseOfferingsAreInSemester_OfferingsAreReturned()

        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndMultipleCourseOfferingsAreInSemester_OfferingsAreReturned()
        {
            // Arrange
            var courses = GetTestCourses();

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(courses);

            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
            new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
            });

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = courses[0]
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = courses[1]
                }
            });

            
            var goalId = "CG1";
            var semester = "Spring 2021";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => 
                r.Semester == semester && 
                r.TheCourse.Name == courses[0].Name);

            Assert.Contains(result, r => 
                r.Semester == semester && 
                r.TheCourse.Name == courses[1].Name);
            
           
        }

        // Add unit test for GetOfferingsByGoalIdAndSemester_GoalIsFoundAndNoCourseOfferingIsInSemester_EmptyListIsReturned()
        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndNoCourseOfferingIsInSemester_EmptyListIsReturned()
        {
            // Arrange
            var courses = GetTestCourses();

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(courses);

            mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
            new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
            });

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = courses[0]
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = courses[1]
                }
            });

            
            var goalId = "CG1";
            var semester = "Spring 2022";
            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester);

            // Assert
            Assert.Empty(result);
            
           
        }

        [Fact]
        public void GetCourses_FoundCourses_ReturnListofCourses()
        {
            // Arrange
            var course = new Course() {
                Name= "ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"
            };
            var course2 = new Course() {
                Name= "ARTD 202",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"
            };

            var mockRepository = new Mock<ICourseRepository>();

            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {
                course, course2});

            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.GetCourses();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(course, result);
            Assert.Contains(course2, result);

            

        }
        [Fact]
        public void GetCourses_FoundOneCourse_ReturnsOneCourse()
        {  
            var course = new Course() {
                Name = "ARTD 201",
                Title = "graphic design",
                Credits = 3.0,
                Description="graphic design descr"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course> { course });

            var courseServices = new CourseServices(mockRepository.Object);

            var result = courseServices.GetCourses();

            var singleItem = Assert.Single(result);
            Assert.Equal("ARTD 201", singleItem.Name);
        }

        [Fact]
        public void GetCourses_FoundNoCourses_ReturnsEmptyList()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();

            mockRepository.Setup(m => m.Courses).Returns(new List<Course> {});

            var courseServices = new CourseServices(mockRepository.Object);

            //Act
            var result = courseServices.GetCourses();

            // Assert
            Assert.Empty(result);

        }

        [Fact]
        public void GetCourseOfferingsBySemester_MultipleCourseOfferingIsInSemester_ListOfOfferingsReturned()
        {

            // Arrange

            var courses = GetTestCourses();

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(courses);

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = courses[0]
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = courses[1]
                }
            });

            var courseServices = new CourseServices(mockRepository.Object);

            // Act

            var result = courseServices.GetCourseOfferingsBySemester("Spring 2021");

            // Assert

            Assert.Equal(2, result.Count());

            Assert.Contains(result, r => r.Semester == "Spring 2021" && r.TheCourse.Name == courses[0].Name);

            Assert.Contains(result, r => r.Semester == "Spring 2021" && r.TheCourse.Name == courses[1].Name
    );
        }

        [Fact]
        public void GetCourseOfferingsBySemester_OneCourseOfferingIsInSemester_SingleOfferingReturned()
        {

            // Arrange

            var course = new Course() {
                Name = "ARTD 201",
                Title = "graphic design",
                Credits = 3.0,
                Description="graphic design descr"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course>() {course});


            var offering = new CourseOffering {
                TheCourse = course,
                Semester = "Spring 2021",
                Section = "1"
            };

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() { offering});

            var courseServices = new CourseServices(mockRepository.Object);

            // Act

            var result = courseServices.GetCourseOfferingsBySemester("Spring 2021");

            // Assert

            var itemInList = Assert.Single(result);
            Assert.Equal("Spring 2021", itemInList.Semester);
        }
        [Fact]
        public void GetCourseOfferingsBySemester_NoOfferingsForSemester_EmptyListReturned()
        {

            // Arrange

            var course = new Course() {
                Name = "ARTD 201",
                Title = "graphic design",
                Credits = 3.0,
                Description="graphic design descr"
            };

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(new List<Course>() {course});


            var offering = new CourseOffering {
                TheCourse = course,
                Semester = "Spring 2022",
                Section = "1"
            };

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() { offering});

            var courseServices = new CourseServices(mockRepository.Object);

            // Act

            var result = courseServices.GetCourseOfferingsBySemester("Spring 2021");

            // Assert

            Assert.Empty(result);
        }
        [Fact]
        public void GetCourseOfferingsBySemesterAndDept_MultipleCourseOfferingsMatchDept_ListOfOfferingsReturned()
        {
            // Arrange
            var courses = GetTestCourses();

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(courses);

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = courses[0] 
                },
                new CourseOffering() {
                    Section = "2",
                    Semester = "Spring 2021",
                    TheCourse = courses[0] 
                }
            });

            var courseServices = new CourseServices(mockRepository.Object);

            // Act
            var result = courseServices.GetCourseOfferingsBySemesterAndDept("Spring 2021", "ARTD");

            // Assert
            Assert.Equal(2, result.Count());

            Assert.Contains(result, r => r.Semester == "Spring 2021" && r.TheCourse.Name.StartsWith("ARTD"));

            Assert.Contains(result, r => r.Semester == "Spring 2021" && r.Section == "2");
        }

        [Fact]
        public void GetCourseOfferingsBySemesterAndDept_SingleOfferingMatchSemesterandDept_SingleOfferingReturned()
        {
            // Arrange
            var courses = GetTestCourses();

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(courses);

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = courses[0] 
                },
            });

            var courseServices = new CourseServices(mockRepository.Object);

            // Act
            var result = courseServices.GetCourseOfferingsBySemesterAndDept("Spring 2021", "ARTD");

            // Assert
            var itemInList = Assert.Single(result);
            Assert.Equal("Spring 2021", itemInList.Semester);
        }

        [Fact]
        public void GetCourseOfferingsBySemesterAndDept_NoCourseOfferingsMatchDept_EmptyListReturned()
        {
            // Arrange
            var courses = GetTestCourses();

            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(m => m.Courses).Returns(courses);

            mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = courses[1] 
                }
            });

            var courseServices = new CourseServices(mockRepository.Object);

            // Act
            var result = courseServices.GetCourseOfferingsBySemesterAndDept("Spring 2021", "ARTD");

            // Assert
            Assert.Empty(result);
        }



        
        













        private List<Course> GetTestCourses()
        {
            return new List<Course>(){
            new Course() {
                Name="ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"

            },
            new Course() {
                Name="ARTS 101",
                Title="art studio",
                Credits=3.0,
                Description="studio descr"

            }
            };
        }

    }
}
