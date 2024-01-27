using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class QuestionRepository
{
    public readonly AppDbContext _context;

    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetGameQuestionsAsync(string gameId)
    {
        try {
            return await _context.Questions.Where(q => q.GameId == gameId).ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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

    public int GetNumberOfQuestions(string gameId)
    {
        return _context.Questions.Count(q => q.GameId == gameId);
    }
}