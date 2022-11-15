using SortingApp.Models.Dtos;

namespace SortingApp.Services
{
    public interface ISortingService
    {
        Task<List<SortingDto>> SortingListAsync(int[] numberArray);
        Task<string[]> LoadSortedArrayAsync();
    }
}
