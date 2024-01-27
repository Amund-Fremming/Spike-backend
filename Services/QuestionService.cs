using Models;
using Repositories;

namespace Services;

public class QuestionService
{
    public readonly QuestionRepository _questionRepo;
    public readonly GameRepository _gameRepository;

    public QuestionService(QuestionRepository questionRepo, GameRepository gameRepository)
    {
        _questionRepo = questionRepo;
        _gameRepository = gameRepository;
    }

    public async Task<ICollection<Question>> GetGameQuestionsAsync(string gameId)
    {
        Game game = await _gameRepository.GetGameById(gameId) ?? throw new KeyNotFoundException($"Game with ID {gameId}, does not exist!");

        return await _questionRepo.GetGameQuestionsAsync(gameId);
    }

    public async Task AddQuestionAsyncTransaction(Question question)
    {

    }

    public int GetNumberOfQuestions(string gameId)
    {
        // TODO
        return 0;
    }

}