using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IBookCopiesData
    {
        BookCopies CreateBookCopies(BookCopies bookCopies);
        BookCopies UpdateBookCopies(BookCopies bookCopies);
        BookCopies DeleteBookCopies(int bookCopiesId);
        BookCopies GetBookCopiesById(int bookCopiesId);
        IEnumerable<BookCopies> BookCopies();
        int Commit();
    }
}
