using API.Pitstop.Products.Contracts;
using API.Pitstop.Products.Controllers;
using API.Pitstop.Products.Services;

namespace API.Pitstop.Products.Managers
{
    public class StudentsManager : IStudentsManager
    {
        private readonly IStudentsService _StudentsService;

        public StudentsManager(IStudentsService studentsService)
        {
            _StudentsService = studentsService;
        }

        public Contracts.StudentsResponse GetAllStudents()
        {
            //var lstStudents = _StudentsService.GetAllStudents().Result;

            Models.Student student = new Models.Student();            
            Models.Student studentsec = new Models.Student();

            List<Models.Student> lstStudents = new List<Models.Student>();

            student.Name = "Sakshi";
            student.Age = 23;
            student.Grade = "A";
            lstStudents.Add(student);
            //lstProducts.Add(new Models.Product(2, "P02", "Semiconductor"));

            studentsec.Name = " Adarsh";
            studentsec.Age = 24;
            studentsec.Grade = "A+";
            lstStudents.Add(studentsec);

            var studentResponse = new Contracts.StudentsResponse() { };
            studentResponse.Students = lstStudents.Select(student => new Contracts.Student()
            {
                Name = student.Name,
                Age = student.Age,
                Grade = student.Grade
            });

            return studentResponse;
        }

    }
}
