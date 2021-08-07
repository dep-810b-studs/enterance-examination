using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectOrientedProgramming
{
    public interface IUniversityLibrary
    {
        void addNewBook(string book);
        void borrowBook(string book, string student);
        void returnBook(string book, string student);
        List<string> findAvailableBooks();
    }

    public class UniversityLibrary : IUniversityLibrary
    {
        private readonly Dictionary<string, (bool isBorrowed, string borrowedBy)> _books = new ();

        public void addNewBook(string book)
        {
            ValidateBookTitle(book);
            _books.Add(book, (false, string.Empty));
        }

        public void borrowBook(string book, string student)
        {
            ValidateBookTitle(book);
            ValidateStudentName(student);
            
            CheckBookExisting(book);

            var bookInfo = _books[book];

            if (bookInfo.isBorrowed) 
                throw new ArgumentException($"This book already borrowed by student {bookInfo.borrowedBy}");

            _books[book] = (true, student);
        }

        public void returnBook(string book, string student)
        {
            ValidateBookTitle(book);
            ValidateStudentName(student);
            
            CheckBookExisting(book);
            
            var bookInfo = _books[book];

            if (!bookInfo.isBorrowed) 
                throw new ArgumentException("This book is not borrowed");

            if (bookInfo.borrowedBy != student)
                throw new ArgumentException("This book borrowed by another student");

            _books[book] = (false, String.Empty);
        }

        public List<string> findAvailableBooks()
        {
            var availableBooksSortedByTitle = _books
                .Where(book => !book.Value.isBorrowed)
                .Select(book => book.Key)
                .OrderBy(book => book);

            return availableBooksSortedByTitle.ToList();
        }

        private void ValidateBookTitle(string bookTitle)
        {
            if (bookTitle == string.Empty)
                throw new ArgumentException("Book title can't be empty");
        }
        
        private void ValidateStudentName(string studentName)
        {
            if (studentName == string.Empty)
                throw new ArgumentException("Book title can't be empty");
        }

        private void CheckBookExisting(string bookTitle)
        {
            if (!_books.ContainsKey(bookTitle))
                throw new ArgumentException($"There is no book with title {bookTitle} in library");
        }
    }
}