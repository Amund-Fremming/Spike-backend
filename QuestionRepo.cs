using Data;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository;

public class QuestionRepo
{
    public readonly AppDbContext _context;

    public QuestionRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetGameQuestionsAsync(int gameId)
    {
        return await _context.Questions.Where(q => q.GameId == gameId).ToListAsync();
    }   

    public async Task AddQuestionAsyncTransaction(Question question)
    {
        using(var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                _context.Add(question);
                await _context.SaveChangesAsync();

                transaction.Commit();
            }
            catch(Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}