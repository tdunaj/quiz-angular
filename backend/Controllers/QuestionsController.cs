using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuizContext context;

        public QuestionsController(QuizContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return context.Questions;
            //return new Question[]
            //{
            //    new Question() { Text = "hello"},
            //    new Question() { Text = "hi"},               
            //};
        }

        [HttpGet("{quizId}")]
        public IEnumerable<Question> Get([FromRoute]int quizId)
        {
            return context.Questions.Where(question => question.QuizId == quizId);            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Question question)
        {
            var quiz = context.Quiz.SingleOrDefault(q => q.Id == question.QuizId);

            if (quiz == null)
                return NotFound();

            context.Questions.Add(question);
            await context.SaveChangesAsync();

            return Ok(question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Question question)
        {
            if (id != question.Id)
                return BadRequest();

            //var question = await context.Questions.SingleOrDefaultAsync(q => q.Id == id);
            context.Entry(question).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(question);
        }

    }
}