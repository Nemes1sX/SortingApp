namespace SortingApp.Models.Dtos
{
    public class SortingDto
    {
        public int[] SortedArray { get; set; } 
        public TimeSpan SortingPerfomance { get; set; }
        public string SortingAlgorithm { get; set; }
    }
}
