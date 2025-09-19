namespace Domain.Filter;

public class ValidFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public ValidFilter()
    {

    }

    public ValidFilter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 1 ? 10 : pageSize;   
    }
}