using GreenStar.API.Controllers.Api;
using GreenStar.Entity.Academy;
using GreenStar.Service;
using GreenStar.Service.Repositories.Academy;
using GreenStar.Service.UnitOfWork;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GreenStar.API.Test
{
    [TestFixture]
    public class TestStudentEntry
    {
        private IStudentRepository _studentRepository;
        private IUnitOfWork _unitOfWork;
        // private ApplicationDbContext _dbEntities;
        public List<Student> studentList;

        [Test]
        public void TestDemo()
        {
            var controller = new DemoController();
            var res = controller.Get(1);
            Assert.AreEqual("value", res);
        }

        [SetUp]
        public void ReInitializeTest()
        {
            ////_products = SetUpProducts();
            //var _dbEntities = new Mock<ApplicationDbContext>();
            ////_productRepository = SetUpProductRepository();
            //var unitOfWork = new Mock<IUnitOfWork>();
            //unitOfWork.SetupGet(s => s.Students).Returns(_studentRepository);
            //_unitOfWork = unitOfWork.Object;
            //// _studentRepository = new StudentRepository(_dbEntities.);
            studentList = new List<Student> { new Student { studID = 1, studName = "test" } };
        }

        //     //EventRepository eventRepository = null;
        //     private readonly IUnitOfWork _unitOfWork;

        //     ApplicationDbContext appDB;

        //     StudentController objStudentController;

        //     string ServiceBaseURL = string.Empty;

        // public TestStudentEntry(IUnitOfWork unitOfWork)
        // {
        //     ServiceBaseURL = "https://localhost:44393";

        //     _unitOfWork = unitOfWork;
        // }

        //[Test]
        // public void GetStudentByIdTest()
        // {
        //     var studentController = new StudentController(_unitOfWork);
        //     {
        //         Request = new HttpRequestMessage
        //         {
        //             Method = HttpMethod.Get,
        //             RequestUri = new Uri(ServiceBaseURL + "api/Student/Product/productid/2")
        //         }
        //     };
        //     productController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        //     _response = productController.Get(2);
        //     varresponseResult = JsonConvert.DeserializeObject<Product>(_response.Content.ReadAsStringAsync().Result);
        //     Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        //     AssertObjects.PropertyValuesAreEquals(responseResult, _products.Find(a => a.ProductName.Contains("Mobile")));
        // }
        

        
        public IStudentRepository SetupStudentRepository()
        {
            // Init repository
            var repo = new Mock<IStudentRepository>();

            // Setup mocking behavior
            repo.Setup(r => r.GetAll()).Returns(studentList);

            repo.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(new Func<int, Student>(
                    id => studentList.Find(a => a.studID.Equals(id))));

            repo.Setup(r => r.Add(It.IsAny<Student>()))
                .Callback(new Action<Student>(newArticle =>
                {
                    dynamic maxArticleID = studentList.Last().studID;
                    dynamic nextArticleID = maxArticleID + 1;
                    newArticle.studID = nextArticleID;
                    newArticle.studName ="test";
                    studentList.Add(newArticle);
                }));

            

            
            // Return mock implementation
            return repo.Object;
        }

        //[Test]
        public void GetStudentByIdTest()
        {

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(x => x.Students.GetAll()).Returns(studentList);
            //mockRepository.Setup(r => r.Students.Get("some param")).Returns(entities);
            // Set up Prerequisites   
            var controller = new StudentController(mockRepository.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get();
            // Assert the result  
            //Employee employee;
            //Assert.IsTrue(response.TryGetContentValue<Employee>(out employee));
            //Assert.AreEqual("Jignesh", employee.Name);


            ////Arrange
            //var mockRepository = new Mock<IUnitOfWork>();
            ////mockRepository.Setup(r => r.Students.Get("some param")).Returns(entities);

            //mockRepository.Setup(x => x.Students.GetAll()).Returns(studentList);
            //var controller = new StudentController(mockRepository.Object);

            ////Act
            //var result= controller.Get(1);

            ////Assert
            //mockRepository.VerifyAll();
            ////Assert.AreEqual(1, result.Student)

            ////var student = _studentRepository.Get(1);
            ////CollectionAssert.AreEqual(student.studName, "Satyajit");
        }
    }
}
