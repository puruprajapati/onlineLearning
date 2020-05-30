using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthorController: ControllerBase
	{
    private IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository)
    { _authorRepository = authorRepository; }

    [HttpGet("")]
    public IEnumerable<Author> GetAllAuthots() => _authorRepository.GetAll();

    [HttpGet("{authorName}")]
    public Task<Author> GetAuthorByName(String authorName) => _authorRepository.GetByName(authorName);

    [HttpPost("")]
    [AllowAnonymous]
    public void AddAuthor([FromBody] Author author) => _authorRepository.Insert(author);
  }
}
