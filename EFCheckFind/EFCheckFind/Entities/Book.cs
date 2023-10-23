namespace EFCheckFind.Entities;

public class Book
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int AuthorId { get; set; }


    public Author Author { get; set; }
}
