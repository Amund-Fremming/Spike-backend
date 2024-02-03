using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class QuestionRepository(AppDbContext context)
{
    public readonly AppDbContext _context = context;

    public async Task<List<Question>> GetGameQuestionsByGameId(string gameId)
    {
        return await _context.Questions
            .Where(q => q.GameId == gameId)
            .ToListAsync();
    }   

    public async Task AddQuestionToGame(Question question)
    {
        await using(var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                _context.Add(question);
                await _context.SaveChangesAsync();

                transaction.Commit();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public int GetNumberOfQuestions(string gameId)
    {
        return _context.Questions.Count(q => q.GameId == gameId);
    }
}