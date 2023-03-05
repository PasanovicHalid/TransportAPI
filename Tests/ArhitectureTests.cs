using NetArchTest.Rules;
using Shouldly;
using System.Reflection;

namespace TransportTests
{
    public class ArhitectureTests
    {
        private const string DomainNamespace = "Domain";
        private const string ApplicationNamespace = "Application";
        private const string InfrastructureNamespace = "Infrastructure";
        private const string PresentationNamespace = "Presentation";
        private const string WebNamespace = "TransportAPI";

        [Fact]
        public void Domain_should_not_have_dependency_on_other_projects()
        {
            //Arrange

            var assembly = Assembly.Load(DomainNamespace);

            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace,
            };

            //Act

            TestResult testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            //Assert

            testResult.IsSuccessful.ShouldBe(true);
        }

        [Fact]
        public void Application_should_not_have_dependency_on_other_projects()
        {
            //Arrange

            var assembly = Assembly.Load(ApplicationNamespace);

            var otherProjects = new[]
            {
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace,
            };

            //Act

            TestResult testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            //Assert

            testResult.IsSuccessful.ShouldBe(true);
        }

        [Fact]
        public void Infrastructure_should_not_have_dependency_on_other_projects()
        {
            //Arrange

            var assembly = Assembly.Load(InfrastructureNamespace);

            var otherProjects = new[]
            {
                PresentationNamespace,
                WebNamespace,
            };

            //Act

            TestResult testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            //Assert

            testResult.IsSuccessful.ShouldBe(true);
        }

        [Fact]
        public void Presentation_should_not_have_dependency_on_other_projects()
        {
            //Arrange

            var assembly = Assembly.Load(PresentationNamespace);

            var otherProjects = new[]
            {
                InfrastructureNamespace,
                WebNamespace,
            };

            //Act

            TestResult testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            //Assert

            testResult.IsSuccessful.ShouldBe(true);
        }
    }
}