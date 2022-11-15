using SortingApp.Helpers;
using SortingApp.Models.Dtos;
using System.Diagnostics;

namespace SortingApp.Services
{
    public class SortingService : ISortingService
    {
        private IConfiguration _configuration;
        private readonly Stopwatch watch = new System.Diagnostics.Stopwatch();

        public SortingService(IConfiguration configuration)
        {
           _configuration = configuration;
        }

        public List<SortingDto> SortingList()
        {
            var sortingList = new List<SortingDto>();
            int[] numberArray = { 6, 8, 1, 5, 4, 12, 3, 34 };

            sortingList.Add(BubbleSort(numberArray));
            sortingList.Add(ArraySort(numberArray));
            sortingList.Add(QuickSort(numberArray));

            string sortedArray = string.Join(",", sortingList.First().SortedArray);
            var savePath = SaveTextFile();
            File.WriteAllText(savePath, sortedArray);

            return sortingList;
        }

        private SortingDto BubbleSort(int[] numbers)
        {
            var sortingDto = new SortingDto();
            watch.Start();
            SortingAlgo.BubbleSortAlgo(numbers);
            watch.Stop();
            sortingDto.SortedArray = numbers;
            sortingDto.SortingPerfomance = watch.Elapsed;
            sortingDto.SortingAlgorithm = "Bubble";

            return sortingDto;
        }

        private SortingDto ArraySort(int[] numbers)
        {
            var sortingDto = new SortingDto();
            watch.Start();
            Array.Sort(numbers);
            watch.Stop();
            sortingDto.SortedArray = numbers;
            sortingDto.SortingPerfomance = watch.Elapsed;
            sortingDto.SortingAlgorithm = "Linq";

            return sortingDto;
        }

        private SortingDto QuickSort(int[] numbers)
        { 
            var sortingDto = new SortingDto();
            watch.Start();
            SortingAlgo.QuickSortAlgo(numbers, 0, numbers.Length-1);
            watch.Stop();
            sortingDto.SortedArray = numbers;
            sortingDto.SortingPerfomance = watch.Elapsed;
            sortingDto.SortingAlgorithm = "Quick sort";

            return sortingDto;
        }

        private string SaveTextFile()
        {
            return _configuration.GetValue<string>("FileLocation:Path") ?? null;
        }
    }
}
