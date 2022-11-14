using SortingApp.Models.Dtos;

namespace SortingApp.Services
{
    public interface ISortingService
    {
        SortingDto BubbleSort();
        SortingDto LinqSort();
        SortingDto QuickSort();
    }
}
