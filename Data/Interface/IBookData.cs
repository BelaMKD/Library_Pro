using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IBookData
    {
        Book CreateBook(Book book);
        Book UpdateBook(Book book);
        Book DeleteBook(int bookId);
        Book GetBookByid(int bookId);
        IEnumerable<Book> GetBooks();
        int Commit();
    }
}
