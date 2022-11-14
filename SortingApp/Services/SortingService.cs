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
           var watch = new System.Diagnostics.Stopwatch();
        }

        public SortingDto BubbleSort()
        {
            int temp;
            var textFile = GetTextFile();
            if (textFile == null)
            {
                throw new FileNotFoundException();
            }
            string[] lines = File.ReadAllLines(textFile);
            int[] numbers = new int[] {};
            for(int i=0; i<lines.Length; i++)
            {
                numbers = lines[i].Split(',').Select(int.Parse).ToArray();
            }

            var sortingDto = new SortingDto();
            watch.Start();
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
            watch.Stop();
            sortingDto.SortedArray = numbers;
            sortingDto.SortingPerfomance = watch.Elapsed;
            sortingDto.SortingAlgorithm = "Bubble";

            return sortingDto;
        }

        public SortingDto LinqSort()
        {
            var textFile = GetTextFile();
            if (textFile == null)
            {
                return null;
            }
            string[] lines = File.ReadAllLines(textFile);
            int[] numbers = new int[] { };
            for (int i = 0; i < lines.Length; i++)
            {
                numbers = lines[i].Split(',').Select(int.Parse).ToArray();
            }

            var sortingDto = new SortingDto();
            watch.Start();
            Array.Sort(numbers);
            watch.Stop();
            sortingDto.SortedArray = numbers;
            sortingDto.SortingPerfomance = watch.Elapsed;
            sortingDto.SortingAlgorithm = "Linq";

            return sortingDto;
        }

        public SortingDto QuickSort()
        {
            var textFile = GetTextFile();
            if (textFile == null)
            {
                throw new FileNotFoundException();
            }
            string[] lines = File.ReadAllLines(textFile);
            int[] numbers = new int[] { };

       
            for (int i = 0; i < lines.Length; i++)
            {
                numbers = lines[i].Split(',').Select(int.Parse).ToArray();
            }

            var sortingDto = new SortingDto();
            watch.Start();
            QuickSort(numbers, 0, numbers.Length-1);
            watch.Stop();
            sortingDto.SortedArray = numbers;
            sortingDto.SortingPerfomance = watch.Elapsed;
            sortingDto.SortingAlgorithm = "Quick sort";

            return sortingDto;
        }

        private void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    QuickSort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(arr, pivot + 1, right);
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

        private string GetTextFile()
        {
            return _configuration.GetValue<string>("FileLocation:Path") ?? null;
        }
    }
}
