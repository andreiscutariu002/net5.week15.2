namespace UsersApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> List = new List<User>
        {
            new User {Id = 1, City = "Iasi", Email = "andrei@site.com", Username = "andrei"},
            new User {Id = 2, City = "Iasi", Email = "misu@site.com", Username = "misu"}
        };

        // GET api/users
        [Route("")]
        [HttpGet]
        public ActionResult<List<User>> Get()
        {   
            return this.Ok(List);
        }

        // GET api/users/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult<User> Get(int id)
        {
            if (id < 0)
            {
                return this.BadRequest("Id should be a positive number!");
            }

            var user = List.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok(user);
        }

        [Route("{id}/address")]
        [HttpGet]
        public ActionResult<string> GetAddress(int id)
        {
            if (id < 0)
            {
                return this.BadRequest("Id should be a positive number!");
            }

            var user = List.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok("O adresa");
        }

        [Route("")]
        [HttpPost]
        public IActionResult Create(User model)
        {
            model.Id = List.Count + 1;

            List.Add(model);

            return this.CreatedAtAction("Get", new { id = model.Id }, model);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = List.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return this.NotFound();
            }

            List.Remove(user);

            return this.Ok();
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update(int id, User model)
        {
            var user = List.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return this.NotFound();
            }

            user.Email = model.Email;
            user.Username = model.Username;
            user.City = model.City;
            user.Description = model.Description;
            user.Street = model.Street;

            return this.Ok();
        }
    }
}
