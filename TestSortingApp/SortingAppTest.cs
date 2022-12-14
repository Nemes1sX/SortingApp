using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SortingApp.Services;
using System.Reflection.PortableExecutable;

namespace TestSortingApp
{
    public class Tests
    {
        private SortingService sortingService;

        [SetUp]
        public void Setup()
        {
           var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();
                            
           
            IConfiguration configuration = builder.Build();
            sortingService = new SortingService(configuration);
        }


        [Test]
        public async Task Test_SortingService_successfullSorting()
        {
            //Act
            int[] initialArray = { 6, 3, 12, 4, 15, 9, 5, 32, 11 };
            int[] expectedArray = {3, 4, 5, 6, 9, 11, 12, 15, 32};

            //Arrange
            var sortingList = await sortingService.SortingListAsync(initialArray);

            //Assert
            foreach(var sorting in sortingList)
            {
                Assert.IsTrue(sorting.SortedArray.SequenceEqual(expectedArray));
            }
        }


        [Test]
        public async Task Test_SortingSerivce_loadSucessfullyFile()
        {
            var loadedArrayText = await sortingService.LoadSortedArrayAsync();
            Assert.AreEqual(loadedArrayText.Length, 1);
        }
    }
}