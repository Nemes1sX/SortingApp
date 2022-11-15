using SortingApp.Models.Dtos;

namespace SortingApp.Services
{
    public interface ISortingService
    {
        Task<List<SortingDto>> SortingList(int[] numberArray);
        Task<string[]> LoadSortedArray();
    }
}
