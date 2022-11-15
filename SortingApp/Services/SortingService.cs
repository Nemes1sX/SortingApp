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
            BubbleSortAlgo(numbers);
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
            QuickSortAlgo(numbers, 0, numbers.Length-1);
            watch.Stop();
            sortingDto.SortedArray = numbers;
            sortingDto.SortingPerfomance = watch.Elapsed;
            sortingDto.SortingAlgorithm = "Quick sort";

            return sortingDto;
        }

        private void BubbleSortAlgo(int[] numbers)
        {
            int temp;
            for (int j = 0; j <= numbers.Length - 2; j++)
            {
                for (int i = 0; i <= numbers.Length - 2; i++)
                {
                    if (numbers[i] > numbers[i + 1])
                    {
                        temp = numbers[i + 1];
                        numbers[i + 1] = numbers[i];
                        numbers[i] = temp;
                    }
                }
            }
        }

        private void QuickSortAlgo(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    QuickSortAlgo(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSortAlgo(arr, pivot + 1, right);
                }
            }
        }

        private int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;


                }
                else
                {
                    return right;
                }
            }
        }

        private string SaveTextFile()
        {
            return _configuration.GetValue<string>("FileLocation:Path") ?? null;
        }
    }
}
