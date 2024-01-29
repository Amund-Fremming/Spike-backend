using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class QuestionRepository(AppDbContext context)
{
    public readonly AppDbContext _context = context;

    public async Task<List<Question>> GetGameQuestionsByGameId(string gameId)
    {
        try {
            return await _context.Questions.Where(q => q.GameId == gameId).ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }   

    public async Task AddQuestionToGame(Question question)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Add(question);
            await _context.SaveChangesAsync();

            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new Exception(e.Message);
        }
    }

    public async Task<int> GetNumberOfQuestions(string gameId)
    {
        return await _context.Questions.CountAsync(q => q.GameId == gameId);
    }
}