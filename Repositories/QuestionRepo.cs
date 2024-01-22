using Data;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repositories;

public class QuestionRepo
{
    public readonly AppDbContext _context;

    public QuestionRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetGameQuestionsAsync(string gameId)
    {
        try {
            return await _context.Questions.Where(q => q.GameId == gameId).ToListAsync();
        } catch (Exception e)
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

    public async Task<int> GetNumberOfQuestions(string gameId)
    {
        try {
            return await _context.Questions
                .Where(q => q.GameId == gameId)
                .CountAsync();
        } catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}