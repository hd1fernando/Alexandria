using Alexandria.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Alexandria.Api.Controllers;

public class BookController : MainController
{
    [HttpPost]
    public ActionResult Create(BookViewModel bookViewModel)
    {

        return Ok();
    }
}

