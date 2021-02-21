using Biblioteca.Core;
using Biblioteca.Core.Repositories;
using Biblioteca.Core.Repositories.Books;
using Biblioteca.Core.Repositories.Checkouts;
using Biblioteca.Data.Repositories;
using Biblioteca.Data.Repositories.Books;
using Biblioteca.Data.Repositories.Checkouts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDbContext _context;

        private BookRepository _bookRepository;
        private BookAuthorRepository _bookAuhtorRepository;
        private BookCategoryRepository _bookCategoryRepository;
        private AuthorRepository _authorRepository;
        private CategoryRepository _categoryRepository;

        private CheckoutRepository _checkoutRepository;
        private CountryRepository _countryRepository;
        private PaymentRepository _paymentRepository;
        private TicketRepository _ticketRepository;


        public UnitOfWork(ApiDbContext context)
        {
            this._context = context;
        }


        public IBookRepository Books => _bookRepository = _bookRepository ?? new BookRepository(_context);
        public IBookAuthorRepository BookAuthors => _bookAuhtorRepository = _bookAuhtorRepository ?? new BookAuthorRepository(_context);
        public IBookCategoryRepository BookCategories => _bookCategoryRepository = _bookCategoryRepository ?? new BookCategoryRepository(_context);
        public IAuthorRepository Authors => _authorRepository = _authorRepository ?? new AuthorRepository(_context);
        public ICategoryRepository Categories => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

     
        public ICheckoutRepository Checkouts => _checkoutRepository = _checkoutRepository ?? new CheckoutRepository(_context);

        public ICountryRepository Countries => _countryRepository = _countryRepository ?? new CountryRepository(_context);
        public IPaymentRepository Payments => _paymentRepository = _paymentRepository ?? new PaymentRepository(_context);
        public ITicketRepository Tickets => _ticketRepository = _ticketRepository ?? new TicketRepository(_context);



        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
