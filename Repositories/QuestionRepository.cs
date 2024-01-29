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
        using var transaction = _context.Database.BeginTransaction();

        _context.Add(question);
        await _context.SaveChangesAsync();

        transaction.Commit();
    }

    public async Task<int> GetNumberOfQuestions(string gameId)
    {
        return await _context.Questions.CountAsync(q => q.GameId == gameId);
    }
}