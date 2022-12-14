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

        public async Task<List<SortingDto>> SortingListAsync(int[] numberArray)
        {
            var sortingList = new List<SortingDto>();

           sortingList.Add(BubbleSort(numberArray));
           sortingList.Add(ArraySort(numberArray));
           sortingList.Add(QuickSort(numberArray));

            string sortedArray = string.Join(",", sortingList.First().SortedArray);
            var savePath = SaveTextFile();
            await File.WriteAllTextAsync(savePath, sortedArray);

            return sortingList;
        }

        public async Task<string[]> LoadSortedArrayAsync()
        {
            var loadPath = SaveTextFile();
            if (loadPath == null)
            {
                throw new FileNotFoundException();
            }
            string[] lines = await File.ReadAllLinesAsync(loadPath);
            return lines;
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
            watch.Reset();
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
            watch.Reset();

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
            watch.Reset();

            return sortingDto;
        }

        private string SaveTextFile()
        {
            return _configuration.GetValue<string>("FileLocation:Path") ?? null;
        }

    }
}
