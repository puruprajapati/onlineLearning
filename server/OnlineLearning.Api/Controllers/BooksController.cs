using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Api.Controllers.Base;
using OnlineLearning.EntityFramework;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
  {
    private IRepository<Book> bookRepository;
    public BooksController(IRepository<Book> bookRepository)
    { this.bookRepository = bookRepository; }

    [HttpGet]
    [Route("")]
    public IEnumerable<Book> GetAllBooks() => bookRepository.GetAll();

    [HttpGet]
    [Route("{bookId}")]
    public Book GetBookById(Guid bookId) => bookRepository.GetById(bookId);

    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    public void AddBook([FromBody] Book book) => bookRepository.Insert(book);

    [HttpDelete]
    [Route("{bookId}")]
    [AllowAnonymous]
    public void DeleteBook(Guid bookId) => bookRepository.Delete(bookId);
  }
}


//https://www.codeproject.com/Articles/5160941/ASP-NET-CORE-Token-Authentication-and-Authorizatio
//https://medium.com/@vaibhavrb999/jwt-authentication-authorization-in-net-core-3-1-e762a7abe00a
//https://github.com/Elfocrash/Youtube.AspNetCoreTutorial/blob/master/Tweetbook/Extensions/GeneralExtensions.cs
